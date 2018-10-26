using System.Collections.Generic;
using System.Net;
using System;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using VideotapesGalore.WebApi;
using VideotapesGalore.Models.Entities;
using AutoMapper;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Repositories.DBContext;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace VideotapesGalore.IntegrationTests.Context
{
    /// <summary>
    /// Initializes context for all integration tests that need it
    /// e.g. seeds three known users and three known tapes to use for testing purposes
    /// Then destroyes the resources after integration tests have run
    /// </summary>
    public class TestsContextFixture : IAsyncLifetime
    {
        /// <summary>
        /// Startup factory for test context
        /// </summary>
        public WebApplicationFactory<Startup> factory { get; }
        /// <summary>
        /// HTTP client to use
        /// </summary>
        public HttpClient client { get; }
        /// <summary>
        /// List of ids for seeded (mock) tapes set up before integration tests run
        /// </summary>
        public List<int> tapeIds { get; set; }
        /// <summary>
        /// List of URLs for seeded (mock) tapes location in API set up before integration tests run
        /// </summary>
        public List<string> tapeUrls { get; set; }
        /// <summary>
        /// List of ids for seeded (mock) users set up before integration tests run
        /// </summary>
        public List<int> userIds { get; set; }
        /// <summary>
        /// List of URLs for seeded (mock) users location in API set up before integration tests run
        /// </summary>
        public List<string> userUrls { get; set; }

        public TestsContextFixture()
        {
            this.factory = new WebApplicationFactory<Startup>();
            client = this.factory.CreateClient();
            this.tapeIds = new List<int>();
            this.userIds = new List<int>();
            this.tapeUrls = new List<string>();
            this.userUrls = new List<string>();
        }

        /// <summary>
        /// Setup seed content to datasource before test run
        /// </summary>
        public async Task InitializeAsync() =>
            await InitializeDbForTests();

        /// <summary>
        /// Remove seeded content from datasource when test finish running
        /// </summary>
        public async Task DisposeAsync() =>
            await RemoveFromDBAfterTests();

        /// <summary>
        /// Seeds data source with three mock tapes and three mock users
        /// For integration testing purposes
        /// </summary>
        /// <returns></returns>
        public async Task InitializeDbForTests()
        {
            foreach (var user in GetSeedingUsers())
            {
              var userInputJson = JsonConvert.SerializeObject(user);
              HttpContent content = new StringContent(userInputJson, Encoding.UTF8, "application/json");
              var response = await client.PostAsync("/api/v1/users", content);
              var path = response.Headers.Location.LocalPath;
              userUrls.Add(path);
              userIds.Add(Convert.ToInt32(path.Substring(path.LastIndexOf("/") + 1)));
            }
            foreach (var tape in GetSeedingTapes())
            {
              var tapeInputJson = JsonConvert.SerializeObject(tape);
              HttpContent content = new StringContent(tapeInputJson, Encoding.UTF8, "application/json");
              var response = await client.PostAsync("/api/v1/tapes", content);
              var path = response.Headers.Location.LocalPath;
              tapeUrls.Add(path);
              tapeIds.Add(Convert.ToInt32(path.Substring(path.LastIndexOf("/") + 1)));
            }
            await SeedBorrowRecords();
        }

        /// <summary>
        /// Removes seeded content from data source
        /// </summary>
        public async Task RemoveFromDBAfterTests()
        {
            await deleteFromDb(userUrls);
            await deleteFromDb(tapeUrls);
        }

        /// <summary>
        /// Deletes resources from database given list of URL to resources
        /// </summary>
        /// <param name="urls">list of urls to send DELETE requests to</param>
        private async Task deleteFromDb(List<string> urls)
        {
            foreach (var url in urls)
            {
              await client.DeleteAsync(url);
            }
        }

        /// <summary>
        /// Three random mock users to seed database with for testing purposes
        /// </summary>
        /// <returns>List of user input model to place in data source</returns>
        public List<UserInputModel> GetSeedingUsers()
        {
          return new List<UserInputModel>()
                {
                    new UserInputModel(){
                        Name = "Mojo Jojo",
                        Email = "m_jo@eviloverlords.com",
                        Address = "Townsville",
                        Phone = "5-888-8443"
                    },
                    new UserInputModel(){
                        Name = "Johnny Bravo",
                        Email = "hey-pretty-mama@hotmale.com",
                        Address = "CN Street 113",
                        Phone = "8-000-8888"
                    },
                    new UserInputModel(){
                        Name = "Spider-Man",
                        Email = "p_parker@nyc.com",
                        Address = "New York City",
                        Phone = "1-234-5678"
                    },
                };
        }

        /// <summary>
        /// Three random mock tapes to seed database with for testing purposes
        /// </summary>
        /// <returns>List of tape input model to place in data source</returns>
        public List<TapeInputModel> GetSeedingTapes()
        {
          return new List<TapeInputModel>()
                {
                    new TapeInputModel(){
                        Title = "The Powerpuffgirls Movie",
                        Director = "Craig McCracken",
                        ReleaseDate = new DateTime(2002, 6, 22),
                        Type = "Betamax",
                        EIDR = "10.5240/9C30-DAF8-8A33-570A-1E8E-4"
                    },
                    new TapeInputModel(){
                        Title = "Scooby-Doo and the Goblin King",
                        Director = "Joe Sichta",
                        ReleaseDate = new DateTime(2004, 11, 23),
                        Type = "VHS",
                        EIDR = "10.5240/FB53-A9C3-7E6F-009E-2B08-W"
                    },
                    new TapeInputModel(){
                        Title = "Dexter's Laboratory Ego Trip",
                        Director = "Genndy Tartakovsky",
                        ReleaseDate = new DateTime(1999, 12, 31),
                        Type = "VHS",
                        EIDR = "10.5240/23F7-EDC8-5187-47FD-73D6-N"
                    },
                };
        }

        public async Task SeedBorrowRecords()
        {
            var borrowRecordInputs = GetBorrowRecordInputs();
            await SeedSingleBorrowRecord(userUrls[0] + "/tapes/" + tapeIds[0], borrowRecordInputs[0]);
            await SeedSingleBorrowRecord(userUrls[0] + "/tapes/" + tapeIds[1], borrowRecordInputs[1]);
            await SeedSingleBorrowRecord(userUrls[1] + "/tapes/" + tapeIds[1], borrowRecordInputs[0]);
            await SeedSingleBorrowRecord(userUrls[1] + "/tapes/" + tapeIds[0], borrowRecordInputs[1]);
            await SeedSingleBorrowRecord(userUrls[1] + "/tapes/" + tapeIds[2], borrowRecordInputs[2]);
        }

        private async Task SeedSingleBorrowRecord(string url, BorrowRecordInputModel input)
        {
            var createResponse = await client.PostAsync(url, null);
            await client.PutAsync(url, GetBorrowRecordAsJson(input));
        }

        private HttpContent GetBorrowRecordAsJson(BorrowRecordInputModel input)
        {
            return new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
        }

        private List<BorrowRecordInputModel> GetBorrowRecordInputs()
        {
            return new List<BorrowRecordInputModel>()
            {
                new BorrowRecordInputModel() { BorrowDate = new DateTime(2018,8,10), ReturnDate = new DateTime(2018, 9, 12)},
                new BorrowRecordInputModel() { BorrowDate = new DateTime(2018,9,13), ReturnDate = new DateTime(2018, 10, 10)},
                new BorrowRecordInputModel() { BorrowDate = new DateTime(2018,10,11), ReturnDate = null}
            };
        }
    }
}