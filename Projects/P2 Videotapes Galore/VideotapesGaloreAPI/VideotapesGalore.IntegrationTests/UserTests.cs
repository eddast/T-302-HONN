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

namespace VideotapesGalore.IntegrationTests
{
    public class UserTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        /// <summary>
        /// Setup web application context as factory
        /// </summary>
        /// <param name="factory">the web application context</param>
        public UserTests(WebApplicationFactory<Startup> factory) =>
            _factory = factory;


        /// <summary>
        /// Simulates and tests all basic CRUD functionalities for User resource in system
        /// E.g. adding user resources, updating user resources, deleteting user resources and reading user resources accordingly 
        /// Also verify that proper errors are returned when user provides any bad or invalid input
        /// </summary>
        [Fact]
        public async Task SimulateUserCRUD()
        {
            // Base URL to tape resources
            string userBaseRoute = "api/v1/users";
            var client = _factory.CreateClient();

            /// [GET] get all users in system and store count
            int allUsersCount = await GetCurrentUserCount(client, userBaseRoute);

            /// [POST] attempt to create user using invalid user model (name is required and email should be valid email)
            /// Expect response to be 412 (precondition failed) to indicate badly formatted input body from user
            var userInput = new UserInputModel(){
                Email = "Mojo Jojo",
                Phone = "123 456 789",
                Address = "Townsville"
            };
            var createFailResponse = await PostUser(client, userBaseRoute, userInput);
            Assert.Equal(HttpStatusCode.PreconditionFailed, createFailResponse.StatusCode);

            /// [POST] create new user using a valid user model
            /// Expect response to POST request to be 201 (created) and expect to get location header pointing to new resource, then
            /// [GET] user by the pointer from location header for response to previous POST request and check if user values match
            userInput = new UserInputModel(){
                Name = "Mojo Jojo",
                Email = "m_jo@eviloverlords.com",
                Phone = "123 456 789",
                Address = "Townsville"
            };
            var createResponse = await PostUser(client, userBaseRoute, userInput);
            Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
            var newResourceLocation = createResponse.Headers.Location;
            await AssertGetUserById(client, newResourceLocation, userInput, true);

            /// [GET] get all users in system and check that count has increased by one
            // Assert.Equal(allUsersCount+1, await GetCurrentUserCount(client, userBaseRoute));

            // [PUT] attempt to update user using invalid input model (phone is required)
            // Expect response to POST request to be 412 (for precondition failed)
            // to indicate badly formatted input body from user
            userInput = new UserInputModel(){
                Name = "Mojo Jojo",
                Email = "Mojo Jojo",
                Address = "Townsville"
            };
            var editFailResponse = await PutUser(client, newResourceLocation, userInput);
            Assert.Equal(HttpStatusCode.PreconditionFailed, editFailResponse.StatusCode);

            /// [PUT] update user using a valid user model
            /// Expect response to be 204 (no content) and then
            /// [GET] user by id again and check if all values were updated in the put request
            userInput = new UserInputModel(){
                Name = "Venom",
                Email = "mrv@symbiotefreaks.com",
                Phone = "987 456 345",
                Address = "New York"
            };
            var editResponse = await PutUser(client, newResourceLocation, userInput);
            Assert.Equal(HttpStatusCode.NoContent, editResponse.StatusCode);
            await AssertGetUserById(client, newResourceLocation, userInput, true);
            
            // [DELETE] new user by id and expect status to be 204 (no content)
            // Attempt to delete again and expect not found error (404)
            // Then lastly re-fetch user by id that was deleted and expect not found error (404)
            var deleteResponse = await client.DeleteAsync(newResourceLocation);
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
            var deleteFailResponse = await client.DeleteAsync(newResourceLocation);
            Assert.Equal(HttpStatusCode.NotFound, deleteFailResponse.StatusCode);
            await AssertGetUserById(client, newResourceLocation, userInput, false);
        }

        /// <summary>
        /// Gets length of the list of all users in system
        /// </summary>
        /// <param name="client">http client to use to issue request to API</param>
        /// <param name="url">url to issue request to</param>
        /// <returns>count of all users in system</returns>
        private async Task<int> GetCurrentUserCount(HttpClient client, string url) {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var users = JsonConvert.DeserializeObject<List<UserDTO>>(await response.Content.ReadAsStringAsync());
            return users.Count;
        }

        /// <summary>
        /// Creates new user into the system e.g. conducts POST request
        /// Returns response for post request
        /// </summary>
        /// <param name="client">http client to use to issue request to API</param>
        /// <param name="url">url to issue request to</param>
        /// <param name="tapeInput">input model to use to create new tape</param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> PostUser(HttpClient client, string url, UserInputModel userInput)
        {
            var userInputJSON = JsonConvert.SerializeObject(userInput);
            HttpContent content = new StringContent(userInputJSON, Encoding.UTF8, "application/json");
            return await client.PostAsync(url, content);
        }

        /// <summary>
        /// Updates existing user into the system e.g. conducts PUT request
        /// Returns response for put request
        /// </summary>
        /// <param name="client">http client to use to issue request to API</param>
        /// <param name="url">url to issue request to</param>
        /// <param name="userInput">input model to use to create new user</param>
        /// <returns>Response for HTTP request made</returns>
        private async Task<HttpResponseMessage> PutUser(HttpClient client, Uri url, UserInputModel userInput)
        {
            var userInputJSON = JsonConvert.SerializeObject(userInput);
            HttpContent content = new StringContent(userInputJSON, Encoding.UTF8, "application/json");
            return await client.PutAsync(url, content);
        }

        /// <summary>
        /// Fetches user by an id using Location URI (which we get when new user is created)
        /// Either expect to get user back and verify that a given user matches user that is returned from request if shouldBeInSystem is set to true
        /// Otherwise we assert that we get a not found error for resource
        /// </summary>
        /// <param name="client">http client to use to issue request to API</param>
        /// <param name="Location">uri to resource</param>
        /// <param name="userInput">input model to compare to user we get back</param>
        /// <param name="shouldBeInSystem">true if we expect this resource to be in system, false if we expect 404 error</param>
        /// <returns>Response for HTTP request made</returns>
        private async Task AssertGetUserById(HttpClient client, Uri Location, UserInputModel userInput, bool shouldBeInSystem)
        {
            var response = await client.GetAsync(Location);
            if(shouldBeInSystem) {
                response.EnsureSuccessStatusCode();
                UserDTO newUser = JsonConvert.DeserializeObject<UserDTO>(await response.Content.ReadAsStringAsync());
                Assert.Equal(newUser.Name, userInput.Name);
                Assert.Equal(newUser.Email, userInput.Email);
                Assert.Equal(newUser.Phone, userInput.Phone);
                Assert.Equal(newUser.Address, userInput.Address);
            } else {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}