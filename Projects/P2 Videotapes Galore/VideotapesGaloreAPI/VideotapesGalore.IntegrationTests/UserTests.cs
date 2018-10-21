using System.Collections.Generic;
using System.Net;
using System;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using VideotapesGalore.WebApi;

namespace VideotapesGalore.IntegrationTests
{
    public class UserTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly ITestOutputHelper output;

        public UserTests(WebApplicationFactory<Startup> factory, ITestOutputHelper output)
        {
            _factory = factory;
            this.output = output;
        }

        [Theory]
        public async Task SimulateUserLoanActivity()
        {
            /*** Urls to use in test ***/
            string user = "api/v1/users";
            string tape1 = "tapes/1";
            string tape2 = "tapes/2";
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.SendAsync(url, {
                name: "Test user 1",
                email: "testing@users.com",
                phone: "8449919",
                address: "address 1337"
            });

            // Assert
            response.EnsureSuccessStatusCode();
            response
        }
    }
}