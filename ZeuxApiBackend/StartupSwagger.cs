// //StartupSwagger.cs
// // Copyright (c) 2019 01 02All Rights Reserved
// // Bogdan Lyashenko
// // corequatro@gmail.com

using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace ZeuxApiBackend
{
    public partial class Startup
    {
        private static void ConfigureSwaggerService(IServiceCollection services)
        {
            //TODO change XML document generation. 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "ZeusServiceApi",
                    Version = "v1",
                    Description = "ZeusService service http api",
                });

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "ZeusService.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        private static void ConfigureSwaggerApp(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var basePath = Environment.GetEnvironmentVariable("ASPNETCORE_APPL_PATH") ?? string.Empty;

                c.SwaggerEndpoint(
                    basePath == "/" ? $"{basePath}swagger/v1/swagger.json" : $"{basePath}/swagger/v1/swagger.json",
                    "ZeusService Api V1");
            });
        }
    }
}