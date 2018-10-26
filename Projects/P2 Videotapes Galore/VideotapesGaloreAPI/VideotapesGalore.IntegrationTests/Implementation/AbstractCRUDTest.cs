using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using VideotapesGalore.IntegrationTests;
using VideotapesGalore.IntegrationTests.Interfaces;
using VideotapesGalore.WebApi;
using Xunit;
using Xunit.Abstractions;

namespace VideotapesGalore.IntegrationTests.Implementation
{
    public abstract class AbstractCRUDTest<I, D> : ICRUDTest<I, D>
    {

        /// <summary>
        /// Startup factory for test context
        /// </summary>
        protected readonly WebApplicationFactory<Startup> _factory;

        /// <summary>
        /// Setup test environment (along with seeded content)
        /// </summary>
        protected TestsContextFixture _fixture;

        /// <summary>
        /// HTTP client to use
        /// </summary>
        protected HttpClient client;

        /// <summary>
        /// URL to the list of all resources of given type
        /// </summary>
        protected string _resourceListRoute;

        /// <summary>
        /// base URL to POST or PUT a given resouce
        /// </summary>
        protected string _resourcePostPutRoute;

        /// <summary>
        /// Sample invalid input model for resource to use for testing
        /// </summary>
        protected I _invalidInputModel;

        /// <summary>
        /// Sample valid input model for resource to use for testing
        /// </summary>
        protected I _validInputModel;

        /// <summary>
        /// Sample valid input model for resource to use for testing that's distinct from the other valid input model
        /// </summary>
        protected I _updatedValidInputModel;

        /// <summary>
        /// Initializes abstract CRUD tests
        /// </summary>
        /// <param name="fixture">text context</param>
        /// <param name="invalidInputModel">sample invalid input model for testing</param>
        /// <param name="validInputModel">sample valid input model for testing</param>
        /// <param name="updatedValidInputModel">sample valid input model for testing, distinct from the other valid input model</param>
        /// <param name="ResourceListRoute">Route to list of specified resource</param>
        /// <param name="ResourcePostPutRoute">Route to post specified resource</param>
        public AbstractCRUDTest(TestsContextFixture fixture, I invalidInputModel, I validInputModel, I updatedValidInputModel, string ResourceListRoute, string ResourcePostPutRoute = "")
        {
            _factory = fixture.factory;
            _fixture = fixture;
            this.client = fixture.factory.CreateClient();
            this._invalidInputModel = invalidInputModel;
            this._validInputModel = validInputModel;
            this._updatedValidInputModel = updatedValidInputModel;
            this._resourceListRoute = ResourceListRoute;
            this._resourcePostPutRoute = ResourcePostPutRoute == "" ? ResourceListRoute : ResourcePostPutRoute;
        }

        /// <summary>
        /// Simulates and tests all basic CRUD functionalities for some resource in system
        /// E.g. adding given resource, updating given resource, deleteting given resources and reading given resources accordingly
        /// Also verify that proper errors are returned when user provides any bad or invalid input
        /// </summary>
        [Fact]
        public async Task SimulateCRUDTests()
        {
            /// [GET] get all resources of given type in system and store their total count
            int allResourceCount = await GetCurrentResourceCount();

            /// [POST] attempt to create resource using invalid resource model (type must be either VHS or Betamax)
            /// Expect response to be 412 (precondition failed) to indicate badly formatted input body from user
            var createPreconditionFailedResponse = await PostResource(_invalidInputModel);
            Assert.Equal(HttpStatusCode.PreconditionFailed, createPreconditionFailedResponse.StatusCode);

            /// [POST] create new resource using a valid resource model
            /// Expect response to POST request to be 201 (created) and expect to get location header pointing to new resource, then
            /// [GET] user by the pointer from location header for response to previous POST request and check if user values match
            var createResponse = await PostResource(_validInputModel);
            Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
            var newResourceLocation = createResponse.Headers.Location;
            await AssertGetById(newResourceLocation, _validInputModel);

            /// [GET] get all resources of given type in in system and check that count has increased by one
            // Assert.Equal(allResourceCount+1, await GetCurrentResourceCount());

            // [PUT] attempt to update resource using invalid input model
            // Expect response to PUT request to be 412 (for precondition failed)
            // to indicate badly formatted input body from user
            var editFailResponse = await PutResource(_invalidInputModel);
            Assert.Equal(HttpStatusCode.PreconditionFailed, editFailResponse.StatusCode);

            /// [PUT] update the new resource using a valid resource model
            /// Expect response to be 204 (no content) and then
            /// [GET] resource by id again and check if all values were updated in the put request
            var editResponse = await PutResource(_updatedValidInputModel);
            Assert.Equal(HttpStatusCode.NoContent, editResponse.StatusCode);
            await AssertGetById(newResourceLocation, _updatedValidInputModel);

            // [DELETE] the new resource by id and expect status to be 204 (no content)
            // Attempt to delete again and expect not found error (404)
            // Then lastly re-fetch resource by id that was deleted and expect not found error (404)
            var deleteResponse = await client.DeleteAsync(newResourceLocation);
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
            var deleteFailResponse = await client.DeleteAsync(newResourceLocation);
            Assert.Equal(HttpStatusCode.NotFound, deleteFailResponse.StatusCode);
            await AssertGetByIdNotFound(newResourceLocation);
        }

