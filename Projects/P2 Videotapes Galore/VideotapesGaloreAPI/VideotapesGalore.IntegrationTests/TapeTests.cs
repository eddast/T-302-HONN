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

        /// <summary>
        /// Setup web application context as factory
        /// </summary>
        /// <param name="factory">the web application context</param>
        public TapeTests(WebApplicationFactory<Startup> factory) =>
            _factory = factory;

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
            var client = _factory.CreateClient();

            /// [GET] get all tapes in system and store their total count
            int allTapesCount = await GetCurrentTapeCount(client, tapesBaseRoute);

            /// [POST] attempt to create tape using invalid tape model (type must be either VHS or Betamax)
            /// Expect response to be 412 (precondition failed) to indicate badly formatted input body from user
            var tapeInput = new TapeInputModel() {
                Title = "Mojo Jojo Attack on Townsville",
                Director = "Not Mojo Jojo",
                ReleaseDate = DateTime.Now,
                Type = "Blu-Ray",
                EIDR = "10.5240/2B3B-1E0E-9314-2C6E-A453-3"
            };
            var createPreconditionFailedResponse = await PostTape(client, tapesBaseRoute, tapeInput);
            Assert.Equal(HttpStatusCode.PreconditionFailed, createPreconditionFailedResponse.StatusCode);

            /// [POST] create new tape using a valid tape model
            /// Expect response to POST request to be 201 (created) and expect to get location header pointing to new resource, then
            /// [GET] user by the pointer from location header for response to previous POST request and check if user values match
            tapeInput = new TapeInputModel() {
                Title = "Mojo Jojo: Greatest Townsville Attacks",
                Director = "Definately Not Mojo Jojo",
                ReleaseDate = DateTime.Now,
                Type = "VHS",
                EIDR = "10.5240/2B3B-1E0E-9314-2C6E-A453-3"
            };
            var createResponse = await PostTape(client, tapesBaseRoute, tapeInput);
            Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
            var newResourceLocation = createResponse.Headers.Location;
            await AssertGetTapeById(client, newResourceLocation, tapeInput, true);

            /// [GET] get all tapes in system and check that count has increased by one
            Assert.Equal(allTapesCount+1, await GetCurrentTapeCount(client, tapesBaseRoute));

            // [PUT] attempt to update tape using invalid input model (title is required)
            // Expect response to POST request to be 412 (for precondition failed)
            // to indicate badly formatted input body from user
            tapeInput = new TapeInputModel() {
                Director = "Bubbles",
                ReleaseDate = DateTime.Now,
                Type = "VHS",
                EIDR = "10.5240/2B3B-1E0E-9314-2C6E-A453-4"
            };
            var editFailResponse = await PutTape(client, newResourceLocation, tapeInput);
            Assert.Equal(HttpStatusCode.PreconditionFailed, editFailResponse.StatusCode);

            /// [PUT] update tape using a valid tape model
            /// Expect response to be 204 (no content) and then
            /// [GET] tape by id again and check if all values were updated in the put request
            tapeInput = tapeInput = new TapeInputModel() {
                Title = "Mojo Jojo Strikes Townsville... Again!",
                Director = "Bubbles",
                ReleaseDate = DateTime.Now,
                Type = "Betamax",
                EIDR = "10.5240/2B3B-1E0E-9314-2C6E-A453-4"
            };
            var editResponse = await PutTape(client, newResourceLocation, tapeInput);
            Assert.Equal(HttpStatusCode.NoContent, editResponse.StatusCode);
            await AssertGetTapeById(client, newResourceLocation, tapeInput, true);

            // [DELETE] new tape by id and expect status to be 204 (no content)
            // Attempt to delete again and expect not found error (404)
            // Then lastly re-fetch tape by id that was deleted and expect not found error (404)
            var deleteResponse = await client.DeleteAsync(newResourceLocation);
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
            var deleteFailResponse = await client.DeleteAsync(newResourceLocation);
            Assert.Equal(HttpStatusCode.NotFound, deleteFailResponse.StatusCode);
            await AssertGetTapeById(client, newResourceLocation, tapeInput, false);
        }

        /// <summary>
        /// Gets length of the list of all tapes in system
        /// </summary>
        /// <param name="client">http client to use to issue request to API</param>
        /// <param name="url">url to issue request to</param>
        /// <returns>count of all tapes in system</returns>
        private async Task<int> GetCurrentTapeCount(HttpClient client, string url) {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var tapes = JsonConvert.DeserializeObject<List<TapeDTO>>(await response.Content.ReadAsStringAsync());
            return tapes.Count;
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
        /// Updates existing tape into the system e.g. conducts PUT request
        /// Returns response for put request
        /// </summary>
        /// <param name="client">http client to use to issue request to API</param>
        /// <param name="url">url to issue request to</param>
        /// <param name="tapeInput">input model to use to create new tape</param>
        /// <returns>Response for HTTP request made</returns>
        private async Task<HttpResponseMessage> PutTape(HttpClient client, Uri url, TapeInputModel tapeInput)
        {
            var tapeInputJSON = JsonConvert.SerializeObject(tapeInput);
            HttpContent content = new StringContent(tapeInputJSON, Encoding.UTF8, "application/json");
            return await client.PutAsync(url, content);
        }

        /// <summary>
        /// Fetches tape by an id using Location URI (which we get when new tape is created)
        /// Either expect to get tape back and verify that a given tape matches tape that is returned from request if shouldBeInSystem is set to true
        /// Otherwise we assert that we get a not found error for resource
        /// </summary>
        /// <param name="client">http client to use to issue request to API</param>
        /// <param name="Location">uri to resource</param>
        /// <param name="tapeInput">input model to compare to tape we get back</param>
        /// <param name="shouldBeInSystem">true if we expect this resource to be in system, false if we expect 404 error</param>
        /// <returns>Response for HTTP request made</returns>
        private async Task AssertGetTapeById(HttpClient client, Uri Location, TapeInputModel tapeInput, bool shouldBeInSystem)
        {
            var response = await client.GetAsync(Location);
            if(shouldBeInSystem) {
                response.EnsureSuccessStatusCode();
                TapeDTO newTape = JsonConvert.DeserializeObject<TapeDTO>(await response.Content.ReadAsStringAsync());
                Assert.Equal(newTape.Title, tapeInput.Title);
                Assert.Equal(newTape.Director, tapeInput.Director);
                Assert.Equal(newTape.Type, tapeInput.Type);
                Assert.Equal(newTape.EIDR, tapeInput.EIDR);
            } else {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}