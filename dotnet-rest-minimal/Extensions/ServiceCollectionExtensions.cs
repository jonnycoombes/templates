using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.OpenApi.Models;

namespace JCS.Neon.Rest.Api.Extensions;

/// <summary>
/// Extensions for the DI <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add/register all the core services required by the API
    /// </summary>
    /// <param name="services">The current <see cref="IServiceCollection"/></param>
    /// <returns>The <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddCoreApiServices(this IServiceCollection services)
    {
        return services;
    }

    /// <summary>
    /// Perform all the base configuration required by the API, including any options binding, JSON serialisation
    /// configuration etc...
    /// </summary>
    /// <param name="services">The current <see cref="IServiceCollection"/></param>
    /// <param name="config">A valid <see cref="IConfiguration"/> instance</param>
    /// <returns>The <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddCoreApiConfiguration(this IServiceCollection services, IConfiguration config)
    {
        // externalised configuration options
        services.Configure<ApiOptions>(config.GetSection(ApiOptions.ConfigurationSection));

        // json configuration options
        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.AllowTrailingCommas = false;
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.SerializerOptions.NumberHandling = JsonNumberHandling.Strict;
            options.SerializerOptions.WriteIndented = false;
        });

        return services;
    }

    /// <summary>
    /// Configure core authentication (JWT bearer based)
    /// </summary>
    /// <param name="services">The current <see cref="IServiceCollection"/></param>
    /// <param name="options">The current <see cref="ApiOptions"/></param>
    /// <returns>The <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddCoreApiAuthentication(this IServiceCollection services, ApiOptions options)
    {
        services.AddAuthentication("Bearer")
            .AddJwtBearer();
        return services;
    }

    /// <summary>
    /// Configure core authorisation (claims based policies)
    /// </summary>
    /// <param name="services">The current <see cref="IServiceCollection"/></param>
    /// <param name="options">The current <see cref="ApiOptions"/></param>
    /// <returns>The <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddCoreApiAuthorization(this IServiceCollection services, ApiOptions options)
    {
        services.AddAuthorization();
        return services;
    }

    /// <summary>
    /// Configures and customises the swagger (OpenAPI) configuration for the API
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCoreApiSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"Please enter a valid token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        // add the explorer
        services.AddEndpointsApiExplorer();
        return services;
    }
}