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
        private readonly ITestOutputHelper output;

        public UserTests(WebApplicationFactory<Startup> factory, ITestOutputHelper output)
        {
            _factory = factory;
            this.output = output;
        }


        /// <summary>
        /// Tests all basic CRUD functionalities for User resource in system
        /// E.g. adding user resources, updating user resources, reading user resources and deleteting user resources
        /// Also verify that errors are returned on bad input from user
        /// </summary>
        [Fact]
        public async Task TestUserCRUDFunctionalities()
        {
            string userRoute = "api/v1/users";
            var client = _factory.CreateClient();

            /// [GET] get all users in system and store count
            var response = await client.GetAsync(userRoute);
            response.EnsureSuccessStatusCode();
            int allUsersCount = JsonConvert.DeserializeObject<List<UserDTO>>(await response.Content.ReadAsStringAsync()).Count;

            /// [POST] api/v1/users an invalid user model (name is required and email should be valid email)
            /// Expect response to be 412 (precondition failed) to indicate badly formatted input body from user
            var userInput = new UserInputModel(){
                Email = "Mojo Jojo",
                Phone = "123 456 789",
                Address = "Townsville"
            };
            var userInputJSON = JsonConvert.SerializeObject(userInput);
            HttpContent content = new StringContent(userInputJSON, Encoding.UTF8, "application/json");
            var createResponse = await client.PostAsync(userRoute, content);
            Assert.Equal(HttpStatusCode.PreconditionFailed, createResponse.StatusCode);

            /// [POST] api/v1/users a valid user model
            /// Expect response to POST request to be 201 (created) and expect to get location header pointing to new resource, then
            /// [GET] user by the pointer from location header for response to previous POST request and check if user values match
            userInput = new UserInputModel(){
                Name = "Mojo Jojo",
                Email = "m_jo@eviloverlords.com",
                Phone = "123 456 789",
                Address = "Townsville"
            };
            userInputJSON = JsonConvert.SerializeObject(userInput);
            content = new StringContent(userInputJSON, Encoding.UTF8, "application/json");
            createResponse = await client.PostAsync(userRoute, content);
            var newResourceLocation = createResponse.Headers.Location;
            Assert.Equal("", await createResponse.Content.ReadAsStringAsync());
            createResponse.EnsureSuccessStatusCode();
            var validateResponse = await client.GetAsync(newResourceLocation);
            validateResponse.EnsureSuccessStatusCode();
            UserDTO newUser = JsonConvert.DeserializeObject<UserDTO>(await validateResponse.Content.ReadAsStringAsync());
            Assert.Equal(newUser.Name,userInput.Name);
            Assert.Equal(newUser.Email,userInput.Email);
            Assert.Equal(newUser.Phone,userInput.Phone);
            Assert.Equal(newUser.Address,userInput.Address);

            /// [GET] get all users in system and check that count has increased by one
            response = await client.GetAsync(userRoute);
            response.EnsureSuccessStatusCode();
            var allUsersAfterPOST = JsonConvert.DeserializeObject<List<UserDTO>>(await response.Content.ReadAsStringAsync());
            Assert.Equal(allUsersCount+1, allUsersAfterPOST.Count);

            // [PUT] api/v1/users with invalid values (phone is required)
            // Expect response to POST request to be 412 (for precondition failed)
            // to indicate badly formatted input body from user
            userInput = new UserInputModel(){
                Name = "Mojo Jojo",
                Email = "Mojo Jojo",
                Address = "Townsville"
            };
            userInputJSON = JsonConvert.SerializeObject(userInput);
            content = new StringContent(userInputJSON, Encoding.UTF8, "application/json");
            response = await client.PostAsync(userRoute, content);
            Assert.Equal(HttpStatusCode.PreconditionFailed, response.StatusCode);

            /// [PUT] api/v1/users a valid user model
            /// Expect response to be 204 (no content) and then
            /// [GET] user by id again and check if all values were updated in the put request
            userInput = new UserInputModel(){
                Name = "Venom",
                Email = "mrv@symbiotefreaks.com",
                Phone = "987 456 345",
                Address = "New York"
            };
            userInputJSON = JsonConvert.SerializeObject(userInput);
            content = new StringContent(userInputJSON, Encoding.UTF8, "application/json");
            response = await client.PutAsync(newResourceLocation, content);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            validateResponse = await client.GetAsync(newResourceLocation);
            validateResponse.EnsureSuccessStatusCode();
            UserDTO updatedUser = JsonConvert.DeserializeObject<UserDTO>(await validateResponse.Content.ReadAsStringAsync());
            Assert.Equal(updatedUser.Name,userInput.Name);
            Assert.Equal(updatedUser.Email,userInput.Email);
            Assert.Equal(updatedUser.Phone,userInput.Phone);
            Assert.Equal(updatedUser.Address,userInput.Address);

            // [DELETE] new user by id and expect status to be 204 (no content)
            // Attempt to delete again and expect not found error (404)
            // Then lastly re-fetch user by id that was deleted and expect not found error (404)
            response = await client.DeleteAsync(newResourceLocation);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            response = await client.DeleteAsync(newResourceLocation);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            response = await client.GetAsync(newResourceLocation);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}