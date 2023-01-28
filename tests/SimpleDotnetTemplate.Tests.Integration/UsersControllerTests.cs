using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace SimpleDotnetTemplate.Tests.Integration
{
    public class UsersControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UsersControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_ListUsers()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/users");

            response.EnsureSuccessStatusCode();
        }
    }
}