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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VideotapesGalore.Models.DTOs;
using AutoMapper;

namespace VideotapesGalore.IntegrationTests.Implementation
{
    [Collection("Test Context Collection")]
    public class ReviewTest 
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private TestsContextFixture _fixture;
        private ITestOutputHelper output;

        /// <summary>
        /// Setup web application context as factory
        /// </summary>
        /// <param name="factory">the web application context</param>
        public ReviewTest(TestsContextFixture fixture, ITestOutputHelper output) {

            _factory = fixture.factory;
            _fixture = fixture;
            this.output = output;
        }


        /// <summary>
        /// Simulates and tests all basic CRUD functionalities for Review resource in system
        /// E.g. adding review resources, updating review resources, deleteting review resources and reading review resources accordingly 
        /// Also verify that proper errors are returned when user provides any bad or invalid input
        /// </summary>
        [Fact]
        public async Task SimulateReviewCRUD()
        {
            // Base URL to review resources
            string allReviewsRoute = "api/v1/tapes/reviews";
            var client = _factory.CreateClient();

            /***************** TODO EYÐA ÚT *****************/
            var userInput = new UserInputModel(){
                Name = "Venom",
                Email = "mrv@symbiotefreaks.com",
                Phone = "987 456 345",
                Address = "New York"
            };
            var userInputJSON = JsonConvert.SerializeObject(userInput);
            HttpContent usercontent = new StringContent(userInputJSON, Encoding.UTF8, "application/json");
            var newUserRespone = await client.PostAsync("api/v1/users", usercontent);
            var newUserLocation = newUserRespone.Headers.Location;

            var tapeInput = new TapeInputModel() {
                Title = "Mojo Jojo: Greatest Townsville Attacks",
                Director = "Definately Not Mojo Jojo",
                ReleaseDate = DateTime.Now,
                Type = "VHS",
                EIDR = "10.5240/2B3B-1E0E-9314-2C6E-A453-3"
            };
            var tapeInputJSON = JsonConvert.SerializeObject(tapeInput);
            HttpContent tapecontent = new StringContent(tapeInputJSON, Encoding.UTF8, "application/json");
            var newTapeRespone = await client.PostAsync("api/v1/tapes", tapecontent);
            var newTapeLocation = newTapeRespone.Headers.Location;
            /************************************************/

            /// [GET] get all reviews in system and store count
            int allReviewsCount = await GetCurrentReviewsCount(client, allReviewsRoute);

            /// [POST] attempt to create review using invalid review model (reveiw must range between 1-5)
            /// Expect response to be 412 (precondition failed) to indicate badly formatted input body from user
            var reviewInput = new ReviewInputModel(){ Rating = 0 };
            var reviewPath = GetReviewPath( newUserLocation.LocalPath, newTapeLocation.LocalPath);
            var createFailResponse = await PostReview(client, reviewPath, reviewInput);
            Assert.Equal(HttpStatusCode.PreconditionFailed, createFailResponse.StatusCode);

            /// [POST] create new user using a valid user model
            /// Expect response to POST request to be 201 (created) and expect to get location header pointing to new resource, then
            /// [GET] user by the pointer from location header for response to previous POST request and check if user values match
            reviewInput = new ReviewInputModel(){ Rating = 3 };
            var createResponse = await PostReview(client, reviewPath, reviewInput);
            Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
            var newResourceLocation = createResponse.Headers.Location;
            await AssertGetReview(client, newResourceLocation, reviewInput, true);

            /// [GET] get all reviews in system and check that count has increased by one
            Assert.Equal(allReviewsCount+1, await GetCurrentReviewsCount(client, allReviewsRoute));

            // [PUT] attempt to update review using invalid input model (rating ranges from 1-5)
            // Expect response to POST request to be 412 (for precondition failed)
            // to indicate badly formatted input body from user
            reviewInput = new ReviewInputModel(){ Rating = 6 };
            var editFailResponse = await PutReview(client, newResourceLocation, reviewInput);
            Assert.Equal(HttpStatusCode.PreconditionFailed, editFailResponse.StatusCode);

            /// [PUT] update review using a valid user model
            /// Expect response to be 204 (no content) and then
            /// [GET] review by id again and check if all values were updated in the put request
            reviewInput = new ReviewInputModel(){ Rating = 5 };
            var editResponse = await PutReview(client, newResourceLocation, reviewInput);
            Assert.Equal(HttpStatusCode.NoContent, editResponse.StatusCode);
            await AssertGetReview(client, newResourceLocation, reviewInput, true);

            // [DELETE] new user by id and expect status to be 204 (no content)
            // Attempt to delete again and expect not found error (404)
            // Then lastly re-fetch user by id that was deleted and expect not found error (404)
            var deleteResponse = await client.DeleteAsync(newResourceLocation);
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
            var deleteFailResponse = await client.DeleteAsync(newResourceLocation);
            Assert.Equal(HttpStatusCode.NotFound, deleteFailResponse.StatusCode);
            await AssertGetReview(client, newResourceLocation, reviewInput, false);
        }

