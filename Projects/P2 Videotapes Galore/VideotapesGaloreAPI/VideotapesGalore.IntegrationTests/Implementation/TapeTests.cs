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

namespace VideotapesGalore.IntegrationTests.Implementation
{
    public class TapeTests: AbstractCRUDTest<TapeInputModel, TapeDTO>
    {
        /// <summary>
        /// Sets up environment for CRUD tests
        /// </summary>
        /// <param name="fixture">application context</param>
        /// <param name="output"></param>
        /// <returns></returns>
        public TapeTests(TestsContextFixture fixture) :
            base(fixture, InvalidTapeInput, ValidTapeInput, UpdatedValidTapeInput, "api/v1/tapes") { }

        /// <summary>
        /// Checks if tape resource that has been fetched from API matches input model
        /// </summary>
        /// <param name="dtoModel">Resource from API</param>
        /// <param name="inputModel">Input model resource</param>
        /// <returns></returns>
        protected override void AssertInputModel(TapeDTO dtoModel, TapeInputModel inputModel)
        {
            Assert.Equal(dtoModel.Title, inputModel.Title);
            Assert.Equal(dtoModel.Director, inputModel.Director);
            Assert.Equal(dtoModel.Type, inputModel.Type);
            Assert.Equal(dtoModel.EIDR, inputModel.EIDR);
        }

        /// <summary>
        /// Sample of valid input model for tape
        /// </summary>
        private static TapeInputModel ValidTapeInput = new TapeInputModel() {
            Title = "Mojo Jojo: Greatest Townsville Attacks",
            Director = "Definately Not Mojo Jojo",
            ReleaseDate = DateTime.Now,
            Type = "VHS",
            EIDR = "10.5240/2B3B-1E0E-9314-2C6E-A453-3"
        };

        /// <summary>
        /// Sample of valid input model for tape, distinct from one above
        /// </summary>
        /// <returns></returns>
        private static TapeInputModel UpdatedValidTapeInput = new TapeInputModel() {
            Title = "Mojo Jojo Strikes Townsville... Again!",
            Director = "Bubbles",
            ReleaseDate = DateTime.Now,
            Type = "Betamax",
            EIDR = "10.5240/2B3B-1E0E-9314-2C6E-A453-4"
        };

        /// <summary>
        /// Sample of invalid tape input model (type must be either VHS or Betamax)
        /// </summary>
        /// <returns></returns>
        private static TapeInputModel InvalidTapeInput = new TapeInputModel() {
            Title = "Mojo Jojo Attack on Townsville",
            Director = "Not Mojo Jojo",
            ReleaseDate = DateTime.Now,
            Type = "Blu-Ray",
            EIDR = "10.5240/2B3B-1E0E-9314-2C6E-A453-3"
        };
    }
}