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
    public class BaseTests 
    : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly ITestOutputHelper output;

        public BaseTests(WebApplicationFactory<Startup> factory, ITestOutputHelper output)
        {
            _factory = factory;
            this.output = output;
        }

        [Theory]
        [InlineData("/api/v1/users")]
        [InlineData("/api/v1/users/2")]
        [InlineData("/api/v1/users/2/tapes")]
        [InlineData("/api/v1/users/2/reviews")]
        [InlineData("/api/v1/tapes")]
        [InlineData("/api/v1/tapes/1")]
        [InlineData("/api/v1/tapes/reviews")]
        [InlineData("api/v1/tapes/1/reviews")]
        public async Task Get_EndpointsReturnSuccess(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            // This ensures that the data received is valid and the endpoints work through and through
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", 
                    response.Content.Headers.ContentType.ToString());
        }
    }
}
