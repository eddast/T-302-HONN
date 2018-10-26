using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using VideotapesGalore.WebApi;
using Xunit;

namespace VideotapesGalore.IntegrationTests.Interfaces
{
    /// <summary>
    /// Interface for crud tests functionality
    /// </summary>
    /// <typeparam name="I">Type of input model for resource</typeparam>
    /// <typeparam name="D">Type of DTO model for resource</typeparam>
    public interface ICRUDTest<I, D> : IClassFixture<WebApplicationFactory<Startup>>
    {
        /// <summary>
        /// Simulates and tests all basic CRUD functionalities for some resource in system
        /// E.g. adding given resource, updating given resource, deleteting given resources and reading given resources accordingly
        /// Also verify that proper errors are returned when user provides any bad or invalid input
        /// </summary>
        Task SimulateCRUDTests();

        /// <summary>
        /// Gets length of the list of all resources of given type in system
        /// </summary>
        /// <returns>count of all resources of given type in system</returns>
        Task<int> GetCurrentResourceCount();

        /// <summary>
        /// Creates new tape into the system e.g. conducts POST request
        /// Returns response for post request
        /// </summary>
        /// <param name="client">http client to use to issue request to API</param>
        /// <param name="url">url to issue request to</param>
        /// <param name="tapeInput">input model to use to create new tape</param>
        /// <returns></returns>
        Task<HttpResponseMessage> PostResource(I inputModel);

        /// <summary>
        /// Updates existing tape into the system e.g. conducts PUT request
        /// Returns response for put request
        /// </summary>
        /// <param name="client">http client to use to issue request to API</param>
        /// <param name="url">url to issue request to</param>
        /// <param name="tapeInput">input model to use to create new tape</param>
        /// <returns>Response for HTTP request made</returns>
        Task<HttpResponseMessage> PutResource(I inputModel);

        /// <summary>
        /// Fetches resource by an id using Location URI (which we get when new resource is created)
        /// Expect to get resource back and verify that a given input resource matches resource that is returned
        /// </summary>
        /// <param name="Location">uri to resource</param>
        /// <param name="inputModel">input model to compare to resource we get back</param>
        Task AssertGetById(Uri Location, I inputModel);

        /// <summary>
        /// Fetches resource by an id using Location URI and expect a 404 (Not Found error)
        /// </summary>
        /// <param name="Location">uri to resource</param>
        Task AssertGetByIdNotFound(Uri Location);
    }
}