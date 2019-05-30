// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using IdentityServer.EndPointRouter;
using IdentityServer.Extensions;
using IdentityServer4.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiddlerWare.Extensions;

namespace IdentityServer
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Parameterized constructor to get Env and configuration data
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="configuration"></param>
        public Startup(IHostingEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        /// <summary>
        /// Configure all services into DI here
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                // uncomment, if you wan to add an MVC-based UI
                //services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

                //All Services that will required to register IdentityServer will be under below extension methid
                services.AddLedgeIdentityServerExtension(Configuration, Environment);
                services.AddTransient<IEndpointRouter, CustomEndpointRouter>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Configure(IApplicationBuilder app)
        {

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            // uncomment if you want to support static files
            //app.UseStaticFiles();

            //app.UseApiResponseWrapperMiddleware();
            app.UseIdentityServer();

            // uncomment, if you wan to add an MVC-based UI
            //app.UseMvcWithDefaultRoute();
        }
    }
}