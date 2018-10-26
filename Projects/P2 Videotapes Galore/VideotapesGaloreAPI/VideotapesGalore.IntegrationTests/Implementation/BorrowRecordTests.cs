using System.Collections.Generic;
using System.Net;
using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using VideotapesGalore.WebApi;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Models.DTOs;
using Newtonsoft.Json;
using VideotapesGalore.IntegrationTests.Context;

namespace VideotapesGalore.IntegrationTests.Implementation
{
    [Collection("Test Context Collection")]
    public class BorrowRecordTests
    {
        /// <summary>
        /// Startup factory for test context
        /// </summary>
        private readonly WebApplicationFactory<Startup> _factory;

        /// <summary>
        /// Setup test environment (along with seeded content)
        /// </summary>
        private TestsContextFixture _fixture;

        /// <summary>
        /// HTTP client to use
        /// </summary>
        protected HttpClient client;

        /// <summary>
        /// Setup web application context as factory
        /// </summary>
        /// <param name="factory">the web application context</param>
        public BorrowRecordTests(TestsContextFixture fixture)
        {

            this._factory = fixture.factory;
            this._fixture = fixture;
            this.client = fixture.factory.CreateClient();
        }

        /// <summary>
        /// Simulates and tests all basic functionalities for the relations for user and tape resources in system e.g. borrow records
        /// And simulates possible actions (registering tape on loan, returning a tape for user, update borrow record (admin functionality) and reading records)
        /// Also verify that proper errors are returned when user provides any bad or invalid input
        /// </summary>
        [Fact]
        public async Task SimulateBorrowRecordActions()
        {
            // Use seeded content to aid tests, a single tape and single user from initialized content
            // Seeded user no 2 has no borrow records and seeded tape no 1 is available (e.g. not on loan) so use that content
            // Construct base URL to request borrow relation for selected seeded content
            int tapeNo = 1, userNo = 2;
            var tapeId = _fixture.tapeIds[tapeNo];
            var userId = _fixture.userIds[userNo];
            var userLocation = _fixture.userUrls[userNo];
            var borrowRecordUrl = userLocation + "/tapes/" + tapeId;

            // Create borrow record for user x for tape y (some user and some tape from seeded content intialized)
            // Expect operation to be successful and return status code of 200 (OK)
            // Then get the new record as it should be and ensure that an entry exists for user for tape with no return date
            var createRecordResponse = await client.PostAsync(borrowRecordUrl, null);
            Assert.Equal("", await createRecordResponse.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.Created, createRecordResponse.StatusCode);
            var newRecord = (await GetUserBorrowRecords(userLocation)).History
                .ToList()
                .FirstOrDefault(r => r.TapeId == tapeId && r.UserId == userId && r.ReturnDate == null);
            Assert.NotNull(newRecord);

            // Return the tape we just registered on loan (done with DELETE method)
            // Verify that operation works (e.g. we recieve status code of 204 (No Content)
            // Then get all borrow records again and verify that the return date has been set (e.g. is not null anymore)
            // Then try to return tape again and expect an error (as tape is not on loan for user anymore)
            var returnTapeResponse = await client.DeleteAsync(borrowRecordUrl);
            Assert.Equal(HttpStatusCode.NoContent, returnTapeResponse.StatusCode);
            var review = (await GetUserBorrowRecords(userLocation)).History
                .ToList()
                .FirstOrDefault(r => r.TapeId == tapeId && r.UserId == userId);
            Assert.NotNull(review.ReturnDate);
            returnTapeResponse =  await client.DeleteAsync(borrowRecordUrl);
            Assert.Equal(HttpStatusCode.NotFound, returnTapeResponse.StatusCode);

            // Update the borrow record (admin functionality only, so full flexibility for input model)
            // Expect operation to work and get a response of 204 (No Content)
            // Re-fetch borrow record and expect returned date to be changed
            var updateModel = new BorrowRecordInputModel(){ BorrowDate = new DateTime(0), ReturnDate = DateTime.Now };
            var updateReviewResponse = await UpdateBorrowRecord(borrowRecordUrl, updateModel);
            Assert.Equal(HttpStatusCode.NoContent, updateReviewResponse.StatusCode);
            var updatedReview = (await GetUserBorrowRecords(userLocation)).History
                .ToList()
                .FirstOrDefault(r => r.TapeId == tapeId && r.UserId == userId);
            Assert.NotEqual(review.ReturnDate, updatedReview.ReturnDate);
        }

        /// <summary>
        /// Updates borrow record
        /// </summary>
        /// <param name="url">url to borrow record</param>
        /// <param name="input">input model for borrow record</param>
        /// <returns>the API HTTP response to request</returns>
        private async Task<HttpResponseMessage> UpdateBorrowRecord(string url, BorrowRecordInputModel input)
        {
            var recordInputJson = JsonConvert.SerializeObject(input);
            HttpContent content = new StringContent(recordInputJson, Encoding.UTF8, "application/json");
            return await client.PutAsync(url, content);
        }

        /// <summary>
        /// Gets list of all borrow records for user
        /// </summary>
        /// <param name="url">url to user borrow records</param>
        /// <returns>the API HTTP response to request</returns>
        private async Task<UserDetailDTO> GetUserBorrowRecords(string url)
        {
            var getUserReviewsResponse = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, getUserReviewsResponse.StatusCode);
            var borrowRecords = JsonConvert.DeserializeObject<UserDetailDTO>(await getUserReviewsResponse.Content.ReadAsStringAsync());
            return borrowRecords;
        }
    }
}