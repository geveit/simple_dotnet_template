using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using SimpleDotnetTemplate.Core.Users.Dto;

namespace SimpleDotnetTemplate.Tests.Integration
{
    public class AuthControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public AuthControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Post_ReturnsTokenForCorrectCredentials()
        {
            var client = _factory.CreateClient();

            var input = new AuthInput() { Email = "string", Password = "string" };

            var content = JsonContent.Create<AuthInput>(input);

            var response = await client.PostAsync("/auth", content);

            response.EnsureSuccessStatusCode();

            var output = await response.Content.ReadFromJsonAsync<AuthOutput>();

            Assert.NotNull(output.Token);
        }

        [Fact]
        public async Task Post_ReturnsUnauthorizedForIncorrectCredentials()
        {
            var client = _factory.CreateClient();

            var input = new AuthInput() { Email = "Wrong", Password = "Wrong" };

            var content = JsonContent.Create<AuthInput>(input);

            var response = await client.PostAsync("/auth", content);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}