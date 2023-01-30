using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using SimpleDotnetTemplate.Core.Users;
using SimpleDotnetTemplate.Core.Users.Dto;
using SimpleDotnetTemplate.Tests.Integration.Common;

namespace SimpleDotnetTemplate.Tests.Integration.Controllers
{
    public class UsersControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public UsersControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _factory.InitDatabase();
        }

        [Fact]
        public async Task Get_ListUsers()
        {
            var client = _factory.CreateAuthorizedClient();

            var response = await client.GetAsync("/users");

            response.EnsureSuccessStatusCode();

            var output = await response.Content.ReadFromJsonAsync<IEnumerable<UserOutput>>();

            Assert.Equal(3, output.Count());
        }
    }
}