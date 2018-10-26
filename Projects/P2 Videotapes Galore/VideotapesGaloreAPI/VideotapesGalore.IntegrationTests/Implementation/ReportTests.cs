using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using VideotapesGalore.IntegrationTests.Context;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.WebApi;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using System.Net.Http;

namespace VideotapesGalore.IntegrationTests.Implementation
{
    [Collection("Test Context Collection")]
    public class ReportTests
    {
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
        public ReportTests(TestsContextFixture fixture)
        {
            this._fixture = fixture;
            this.client = fixture.factory.CreateClient();
        }

        /// <summary>
        /// Tests user loan date report all through the API and the database,
        /// verifies that the information is correct for the test data
        /// </summary>
        [Fact]
        public async Task TestUserLoanDateReport()
        {
            var path = "api/v1/users?LoanDate=2018-09-10";
            var reportResponse = await client.GetAsync(path);
            Assert.Equal(HttpStatusCode.OK, reportResponse.StatusCode);
            // Select the test users we created to ensure that we get only the test data we know
            var users = await SelectUsers(reportResponse);
            Assert.Equal(2, users.Count);
        }

        /// <summary>
        /// Tests user loan duration report all through the API and the database,
        /// verifies that the information is correct for the test data
        /// </summary>
        [Fact]
        public async Task TestUserLoanDurationReport()
        {
            var path = "api/v1/users?LoanDuration=10";
            var reportResponse = await client.GetAsync(path);
            Assert.Equal(HttpStatusCode.OK, reportResponse.StatusCode);
            // Select the test users we created to ensure that we get only the test data we know
            var users = await SelectUsers(reportResponse);
            Assert.Single(users);
        }

        /// <summary>
        /// Tests user loan date report and loan duration together all through the API and the database,
        /// verifies that the information is correct for the test data
        /// </summary>
        [Fact]
        public async Task TestUserLoanDurationAndDateReport()
        {
            var path = "api/v1/users?LoanDate=2018-09-10&LoanDuration=10";
            var reportResponse = await client.GetAsync(path);
            Assert.Equal(HttpStatusCode.OK, reportResponse.StatusCode);
            // Select the test users we created to ensure that we get only the test data we know
            var users = await SelectUsers(reportResponse);
            Assert.Equal(2, users.Count);
        }

        /// <summary>
        /// Tests tape loan date report all through the API and the database,
        /// verifies that the information is correct for the test data
        /// </summary>
        [Fact]
        public async Task TestTapeLoanDateReport()
        {
            var path = "api/v1/tapes?LoanDate=2018-09-10";
            var reportResponse = await client.GetAsync(path);
            Assert.Equal(HttpStatusCode.OK, reportResponse.StatusCode);
            // Select the test tapes we created to ensure that we get only the test data we know
            var tapes = await SelectTapes(reportResponse);
            Assert.Equal(2, tapes.Count);
        }

        private async Task<List<UserDTO>> SelectUsers(HttpResponseMessage reportResponse)
        {
            var users = JsonConvert.DeserializeObject<List<UserDTO>>(await reportResponse.Content.ReadAsStringAsync());
            return users.Where(u => _fixture.userIds.IndexOf(u.Id) != -1).ToList();
        }

        private async Task<List<TapeDTO>> SelectTapes(HttpResponseMessage reportResponse)
        {
            var tapes = JsonConvert.DeserializeObject<List<TapeDTO>>(await reportResponse.Content.ReadAsStringAsync());
            return tapes.Where(t => _fixture.tapeIds.IndexOf(t.Id) != -1).ToList();
        }
    }
}