using System.Collections.Generic;
using System.Net;
using System;
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

namespace VideotapesGalore.IntegrationTests
{
    public class UserTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly ITestOutputHelper output;

        public UserTests(ITestOutputHelper output)
        {
            AutoMapper.Mapper.Reset();
            _factory = SetupTests.GetWebApplicationFactory();
            this.output = output;
        }

        [Fact]
        public async Task SimulateUserLoanActivity()
        {
            /*** Urls to use in test ***/
            string user = "api/v1/users";
            //string tape1 = "tapes/1";
            //string tape2 = "tapes/2";
            var client = _factory.CreateClient();
            var userInput = new UserInputModel(){
                Name = "Test user 1",
                Email = "testing@users.com",
                Phone = "8449919",
                Address = "address 1337"
            };
            var response = await client.PostAsync(user, new StringContent(userInput.ToString()));
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task SimulateUserReviewActivity()
        {
            /*** Urls to use in test ***/
            string user = "api/v1/users";
            string tape1 = "tapes/1";
            string tape2 = "tapes/2";

            // Arrange
            var client = _factory.CreateClient();
            var userInput = new UserInputModel(){
                Name = "Test user 1",
                Email = "testing@users.com",
                Phone = "8449919",
                Address = "address 1337"
            };

            // Act
            var response = await client.PostAsync(user, new StringContent(userInput.ToString()));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task SimulateUserReturnTapeActivity()
        {
            /*** Urls to use in test ***/
            string user = "api/v1/users";
            string tape1 = "tapes/1";
            string tape2 = "tapes/2";

            // Arrange
            var client = _factory.CreateClient();
            var userInput = new UserInputModel(){
                Name = "Test user 1",
                Email = "testing@users.com",
                Phone = "8449919",
                Address = "address 1337"
            };

            // Act
            var response = await client.PostAsync(user, new StringContent(userInput.ToString()));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task SimulateUserRecommendationActivity()
        {
            /*** Urls to use in test ***/
            string user = "api/v1/users";
            string tape1 = "tapes/1";
            string tape2 = "tapes/2";

            // Arrange
            var client = _factory.CreateClient();
            var userInput = new UserInputModel(){
                Name = "Test user 1",
                Email = "testing@users.com",
                Phone = "8449919",
                Address = "address 1337"
            };

            // Act
            var response = await client.PostAsync(user, new StringContent(userInput.ToString()));

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}