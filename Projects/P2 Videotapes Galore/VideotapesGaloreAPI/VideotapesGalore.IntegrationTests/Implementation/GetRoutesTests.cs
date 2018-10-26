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
using System.Net.Http;

namespace VideotapesGalore.IntegrationTests.Implementation
{
    public class GetRoutesTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        /// <summary>
        /// HTTP client to use
        /// </summary>
        private HttpClient client;

        public GetRoutesTests(WebApplicationFactory<Startup> factory) =>
            client = factory.CreateClient();


        /// <summary>
        /// Check if all safe requests return 200 (OK) status code and application/json content
        /// (Safe routes are all GET routes in system e.g. routes that do not modify any system data)
        /// </summary>
        [Theory]
        [InlineData("/api/v1/users")]
        [InlineData("/api/v1/tapes")]
        [InlineData("/api/v1/tapes/reviews")]
        public async Task Get_EndpointsReturnSuccess(string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
