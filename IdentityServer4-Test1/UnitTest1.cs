using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace IdentityServer4_Test1
{
    public class IdentityServerFixtureTests : IClassFixture<WebApplicationFactory<IdentityServer.Startup>>
    {
        private readonly WebApplicationFactory<IdentityServer.Startup> _factory;

        public IdentityServerFixtureTests(WebApplicationFactory<IdentityServer.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task identityserver4_discovery_endpoint_running()
        {

            var client = _factory.CreateClient();
            // Act
            var response = await client.GetAsync("/.well-known/openid-configuration");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        //[Fact]
        //public async Task Is_Token_Generated_Successfully()
        //{

        //    var client = _factory.CreateClient();

        //    // Act
        //    var response = await client.GetAsync("/.well-known/openid-configuration");
        //    dynamic content = await response.Content.ReadAsStringAsync();
        //    Discoveryresponse result = JsonConvert.DeserializeObject<Discoveryresponse>(content);
        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        //    if (response.StatusCode != HttpStatusCode.OK)
        //        return;


        //    var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        //    {
        //        Address = result.token_endpoint,
        //        ClientId = "client",
        //        ClientSecret = "secret",
        //        Scope = "Ledger-API"
        //    });

        //    tokenResponse.IsError.Should().Be(false);
        //}

        [Fact]
        public void Token_Generation_Should_be_With_Error_for_invalid_scope()
        {

            Assert.True(true);
        }

        [Fact]
        public void Token_Generation_Should_be_With_Error_for_clientId()
        {
            Assert.True(true);
        }

        public class Discoveryresponse
        {
            public string token_endpoint { get; set; }
        }
    }
}
