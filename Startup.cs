using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env , ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Use(async (context , next) =>
            {
                logger.LogInformation("mw1: incommig Request");
                await next();
                logger.LogInformation("mw1: incommig Request");
            });
            app.Use(async (context, next) =>
            {
                logger.LogInformation("mw2: incommig Request");
                await next();
                logger.LogInformation("mw2: incommig Request");
            });
            app.Run(async (context) =>
            {
                await context.Response
                .WriteAsync("mw3: Request handled and response produced");
                logger.LogInformation("mw3: Request handled and response produced");
            });
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response
                    .WriteAsync("hello");
                });
            });
        }
    }
}
