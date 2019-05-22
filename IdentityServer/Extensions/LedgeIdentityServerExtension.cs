using System;
using System.Collections.Generic;
using IdentityServer.IdentityOptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Extensions
{
    public static class LedgeIdentityServerExtension
    {
        public static void AddLedgeIdentityServerExtension(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment environment)
        {
             var resourceoptions = new List<ApiResourceOptions>();
             configuration.GetSection("ApiResourceOptions").Bind(resourceoptions);
            //services.Configure<ApiResourceOptions>(section);

            var clientoptions = new List<ClientOptions>();
            configuration.GetSection("ClinetOptions").Bind(clientoptions);

            //This is inmemory collection as of now, it will be cross cutting and change it to Custome stor in future
            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApis(resourceoptions))
                .AddInMemoryClients(Config.GetClients(clientoptions));
                //.AddTestUsers(Config.GetUsers());

            if (environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }
        }
    }
}
