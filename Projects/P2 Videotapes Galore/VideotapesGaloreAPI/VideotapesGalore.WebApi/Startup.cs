using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.Entities;
using VideotapesGalore.Models.Entities.Entities;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Repositories.Implementation;
using VideotapesGalore.Repositories.Interfaces;
using VideotapesGalore.Services.Implementation;
using VideotapesGalore.Services.Implementations;
using VideotapesGalore.Services.Interfaces;
using VideotapesGalore.WebApi.Extensions;

namespace VideotapesGalore.WebApi
{
    /// <summary>
    /// Setup WebApi project
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Sets up configurations
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Returns configuration
        /// </summary>
        /// <value>configuration</value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Setup Swagger for API documentation of the system
            // Uses the in-code XML comments and MVC tags to generate understandable description for routes and models
            // Documentation available when server is running at <host>/api-documentation
            services.AddSwaggerGen(opt => {
                opt.SwaggerDoc("v1", new Info {
                    Version = "v1",
                    Title = "Videotapes Galore API",
                    Description = "Management system for renting video tapes to users with review functionality and recommendation service",
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Provide MySQL connection prerequisite (connection string) to concrete repositories
            // services.Add(new ServiceDescriptor(typeof(TapeRepository), new TapeRepository(Configuration.GetConnectionString("DefaultConnection"))));
            Console.WriteLine(Configuration);
            // services.AddTransient<TapeRepository>(_ => new TapeRepository(Configuration["ConnectionStrings:DefaultConnection"]));

            // Set up API-specific dependency injections for services and repositories
            services.AddSingleton<ILogService, LogService>();
            services.AddTransient<ITapeRepository, TapeRepository>();
            services.AddTransient<ITapeService, TapeService>();
        }
        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            // Enable Swagger for API documentation of the system
            // Documentation available when server is running at <host>/api-documentation
            app.UseSwagger();
            app.UseSwaggerUI(opt =>{
                opt.RoutePrefix = "api-documentation";
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Videotapes Galore API");
            });

            // Sets up global exception handling and exception logging
            // On exceptions, an automatic HTTP response to client with error message
            // Handles the custom exceptions that yield HTTP responses of
            //      404s (ResourceNotFoundException),
            //      400s (ParameterFormatException) and
            //      412s (InputFormatException) and
            //      500 (Default for any other exception)
            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();
            app.UseMvc();

            // Create support for automatic mapping of models in system
            AutoMapper.Mapper.Initialize(cfg => {
                // Map entities to DTOs
                cfg.CreateMap<Tape, TapeDTO>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<Review, ReviewDTO>();
                cfg.CreateMap<BorrowRecord, BorrowRecordDTO>();
                // Map DTOs to entities
                cfg.CreateMap<TapeDTO, Tape>();
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<ReviewDTO, Review>();
                cfg.CreateMap<BorrowRecordDTO, BorrowRecord>();
                // Map input models to entities
                cfg.CreateMap<TapeInputModel, Tape>()
                    .ForMember(m => m.CreatedDate, opt => opt.UseValue(DateTime.Now))
                    .ForMember(m => m.LastModified, opt => opt.UseValue(DateTime.Now));
                cfg.CreateMap<UserInputModel, User>()
                    .ForMember(m => m.CreatedDate, opt => opt.UseValue(DateTime.Now))
                    .ForMember(m => m.LastModified, opt => opt.UseValue(DateTime.Now));
                cfg.CreateMap<ReviewInputModel, Review>()
                    .ForMember(m => m.CreatedDate, opt => opt.UseValue(DateTime.Now))
                    .ForMember(m => m.LastModified, opt => opt.UseValue(DateTime.Now));
                cfg.CreateMap<BorrowRecordInputModel, BorrowRecord>()
                    .ForMember(m => m.CreatedDate, opt => opt.UseValue(DateTime.Now))
                    .ForMember(m => m.LastModified, opt => opt.UseValue(DateTime.Now));
            });
        }
    }
}
