using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using IdentityModel.Client;
using Xunit;
using FluentAssertions;

using System.Threading.Tasks;

namespace IdentityServer_Test
{
    public class IdentityServerFixture
    {
        [Fact]
        public async Task Is_IdentityServer_endpoint_discovered_Async()
        {
            // discover endpoints from metadata
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            disco.IsError.Should().Be(false);
        }
        
        [Fact]
        public async Task Is_Token_Generated_Successfully()
        {
            // discover endpoints from metadata
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            disco.IsError.Should().Be(false);

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret", //key for authentication

               // UserName = "alice",
              //  Password = "password",

                Scope = "Ledger-API"
            });

            tokenResponse.IsError.Should().Be(false);
        }

        [Fact]
        public async Task Token_Generation_Should_be_With_Error_for_invalid_scope()
        {
            // discover endpoints from metadata
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            disco.IsError.Should().Be(false);

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret", //key for authentication
                Scope = "Ledger-API1"
            });

            tokenResponse.IsError.Should().Be(true);
            
        }

        [Fact]
        public async Task Token_Generation_Should_be_With_Error_for_invalid_clientID_Password()
        {
            // discover endpoints from metadata
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            disco.IsError.Should().Be(false);

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = disco.TokenEndpoint,
                ClientId = "client1",
                ClientSecret = "secret", //key for authentication
                Scope = "Ledger-API1"
            });

            tokenResponse.IsError.Should().Be(true);
        }
    }
}