        /// <summary>
        /// Gets length of the list of all users in system
        /// </summary>
        /// <param name="client">http client to use to issue request to API</param>
        /// <param name="url">url to issue request to</param>
        /// <returns>count of all users in system</returns>
        private async Task<int> GetCurrentReviewsCount(HttpClient client, string url) {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var reviews = JsonConvert.DeserializeObject<List<ReviewDTO>>(await response.Content.ReadAsStringAsync());
            return reviews.Count;
        }

        /// <summary>
        /// Creates new review into the system e.g. conducts POST request
        /// Returns response for post request
        /// </summary>
        /// <param name="client">http client to use to issue request to API</param>
        /// <param name="url">url to issue request to</param>
        /// <param name="reviewInput">input model to use to create new review</param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> PostReview(HttpClient client, string url, ReviewInputModel reviewInput)
        {
            var reviewInputJSON = JsonConvert.SerializeObject(reviewInput);
            HttpContent content = new StringContent(reviewInputJSON, Encoding.UTF8, "application/json");
            return await client.PostAsync(url, content);
        }

        /// <summary>
        /// Updates existing review into the system e.g. conducts PUT request
        /// Returns response for put request
        /// </summary>
        /// <param name="client">http client to use to issue request to API</param>
        /// <param name="url">url to issue request to</param>
        /// <param name="reviewInput">input model to use to create new review</param>
        /// <returns>Response for HTTP request made</returns>
        private async Task<HttpResponseMessage> PutReview(HttpClient client, Uri url, ReviewInputModel reviewInput)
        {
            var reviewInputJSON = JsonConvert.SerializeObject(reviewInput);
            HttpContent content = new StringContent(reviewInputJSON, Encoding.UTF8, "application/json");
            return await client.PutAsync(url, content);
        }

        /// <summary>
        /// Fetches user by an id using Location URI (which we get when new review is created)
        /// Either expect to get review back and verify that a given review matches user that is returned from request if shouldBeInSystem is set to true
        /// Otherwise we assert that we get a not found error for resource
        /// </summary>
        /// <param name="client">http client to use to issue request to API</param>
        /// <param name="Location">uri to resource</param>
        /// <param name="reviewInput">input model to compare to review we get back</param>
        /// <param name="shouldBeInSystem">true if we expect this resource to be in system, false if we expect 404 error</param>
        /// <returns>Response for HTTP request made</returns>
        private async Task AssertGetReview(HttpClient client, Uri Location, ReviewInputModel reviewInput, bool shouldBeInSystem)
        {
            var response = await client.GetAsync(Location);
            if(shouldBeInSystem) {
                response.EnsureSuccessStatusCode();
                ReviewDTO newReview = JsonConvert.DeserializeObject<ReviewDTO>(await response.Content.ReadAsStringAsync());
                Assert.Equal(newReview.Rating, reviewInput.Rating);
            } else {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        private string GetReviewPath(string userpath, string tapepath)
        {
            return (userpath + "/reviews" + tapepath.Substring(tapepath.LastIndexOf("/")));
        }
    }
}