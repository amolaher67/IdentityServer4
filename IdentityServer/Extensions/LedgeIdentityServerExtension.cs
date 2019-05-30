using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using IdentityModel;
using IdentityServer.IdentityOptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;
using IdentityServer.Certificates;

namespace IdentityServer.Extensions
{
    public static class LedgeIdentityServerExtension
    {
        public static void AddLedgeIdentityServerExtension(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment environment)
        {
            try
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
                // .AddSigningCredential(Certificate.Get());

                if (environment.IsDevelopment())
                {
                    builder.AddDeveloperSigningCredential();
                }
                else
                {
                    builder.AddCertificateFromStore(configuration);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private static void AddCertificateFromStore(this IIdentityServerBuilder builder, IConfiguration options)
        {
            try
            {
                var subjectName = options.GetValue<string>("ThumbPrint");

                var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);
                var certificates = store.Certificates.Find(X509FindType.FindByThumbprint, subjectName, false);
                if (certificates.Count > 0)
                {
                    builder.AddSigningCredential(certificates[0]);
                }
                else
                    Log.Error("A matching key couldn't be found in the store");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
