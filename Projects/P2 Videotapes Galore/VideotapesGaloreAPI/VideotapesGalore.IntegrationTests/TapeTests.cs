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
    public class TapeTests: IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly ITestOutputHelper output;

        public TapeTests(WebApplicationFactory<Startup> factory, ITestOutputHelper output)
        {
            _factory = factory;
            this.output = output;
        }


        [Fact]
        public async Task SimulateCreateNewTape()
        {
            /*** Urls to use in test ***/
            string user = "api/v1/tapes";

            // Arrange
            var client = _factory.CreateClient();

            // Act

            // Assert
        }

        [Fact]
        public async Task SimulateReviewTape()
        {
            /*** Urls to use in test ***/
            string user = "api/v1/tapes";

            // Arrange
            var client = _factory.CreateClient();

            // Act

            // Assert
        }
    }
}