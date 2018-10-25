using System.Collections.Generic;
using System.Net;
using System;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using VideotapesGalore.WebApi;

namespace VideotapesGalore.IntegrationTests
{
    public class TestsContextFixture : IClassFixture<WebApplicationFactory<Startup>>, IDisposable
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public List<int> tapeIds { get; set; }
        public List<int> userIds { get; set; }
        public TestsContextFixture(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            this.tapeIds = new List<int>();
            this.userIds = new List<int>();
        }
        public void Dispose()
        {
            // ... clean up test data from the database ...
        }
        public void InitializeDbForTests(VideotapesGaloreDBContext db)
        {
            db.Users.AddRange(GetSeedingUsers());
            db.Tapes.AddRange(GetSeedingTapes());
            db.SaveChanges();
            userIds = db.Users.ToList().OrderByDescending(u => u.CreatedAt).Select(u => u.Id).Take(3).ToList();
            tapeIds = db.Tapes.ToList().OrderByDescending(t => t.CreatedAt).Select(t => t.Id).Take(3).ToList();
        }

        public void RemoveFromDBAfterTests(VideotapesGaloreDBContext db) {
            foreach(var userId in userIds) {
                db.Users.Remove(db.Users.FirstOrDefault(user => user.Id == userId));
            }
            foreach(var tapeId in tapeIds) {
                db.Tapes.Remove(db.Tapes.FirstOrDefault(user => user.Id == tapeId));
            }
            db.SaveChanges();
        }

        public List<User> GetSeedingUsers()
        {
            return new List<User>()
            {
                Mapper.Map<User>(new UserInputModel(){ 
                    Name = "Eddy",
                    Email = "eddy@genious.com",
                    Address = "Mom's house",
                    Phone = "8451234"
                }),
                Mapper.Map<User>(new UserInputModel(){
                    Name = "Mojo Jojo",
                    Email = "major@townsville.org",
                    Address = "Townstreet 3",
                    Phone = "8443511"
                 }),
                Mapper.Map<User>(new UserInputModel() { 
                    Name = "Johnny Bravo",
                    Email = "hey-pretty-mama@hotmale.com",
                    Address = "Street 123",
                    Phone = "8227979"

                })
            };
        }

        public List<Tape> GetSeedingTapes()
        {
            return new List<Tape>()
            {
                Mapper.Map<Tape>(new TapeInputModel(){ 
                    Title = "Mojo Jojo's Revenge",
                    Director = "Mojo Jojo",
                    ReleaseDate = DateTime.Now,
                    Type = "VHS",
                    EIDR = "10.5240/72B3-2D9E-35E1-6760-83FA-K"
                }),
                Mapper.Map<Tape>(new TapeInputModel(){
                    Title = "Mojo Jojo's Revenge",
                    Director = "Mojo Jojo",
                    ReleaseDate = DateTime.Now,
                    Type = "VHS",
                    EIDR = "10.5240/72B3-2D9E-35E1-6760-83FA-K"
                 }),
                Mapper.Map<Tape>(new TapeInputModel() { 
                    Title = "Mojo Jojo's Revenge",
                    Director = "Mojo Jojo",
                    ReleaseDate = DateTime.Now,
                    Type = "VHS",
                    EIDR = "10.5240/72B3-2D9E-35E1-6760-83FA-K"

                })
            };
        }
    }
}
