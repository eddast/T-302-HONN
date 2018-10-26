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

namespace VideotapesGalore.IntegrationTests.Implementation
{
    [Collection("Test Context Collection")]
    public class BorrowRecordTests
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private TestsContextFixture _fixture;
        private ITestOutputHelper output;

        /// <summary>
        /// Setup web application context as factory
        /// </summary>
        /// <param name="factory">the web application context</param>
        public BorrowRecordTests(TestsContextFixture fixture, ITestOutputHelper output) {

            _factory = fixture.factory;
            _fixture = fixture;
            this.output = output;
        }

        /// <summary>
        /// Simulates and tests all basic CRUD functionalities for Tape resource in system
        /// E.g. adding tape resources, updating tape resources, deleteting tape resources and reading tape resources accordingly
        /// Also verify that proper errors are returned when user provides any bad or invalid input
        /// </summary>
        [Fact]
        public async Task SimulateTapeCRUD()
        {
            // Base URL to tape resources
            string tapesBaseRoute = "api/v1/tapes";
            string userBaseRoute = "api/v1/users";
            var client = _factory.CreateClient();

            /// [POST] create new tape using a valid tape model
            /// Expect response to POST request to be 201 (created) and expect to get location header pointing to new resource, then
            /// [GET] user by the pointer from location header for response to previous POST request and check if user values match
            var tapeInput = new TapeInputModel() {
                Title = "Mojo Jojo: Greatest Townsville Attacks",
                Director = "Definately Not Mojo Jojo",
                ReleaseDate = DateTime.Now,
                Type = "VHS",
                EIDR = "10.5240/2B3B-1E0E-9314-2C6E-A453-3"
            };
            var createTapeResponse = await PostTape(client, tapesBaseRoute, tapeInput);
            Assert.Equal(HttpStatusCode.Created, createTapeResponse.StatusCode);
            var newTapeResourceLocation = createTapeResponse.Headers.Location;

            var userInput = new UserInputModel(){
                Name = "Mojo Jojo",
                Email = "m_jo@eviloverlords.com",
                Phone = "123 456 789",
                Address = "Townsville"
            };
            var createUserResponse = await PostUser(client, userBaseRoute, userInput);
            Assert.Equal(HttpStatusCode.Created, createUserResponse.StatusCode);
            var newUserResourceLocation = createUserResponse.Headers.Location;
            var borrowRecordUrl = GetRecordPath(newUserResourceLocation.LocalPath, newTapeResourceLocation.LocalPath);
            /* ***************** TEMP ABOVE ******************* */

            var createRecordResponse = await CreateBorrowRecord(client, borrowRecordUrl);
            Assert.Equal(HttpStatusCode.Created, createRecordResponse.StatusCode);
            string userTapeUrl = newUserResourceLocation.LocalPath;
            var userReviewCount = (await GetUserBorrowRecords(client, userTapeUrl)).History.Count();
            Assert.Equal(1, userReviewCount);

            // Return the newly created tape
            var returnTapeResponse = await ReturnTape(client, borrowRecordUrl);
            Assert.Equal(HttpStatusCode.NoContent, returnTapeResponse.StatusCode);

            // Get all borrow records again and verify that the return date has been updated
            var allReviews = (await GetUserBorrowRecords(client, userTapeUrl)).History.ToList();
            Assert.Equal(1, allReviews.Count);
            Assert.NotNull(allReviews[0].ReturnDate);

            // Try to return tape again to verify that an error occurs
            returnTapeResponse = await ReturnTape(client, borrowRecordUrl);
            Assert.Equal(HttpStatusCode.NotFound, returnTapeResponse.StatusCode);
            
            var updateModel = new BorrowRecordInputModel() {
                BorrowDate = new DateTime(0),
                ReturnDate = DateTime.Now
            };
            var updateReviewResponse = await UpdateBorrowRecord(client, borrowRecordUrl, updateModel);
            Assert.Equal(HttpStatusCode.NoContent, updateReviewResponse.StatusCode);

            var updatedReviews = (await GetUserBorrowRecords(client, userTapeUrl)).History.ToList();
            Assert.Equal(1, updatedReviews.Count);
            Assert.NotEqual(allReviews[0].ReturnDate, updatedReviews[0].ReturnDate);
        }

        /// <summary>
        /// Creates new tape into the system e.g. conducts POST request
        /// Returns response for post request
        /// </summary>
        /// <param name="client">http client to use to issue request to API</param>
        /// <param name="url">url to issue request to</param>
        /// <param name="tapeInput">input model to use to create new tape</param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> PostTape(HttpClient client, string url, TapeInputModel tapeInput)
        {
            var tapeInputJSON = JsonConvert.SerializeObject(tapeInput);
            HttpContent content = new StringContent(tapeInputJSON, Encoding.UTF8, "application/json");
            return await client.PostAsync(url, content);
        }

        /// <summary>
        /// Creates new user into the system e.g. conducts POST request
        /// Returns response for post request
        /// </summary>
        /// <param name="client">http client to use to issue request to API</param>
        /// <param name="url">url to issue request to</param>
        /// <param name="tapeInput">input model to use to create new tape</param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> PostUser(HttpClient client, string url, UserInputModel userInput)
        {
            var userInputJSON = JsonConvert.SerializeObject(userInput);
            HttpContent content = new StringContent(userInputJSON, Encoding.UTF8, "application/json");
            return await client.PostAsync(url, content);
        }

        private async Task<HttpResponseMessage> CreateBorrowRecord(HttpClient client, string url)
        {
            return await client.PostAsync(url, null);
        }

        private async Task<HttpResponseMessage> GetUrl(HttpClient client, string url)
        {
            return await client.GetAsync(url);
        }

        private async Task<HttpResponseMessage> UpdateBorrowRecord(HttpClient client, string url, BorrowRecordInputModel input)
        {
            var recordInputJson = JsonConvert.SerializeObject(input);
            HttpContent content = new StringContent(recordInputJson, Encoding.UTF8, "application/json");
            return await client.PutAsync(url, content);
        }

        private async Task<HttpResponseMessage> ReturnTape(HttpClient client, string url)
        {
            return await client.DeleteAsync(url);
        }

        private async Task<UserDetailDTO> GetUserBorrowRecords(HttpClient client, string url)
        {
            var getUserReviewsResponse = await GetUrl(client, url);
            Assert.Equal(HttpStatusCode.OK, getUserReviewsResponse.StatusCode);
            var borrowRecords = JsonConvert.DeserializeObject<UserDetailDTO>(await getUserReviewsResponse.Content.ReadAsStringAsync());
            return borrowRecords;
        }


        private string GetRecordPath(string userpath, string tapepath)
        {
            return (userpath + "/tapes" + tapepath.Substring(tapepath.LastIndexOf("/")));
        }
    }
}