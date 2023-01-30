using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using SimpleDotnetTemplate.Core.Users.Dto;
using SimpleDotnetTemplate.Tests.Integration.Common;

namespace SimpleDotnetTemplate.Tests.Integration.Controllers
{
    public class AuthControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public AuthControllerTests(CustomWebApplicationFactory factory)
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