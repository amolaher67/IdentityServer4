using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer.Exceptions;
using IdentityServer.IdentityOptions;
using IdentityServer4.Test;
using Microsoft.EntityFrameworkCore.Internal;

namespace IdentityServer
{
    //In Memory configuration for Identity server setup
    //All below configuration are in memeroy for now.We can move this to custome store to get all details from Databse and everthing configurable
    public static class Config
    {
        /// <summary>
        /// Define All resources here - these all are acts as scope while requesting access token from identity service
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        /// <summary>
        /// We can also define Resources here 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApis(List<ApiResourceOptions> apiResourceOptions)
        {
            if (apiResourceOptions == null || apiResourceOptions.Count == 0)
                throw new IdentityServerExceptions(
                    "IdentityServer.Config.GetApis - Application configuration missing for setting key 'ApiResourceOptions'");

            var resourceslst = new List<ApiResource>();
            foreach (var resource in apiResourceOptions)
            {
                resourceslst.Add(new ApiResource(resource.Name, resource.DisplayName));
            }

            return resourceslst;
        }
        
        /// <summary>
        /// Parametrise client systems here.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients(List<ClientOptions> clientOptions)
        {
            var clientList = new List<Client>();

            if (clientOptions == null || clientOptions.Count == 0)
                throw new IdentityServerExceptions(
                    "IdentityServer.Config.GetClients - Application configuration missing for setting key 'ClinetOptions'");

            #region GrantTypes

            /*
                public const string Implicit = "implicit";
                public const string Hybrid = "hybrid";
                public const string AuthorizationCode = "authorization_code";
                public const string ClientCredentials = "client_credentials";
                public const string ResourceOwnerPassword = "password";
                public const string DeviceFlow = "urn:ietf:params:oauth:grant-type:device_code";
             */

            #endregion

            foreach (var item in clientOptions)
            {
                clientList.Add(new Client()
                {
                    ClientId = item.ClientId,

                    AllowedGrantTypes =GrantTypes.ClientCredentials, //its hardCode as of now--- TODO to make it dynamic

                    ClientSecrets =
                    {
                        new Secret(!string.IsNullOrEmpty(item.ClientSecret)? item.ClientSecret.Sha256()
                            : throw new IdentityServerExceptions("IdentityServer.Config.GetClients - Application configuration missing for setting key 'ClinetOptions.ClientSecret'"))
                    },
                    AllowedScopes = (item.AllowedScopes != null && item.AllowedScopes.Count > 0)
                        ? item.AllowedScopes
                        : throw new IdentityServerExceptions("IdentityServer.Config.GetClients - Application configuration missing for setting key 'ClinetOptions.ClientSecret'")

                });

                #region Commented
                /*return new List<Client>
               {
                   new Client
                   {
                       ClientId = "client",

                       // no interactive user, use the clientid/secret for authentication
                       AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                       //AccessTokenType = AccessTokenType.Jwt,
                       //AccessTokenLifetime = 120, //86400,
                       //IdentityTokenLifetime = 120, //86400,
                       //UpdateAccessTokenClaimsOnRefresh = true,
                       //SlidingRefreshTokenLifetime = 30,
                       //AllowOfflineAccess = true,
                       //RefreshTokenExpiration = TokenExpiration.Absolute,
                       //RefreshTokenUsage = TokenUsage.OneTimeOnly,
                       //AlwaysSendClientClaims = true,
                       //Enabled = true,
                       // secret for authentication
                       ClientSecrets =
                       {
                           new Secret("secret".Sha256())
                       },

                       // scopes that client has access to
                       AllowedScopes = { "api1" }
                   },
                   //new Client
                   //{
                   //    ClientId = "mvc",
                   //    ClientName = "MVC Client",

                   //    AllowedGrantTypes = GrantTypes.Implicit,

                   //    // where to redirect to after login
                   //    RedirectUris = { "http://localhost:5002/signin-oidc" },

                   //    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                   //    AllowedScopes = new List<string>
                   //    {
                   //        IdentityServerConstants.StandardScopes.OpenId,
                   //        IdentityServerConstants.StandardScopes.Profile
                   //    },

                   //    // secret for authentication
                   //    ClientSecrets =
                   //    {
                   //        new Secret("secret".Sha256())
                   //    }
                   //}
               };*/


                #endregion
            }
            return clientList;
        }

        //public static List<TestUser> GetUsers()
        //{
        //    return new List<TestUser>
        //    {
        //        new TestUser
        //        {
        //            SubjectId = "1",
        //            Username = "alice",
        //            Password = "password"
        //        },
        //        new TestUser
        //        {
        //            SubjectId = "2",
        //            Username = "bob",
        //            Password = "password"
        //        }
        //    };
        //}
    }
}