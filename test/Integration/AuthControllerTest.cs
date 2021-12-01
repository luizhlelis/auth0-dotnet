using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Web;
using Microsoft.Extensions.Configuration;

namespace Auth0Dotnet.Test.Integration
{
    public class AuthControllerTest
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Startup> _factory;

        public AuthControllerTest()
        {
            // constructs the testing server with the HostBuilder configuration
            _factory = new WebApplicationFactory<Startup>();
            _client = _factory.CreateClient();
        }

        [Fact(DisplayName = "Should return found with redirect url when login")]
        public async Task ShouldReturnFoundWithRedirectUrlWhenLoginAsync()
        {
            // Act
            var response = await _client.GetAsync("v1/Auth/login");

            // Assert
            var responseRequestUri = response.RequestMessage.RequestUri;

            responseRequestUri.AbsolutePath
                .Should().BeEquivalentTo("/authorize");
            responseRequestUri.Authority
                .Should().BeEquivalentTo("auth.luizlelis.com");
        }
    }
}
