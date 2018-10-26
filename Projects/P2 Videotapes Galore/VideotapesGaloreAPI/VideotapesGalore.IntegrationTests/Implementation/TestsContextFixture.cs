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

namespace VideotapesGalore.IntegrationTests.Implementation
{
    public class TestsContextFixture : IAsyncLifetime
    {
        public WebApplicationFactory<Startup> factory { get; }
        public HttpClient client { get; }
        public List<int> tapeIds { get; set; }
        public List<string> tapeUrls { get; set; }
        public List<int> userIds { get; set; }
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

        public async Task InitializeAsync()
        {
            await InitializeDbForTests();
        }

        public async Task DisposeAsync()
        {
            await RemoveFromDBAfterTests();
        }

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
        }

        public async Task RemoveFromDBAfterTests()
        {
            await deleteFromDb(userUrls);
            await deleteFromDb(tapeUrls);
        }

        private async Task deleteFromDb(List<string> urls)
        {
            foreach (var url in urls)
            {
              await client.DeleteAsync(url);
            }
        }

        public List<UserInputModel> GetSeedingUsers()
        {
          return new List<UserInputModel>()
                {
                    new UserInputModel(){
                        Name = "Eddy",
                        Email = "eddy@genious.com",
                        Address = "Mom's house",
                        Phone = "8451234"
                    },
                    new UserInputModel(){
                        Name = "Mojo Jojo",
                        Email = "major@townsville.org",
                        Address = "Townstreet 3",
                        Phone = "8443511"
                    },
                    new UserInputModel() {
                        Name = "Johnny Bravo",
                        Email = "hey-pretty-mama@hotmale.com",
                        Address = "Street 123",
                        Phone = "8227979"

                    }
                };
        }

        public List<TapeInputModel> GetSeedingTapes()
        {
          return new List<TapeInputModel>()
                {
                    new TapeInputModel(){
                        Title = "Mojo Jojo's Revenge",
                        Director = "Mojo Jojo",
                        ReleaseDate = DateTime.Now,
                        Type = "VHS",
                        EIDR = "10.5240/72B3-2D9E-35E1-6760-83FA-K"
                    },
                    new TapeInputModel(){
                        Title = "Mojo Jojo's Revenge",
                        Director = "Mojo Jojo",
                        ReleaseDate = DateTime.Now,
                        Type = "VHS",
                        EIDR = "10.5240/72B3-2D9E-35E1-6760-83FA-K"
                    },
                    new TapeInputModel() {
                        Title = "Mojo Jojo's Revenge",
                        Director = "Mojo Jojo",
                        ReleaseDate = DateTime.Now,
                        Type = "VHS",
                        EIDR = "10.5240/72B3-2D9E-35E1-6760-83FA-K"
                    }
                };
        }

    }
}