        /// <summary>
        /// Gets length of the list of all resources of given type in system
        /// </summary>
        /// <returns>count of all resources of given type in system</returns>
        public async Task<int> GetCurrentResourceCount()
        {
            var response = await client.GetAsync(this._resourceListRoute);
            response.EnsureSuccessStatusCode();
            var resources = JsonConvert.DeserializeObject<List<D>>(await response.Content.ReadAsStringAsync());
            return resources.Count;
        }

        /// <summary>
        /// Creates new tape into the system e.g. conducts POST request
        /// Returns response for post request
        /// </summary>
        /// <param name="client">http client to use to issue request to API</param>
        /// <param name="url">url to issue request to</param>
        /// <param name="tapeInput">input model to use to create new tape</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostResource(I inputModel)
        {
            var inputJSON = JsonConvert.SerializeObject(inputModel);
            HttpContent content = new StringContent(inputJSON, Encoding.UTF8, "application/json");
            return await client.PostAsync(this._resourcePostPutRoute, content);
        }

        /// <summary>
        /// Updates existing tape into the system e.g. conducts PUT request
        /// Returns response for put request
        /// </summary>
        /// <param name="client">http client to use to issue request to API</param>
        /// <param name="url">url to issue request to</param>
        /// <param name="tapeInput">input model to use to create new tape</param>
        /// <returns>Response for HTTP request made</returns>
        public async Task<HttpResponseMessage> PutResource(I inputModel)
        {
            var tapeInputJSON = JsonConvert.SerializeObject(inputModel);
            HttpContent content = new StringContent(tapeInputJSON, Encoding.UTF8, "application/json");
            return await client.PutAsync(_resourcePostPutRoute, content);
        }

        /// <summary>
        /// Fetches resource by an id using Location URI (which we get when new resource is created)
        /// Expect to get resource back and verify that a given input resource matches resource that is returned
        /// </summary>
        /// <param name="Location">uri to resource</param>
        /// <param name="inputModel">input model to compare to resource we get back</param>
        public async Task AssertGetById(Uri Location, I inputModel)
        {
            var response = await client.GetAsync(Location);
            response.EnsureSuccessStatusCode();
            D systemResource = JsonConvert.DeserializeObject<D>(await response.Content.ReadAsStringAsync());
            AssertInputModel(systemResource, inputModel);
        }

        /// <summary>
        /// Fetches resource by an id using Location URI (which we get when new resource is created)
        /// Expect to get resource back and verify that a given input resource matches resource that is returned
        /// </summary>
        /// <param name="Location">uri to resource</param>
        /// <param name="inputModel">input model to compare to resource we get back</param>
        protected abstract void AssertInputModel(D dtoModel, I inputModel);

        /// <summary>
        /// Fetches resource by an id using Location URI and expect a 404 (Not Found error)
        /// </summary>
        /// <param name="Location">uri to resource</param>
        public async Task AssertGetByIdNotFound(Uri Location)
        {
            var response = await client.GetAsync(Location);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}