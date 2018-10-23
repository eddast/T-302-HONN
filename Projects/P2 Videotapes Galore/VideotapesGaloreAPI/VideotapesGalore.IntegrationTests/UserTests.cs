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

        [Fact]
        public async Task SimulateUserLoanActivity()
        {
            string user = "api/v1/users";
            var client = _factory.CreateClient();
            
            /// [POST] api/v1/users an invalid user model (name is required and email should be valid email)
            var invalidUserInput = new UserInputModel(){
                Email = "testing",
                Phone = "8449919",
                Address = "address 1337"
            };
            var invalidUserInputJSON = JsonConvert.SerializeObject(invalidUserInput);
            HttpContent invalidContent = new StringContent(invalidUserInputJSON, Encoding.UTF8, "application/json");
            var createPreconditionFailedResponse = await client.PostAsync(user, invalidContent);

            // Expect response to POST request to be 412 (precondition failed) to indicate badly formatted input body from user
            Assert.Equal(HttpStatusCode.PreconditionFailed, createPreconditionFailedResponse.StatusCode);

            /// [POST] api/v1/users a valid user model
            var userInput = new UserInputModel(){
                Name = "Test user 1",
                Email = "testing@users.com",
                Phone = "8449919",
                Address = "address 1337"
            };
            var userInputJSON = JsonConvert.SerializeObject(userInput);
            HttpContent content = new StringContent(userInputJSON, Encoding.UTF8, "application/json");
            var createUserResponse = await client.PostAsync(user, content);

            // Expect response to POST request to be 201 (created) and location header pointing to new resource
            Assert.Equal("", await createUserResponse.Content.ReadAsStringAsync());
            createUserResponse.EnsureSuccessStatusCode();

            /// [GET] user by the pointer from location header for response to previous POST request and check if user values match
            var validateCreateUserResponse = await client.GetAsync(createUserResponse.Headers.Location);
            validateCreateUserResponse.EnsureSuccessStatusCode();
            UserDTO newUser = JsonConvert.DeserializeObject<UserDTO>(await validateCreateUserResponse.Content.ReadAsStringAsync());
            Assert.Equal(newUser.Name,userInput.Name);
            Assert.Equal(newUser.Email,userInput.Email);
            Assert.Equal(newUser.Phone,userInput.Phone);
            Assert.Equal(newUser.Address,userInput.Address);
        }
    }
}