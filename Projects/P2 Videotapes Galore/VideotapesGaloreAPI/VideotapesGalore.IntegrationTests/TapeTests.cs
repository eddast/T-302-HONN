using System.Collections.Generic;
using System.Net;
using System;
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
        public async Task SimulateTapeCRUD()
        {
            /*** Urls to use in test ***/
            string tapes = "api/v1/tapes";


            var client = _factory.CreateClient();

            int previousLength = await GetCurrentTapesLength(client, tapes);
            var currentDate = DateTime.Now;
            /// [POST] api/v1/tape an invalid user model (name is required and email should be valid email)
            var invalidTapeInput = new TapeInputModel(){
                Title = "Mojo Jojo Attack on Townsville",
                ReleaseDate = currentDate,
                Type = "VHS",
                EIDR = "10.5240/2B3B-1E0E-9314-2C6E-A453-3"
            };

            var createPreconditionFailedResponse = await PostTape(client, tapes, invalidTapeInput);
            Assert.Equal(HttpStatusCode.PreconditionFailed, createPreconditionFailedResponse.StatusCode);

            /// [POST] api/v1/tape a valid user model (name is required and email should be valid email)
            var tapeInput = new TapeInputModel(){
                Title = "Mojo Jojo Attack on Townsville",
                Director = "Not Mojo Jojo",
                ReleaseDate = currentDate,
                Type = "VHS",
                EIDR = "10.5240/2B3B-1E0E-9314-2C6E-A453-3"
            };

            var createTapeResponse = await PostTape(client, tapes, tapeInput);

            // Expect response to POST request to be 201 (created) and location header pointing to new resource
            Assert.Equal("", await createTapeResponse.Content.ReadAsStringAsync());
            createTapeResponse.EnsureSuccessStatusCode();

            int afterCreateLength = await GetCurrentTapesLength(client, tapes);
            Assert.Equal(previousLength + 1, afterCreateLength);

            /// [GET] user by the pointer from location header for response to previous POST request and check if user values match
            await AssertGetTapeById(client, createTapeResponse.Headers.Location, tapeInput);
        }

        private async Task<int> GetCurrentTapesLength(HttpClient client, string url) {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            List<TapeDTO> tapes = JsonConvert.DeserializeObject<List<TapeDTO>>(await response.Content.ReadAsStringAsync());
            return tapes.Count;
        }

        private async Task AssertGetTapeById(HttpClient client, Uri Location, TapeInputModel tapeInput)
        {
            var validateCreateTapeResponse = await client.GetAsync(Location);
            validateCreateTapeResponse.EnsureSuccessStatusCode();
            TapeDTO newUser = JsonConvert.DeserializeObject<TapeDTO>(await validateCreateTapeResponse.Content.ReadAsStringAsync());
            Assert.Equal(newUser.Title, tapeInput.Title);
            Assert.Equal(newUser.Director, tapeInput.Director);
            Assert.Equal(newUser.Type, tapeInput.Type);
            Assert.Equal(newUser.EIDR, tapeInput.EIDR);
        }

        private async Task<HttpResponseMessage> PostTape(HttpClient client, string url, TapeInputModel tapeInput)
        {
            var tapeInputJSON = JsonConvert.SerializeObject(tapeInput);
            HttpContent content = new StringContent(tapeInputJSON, Encoding.UTF8, "application/json");
            var createTapeResponse = await client.PostAsync(url, content);
            return createTapeResponse;

        }
    }
}