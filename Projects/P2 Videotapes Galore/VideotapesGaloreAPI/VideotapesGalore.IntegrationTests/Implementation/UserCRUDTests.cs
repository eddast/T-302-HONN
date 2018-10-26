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
using VideotapesGalore.IntegrationTests.Context;

namespace VideotapesGalore.IntegrationTests.Implementation
{
    [Collection("Test Context Collection")]
    public class UserTests: AbstractCRUDTest<UserInputModel, UserDTO>
    {
        /// <summary>
        /// Sets up environment for CRUD tests
        /// </summary>
        /// <param name="fixture">application context</param>
        /// <param name="output"></param>
        /// <returns></returns>
        public UserTests(TestsContextFixture fixture) :
            base(fixture, InvalidUserInput, ValidUserInput, UpdatedValidUserInput, "api/v1/users") { }

        /// <summary>
        /// Checks if user resource that has been fetched from API matches input model
        /// </summary>
        /// <param name="dtoModel">Resrouce from API</param>
        /// <param name="inputModel">Input model resource</param>
        /// <returns></returns>
        protected override void AssertInputModel(UserDTO dtoModel, UserInputModel inputModel)
        {
            Assert.Equal(dtoModel.Name, inputModel.Name);
            Assert.Equal(dtoModel.Email, inputModel.Email);
            Assert.Equal(dtoModel.Phone, inputModel.Phone);
            Assert.Equal(dtoModel.Address, inputModel.Address);
        }

        /// <summary>
        /// Sample of valid input model for user
        /// </summary>
        private static UserInputModel ValidUserInput = new UserInputModel() {
            Name = "Mojo Jojo",
            Email = "m_jo@eviloverlords.com",
            Phone = "123 456 789",
            Address = "Townsville"
        };

        /// <summary>
        /// Sample of valid input model for user, distinct from one above
        /// </summary>
        /// <returns></returns>
        private static UserInputModel UpdatedValidUserInput = new UserInputModel(){
            Name = "Venom",
            Email = "mrv@symbiotefreaks.com",
            Phone = "987 456 345",
            Address = "New York"
        };

        /// <summary>
        /// Sample of invalid user input model (email must be valid and name is required)
        /// </summary>
        /// <returns></returns>
        private static UserInputModel InvalidUserInput = new UserInputModel() {
            Email = "Mojo Jojo",
            Phone = "123 456 789",
            Address = "Townsville"
        };
    }
}