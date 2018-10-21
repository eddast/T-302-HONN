using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace VideotapesGalore.IntegrationTests
{
    public class BasicTests 
    : IClassFixture<WebApplicationFactory<Video>>
    {
        private readonly WebApplicationFactory<RazorPagesProject.Startup> _factory;

        public BasicTests(WebApplicationFactory<RazorPagesProject.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Index")]
        [InlineData("/About")]
        [InlineData("/Privacy")]
        [InlineData("/Contact")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
        }
    }
}
