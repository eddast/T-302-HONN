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

namespace VideotapesGalore.IntegrationTests.Implementation
{
    public class GetRoutesTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public GetRoutesTests(WebApplicationFactory<Startup> factory) =>
            _factory = factory;


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
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
