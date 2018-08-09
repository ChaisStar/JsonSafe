namespace JsonSafe.WebApi.Extensions
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using AutoMapper;
    using Dtos.FluentValidators;
    using Dtos.JsonModels;
    using Dtos.UserModels;
    using FluentValidation;
    using Mappings;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection @this, string issuer, string key)
        {
            @this.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });
        }

        
        public static void AddAutomapperProfiles(this IServiceCollection @this)
        {
            @this.AddAutoMapper(configurationExpression =>
            {
                configurationExpression.AddProfile<JsonProfile>();
                configurationExpression.AddProfile<UserProfile>();
            });
        }

        public static void AddFluentValidators(this IServiceCollection @this)
        {
            @this.AddTransient<IValidator<CreateJsonRequestDto>, CreateJsonRequestDtoValidator>();
            @this.AddTransient<IValidator<LoginUserRequestDto>, LoginUserRequestDtoValidator>();
            @this.AddTransient<IValidator<RegisterUserRequestDto>, RegisterUserRequestDtoValidator>();
        }
    }
}
