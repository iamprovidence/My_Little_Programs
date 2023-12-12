using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace FunWithTests.Configurations
{
    public static class SwaggerConfigurations
    {
        private const string SwaggerClientId = "swagger";
        private const string ServerScope = "server";

        public static IServiceCollection AddServerSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.ToString());
                c.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Title = "FunWithTests",
                    Version = "v1.0",
                });

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:5001/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                [ServerScope] = "Server scope",
                            },
                        },
                    },
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" },
                        },
                        Array.Empty<string>()
                    },
                });
            });

            return services;
        }

        public static IApplicationBuilder UseServerSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.OAuthClientId(SwaggerClientId);
                c.OAuthScopes(ServerScope);

                c.SwaggerEndpoint("v1.0/swagger.json", "FunWithTests API");
            });

            return app;
        }
    }
}
