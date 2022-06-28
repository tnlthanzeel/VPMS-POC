using Microsoft.OpenApi.Models;
using System.Reflection;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

namespace VPMS.Api.DIServiceExtensions;

internal static class SwaggerConfig
{
    public static void AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddEnumsWithValuesFixFilters();
            c.EnableAnnotations();
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer" }
                    }, new List<string>() }
            });

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "VPMS API",

            });

            var apiCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var apiCommentsFullPath = Path.Combine(AppContext.BaseDirectory, apiCommentsFile);
            c.IncludeXmlComments(apiCommentsFullPath);


        });

        services.AddSwaggerGenNewtonsoftSupport();
    }
}