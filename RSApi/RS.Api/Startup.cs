using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RS.Domain;
using RS.Domain.Models.Options;
using RS.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace RS.Api
{
    /// <summary>
    /// Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The origin name.
        /// </summary>
        private const string OriginName = "RSOrigin";

        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            AddAuth(services);
            AddCors(services);
            services.AddMvc().AddFluentValidation();

            services.AddSingleton(provider => configuration);
            BootstrapperServices.Initialize(services, configuration);
            BootstrapperValidators.Initialize(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Realsize API",
                    Description = "ASP.NET Core Web API"
                });
            });
        }

        /// <summary>
        /// Adds the cors.
        /// </summary>
        /// <param name="services">The services.</param>
        private static void AddCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(OriginName, builder => builder
                    .WithOrigins(
                        "http://localhost:4200",
                        "http://localhost:3434")
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        /// <summary>
        /// Adds the authentication.
        /// </summary>
        /// <param name="services">The services.</param>
        private static void AddAuth(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //// app.UseCors(builder => builder.AllowAnyOrigin());
            app.UseCors(OriginName);

            //// app.UseAuthentication();
            app.UseMvc();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Realsize API V1");
            });
        }
    }
}
