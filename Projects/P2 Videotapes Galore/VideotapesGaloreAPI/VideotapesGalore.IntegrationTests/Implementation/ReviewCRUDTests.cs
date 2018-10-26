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
    public class ReviewTests : AbstractCRUDTest<ReviewInputModel, ReviewDTO>
    {
        /// <summary>
        /// Sets up environment for CRUD tests
        /// </summary>
        /// <param name="fixture">application context</param>
        /// <param name="output"></param>
        /// <returns></returns>
        public ReviewTests(TestsContextFixture fixture) :
            base(fixture, InvalidReviewInput, ValidReviewInput, UpdatedValidReviewInput, "api/v1/tapes/reviews", getReviewPostPath(fixture)) { }

        /// <summary>
        /// Checks if review resource that has been fetched from API matches input model
        /// </summary>
        /// <param name="dtoModel">Resource from API</param>
        /// <param name="inputModel">Input model resource</param>
        /// <returns></returns>
        protected override void AssertInputModel(ReviewDTO dtoModel, ReviewInputModel inputModel) =>
            Assert.Equal(dtoModel.Rating, inputModel.Rating);

        /// <summary>
        /// Sample of valid input model for review
        /// </summary>
        private static ReviewInputModel ValidReviewInput = new ReviewInputModel() { Rating = 3 };

        /// <summary>
        /// Sample of valid input model for tape, distinct from one above
        /// </summary>
        /// <returns></returns>
        private static ReviewInputModel UpdatedValidReviewInput = new ReviewInputModel() { Rating = 5 };

        /// <summary>
        /// Sample of invalid tape input model (type must be either VHS or Betamax)
        /// </summary>
        /// <returns></returns>
        private static ReviewInputModel InvalidReviewInput = new ReviewInputModel() { Rating = 6 };

        /// <summary>
        /// Gets the path to return the URL in api to post a review for given user
        /// </summary>
        /// <returns>The path to post new review for user in system</returns>
        private static string getReviewPostPath(TestsContextFixture fixture) =>
            fixture.userUrls[0] + "/reviews/" + fixture.tapeIds[0];
    }
}