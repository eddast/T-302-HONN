// using System;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Mvc.Testing;
// using VideotapesGalore.IntegrationTests;
// using VideotapesGalore.Repositories.DBContext;
// using VideotapesGalore.WebApi;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.EntityFrameworkCore;

// public class CustomWebApplicationFactory<Startup> 
//     : WebApplicationFactory<Startup>
// {
//     protected override void ConfigureWebHost(IWebHostBuilder builder)
//     {
//         builder.UseStartup("VideotapesGalore.WebApi");
//         /*builder.ConfigureServices(services =>
//         {
//             // Create a new service provider.
//             var serviceProvider = new ServiceCollection()
//                 .AddEntityFrameworkInMemoryDatabase()
//                 .BuildServiceProvider();

//             // Add a database context (ApplicationDbContext) using an in-memory 
//             // database for testing.
//             services.AddDbContext<VideotapesGaloreDBContext>(options => 
//             {
//                 options.UseInMemoryDatabase("InMemoryDbForTesting");
//                 options.UseInternalServiceProvider(serviceProvider);
//             });

//             // Build the service provider.
//             var sp = services.BuildServiceProvider();

//             // Create a scope to obtain a reference to the database
//             // context (ApplicationDbContext).
//             using (var scope = sp.CreateScope())
//             {
//                 var scopedServices = scope.ServiceProvider;
//                 var db = scopedServices.GetRequiredService<VideotapesGaloreDBContext>();
//                 //var logger = scopedServices
//                 //    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

//                 // Ensure the database is created.
//                 db.Database.EnsureCreated();

//                 // Seed the database with test data.
//                 TestContextMaintenance tcm = new TestContextMaintenance();
//                 tcm.InitializeDbForTests(db);
//             }
//         });*/
//     }
// }