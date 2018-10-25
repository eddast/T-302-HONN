using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.Entities;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Repositories.DBContext;
using VideotapesGalore.Repositories.Implementation;
using VideotapesGalore.Repositories.Interfaces;
using VideotapesGalore.Services.Implementation;
using VideotapesGalore.Services.Interfaces;
using VideotapesGalore.WebApi.Authorization;
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
                    Description =
                    "API for management system for renting video tapes to users. " +
                    "Includes review functionality as well as user personalized recommendation service. " +
                    "Serves data from a remote MySQL server. " +
                    "This API was developed and designed as assignment for the course T-302-HÖNN " +
                    "for Reykjavík University by Edda Steinunn Rúnarsdóttir and Alexander Björnsson.",
                    
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHttpContextAccessor();

            // Set up API-specific dependency injections for services and repositories
            
            // Singleton is set for authorization handler and log service
            // (i.e. serves same purpose as using factory design pattern)
            services.AddSingleton<IAuthorizationHandler, InitializationRequirementHandler>();
            services.AddSingleton<ILogService, LogService>();
            // Transient is set for Repositories and services as they can belong to
            // many classes and/or controllers
            services.AddTransient<ITapeRepository, TapeRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<IBorrowRecordRepository, BorrowRecordRepository>();
            services.AddTransient<ITapeService, TapeService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IRecommendationService, RecommendationService>();

            // Add authorization to initialization process - VERY RESTRICTED
            // I.e. tells system to force a requirement client must fulfill to access restricted routes in system
            // (In this case, client authorization header value must match a secret string/shared key) 
            services.AddAuthorization(options =>
                options.AddPolicy("InitializationAuth", policy => policy.Requirements.Add(new InitializationRequirement())));

            // Provide MySQL connection prerequisite (connection string) to concrete repositories
            var MySqlConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContextPool<VideotapesGaloreDBContext>(
                options => options.UseMySql(MySqlConnectionString));
        }
        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="applicationLifetime"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            // Enable Swagger for API documentation of the system
            // Documentation available when server is running at <host>/api-documentation
            app.UseSwagger();
            app.UseSwaggerUI(opt =>{
                opt.RoutePrefix = "api/v1/documentation";
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Videotapes Galore API");
            });

            // Sets up global exception handling and exception logging
            // On exceptions, an automatic HTTP response to client with error message
            // Handles the custom exceptions that yield HTTP responses of
            //      404s (ResourceNotFoundException),
            //      400s (ParameterFormatException) and
            //      412s (InputFormatException) and
            //      401s (AuthorizationException) and
            //      500 (Default for any other exception)
            app.ConfigureExceptionHandler();

            // Require authentication mechanism in system - ONLY FOR SYSTEM INITIALIZATION
            // Forces requirement client must fulfill to access specified restricted routes in system
            // (In this case, client authorization header value must match a secret string/shared key)
            app.UseAuthentication();

            app.UseMvc();

           try {
                // Create support for automatic mapping of models in system
                AutoMapper.Mapper.Initialize(cfg => {
                    // Map entities to DTOs
                    cfg.CreateMap<Tape, TapeDTO>();
                    cfg.CreateMap<User, UserDTO>();
                    cfg.CreateMap<Review, ReviewDTO>();
                    cfg.CreateMap<BorrowRecord, BorrowRecordDTO>();
                    // Map DTOs to detail DTOs
                    cfg.CreateMap<TapeDetailDTO, TapeDTO>();
                    cfg.CreateMap<TapeRecommendationDTO, TapeDTO>();
                    cfg.CreateMap<UserDetailDTO, UserDTO>();
                    cfg.CreateMap<TapeDTO, TapeBorrowRecordDTO>();
                    cfg.CreateMap<BorrowRecordInputModel, BorrowRecordDTO>();
                    // Map detail DTOs to DTOs
                    cfg.CreateMap<TapeDTO, TapeRecommendationDTO>();
                    cfg.CreateMap<TapeDTO, TapeDetailDTO>();
                    cfg.CreateMap<UserDTO, UserDetailDTO>();
                    cfg.CreateMap<TapeBorrowRecordDTO, TapeDTO>();
                    // Map DTOs to entities
                    cfg.CreateMap<TapeDTO, Tape>();
                    cfg.CreateMap<UserDTO, User>();
                    cfg.CreateMap<ReviewDTO, Review>();
                    cfg.CreateMap<BorrowRecordDTO, BorrowRecord>();
                    cfg.CreateMap<UserInputModel, UserDTO>();
                    // Map input models to entities
                    cfg.CreateMap<TapeInputModel, Tape>()
                        .ForMember(m => m.CreatedAt, opt => opt.UseValue(DateTime.Now))
                        .ForMember(m => m.LastModified, opt => opt.UseValue(DateTime.Now));
                    cfg.CreateMap<UserInputModel, User>()
                        .ForMember(m => m.CreatedAt, opt => opt.UseValue(DateTime.Now))
                        .ForMember(m => m.LastModified, opt => opt.UseValue(DateTime.Now));
                    cfg.CreateMap<ReviewInputModel, Review>()
                        .ForMember(m => m.CreatedAt, opt => opt.UseValue(DateTime.Now))
                        .ForMember(m => m.LastModified, opt => opt.UseValue(DateTime.Now));
                    cfg.CreateMap<BorrowRecordInputModel, BorrowRecord>()
                        .ForMember(m => m.CreatedAt, opt => opt.UseValue(DateTime.Now))
                        .ForMember(m => m.LastModified, opt => opt.UseValue(DateTime.Now));
                    cfg.CreateMap<BorrowRecordDTO, BorrowRecord>()
                        .ForMember(m => m.CreatedAt, opt => opt.UseValue(DateTime.Now))
                        .ForMember(m => m.LastModified, opt => opt.UseValue(DateTime.Now));
                });
           } catch(InvalidOperationException) {
                // Automapper already set, do nothing    
           }
        }
    }
}
