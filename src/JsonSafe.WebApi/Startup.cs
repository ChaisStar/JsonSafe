namespace JsonSafe.WebApi
{
    using System.Diagnostics.CodeAnalysis;
    using ApplicationSettings;
    using Database;
    using Database.Infrastructure;
    using Extensions;
    using FluentValidation.AspNetCore;
    using Infrastructure;
    using Infrastructure.Middlewares;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Infrastructure;

    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private readonly AppSettings _appSettings;

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            _appSettings = new AppSettings(builder.Build());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAppSettings, AppSettings>();
            services.AddScoped<IDatabaseConfig, DatabaseConfig>(provider => _appSettings.Database);
            services.AddScoped<IJwtTokenConfig, JwtTokenConfig>(provider => _appSettings.JwtToken);
            services.AddScoped<IJsonService, JsonService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordManager, PasswordManager>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJsonRepository, JsonRepository>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IApiKeyGenerator, ApiKeyGenerator>();
            services.AddScoped<INumberGenerator, NumberGenerator>();
            services.AddScoped(typeof(IMongoCollectionClient<>), typeof(MongoCollectionClient<>));
            services.AddScoped<IDatabaseMongoClient, DatabaseMongoClient>();
            services.AddJwtAuthentication(_appSettings.JwtToken.Issuer, _appSettings.JwtToken.Key);
            services.AddAutomapperProfiles();
            services.AddFluentValidators();
            services.AddMvc(x => x.Filters.Add(typeof(ValidateModelAttribute)))
                .AddFluentValidation(_ => {});
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseDefaultFiles();
            app.UseAuthentication();
            app.UseMiddleware<RedirectToStaticFile>("/index.html", "api");
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
