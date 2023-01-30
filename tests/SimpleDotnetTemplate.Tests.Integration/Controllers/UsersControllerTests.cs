using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using SimpleDotnetTemplate.Core.Users;
using SimpleDotnetTemplate.Tests.Integration.Common;

namespace SimpleDotnetTemplate.Tests.Integration.Controllers
{
    public class UsersControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public UsersControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_ListUsers()
        {
            var client = _factory.CreateAuthorizedClient();

            var response = await client.GetAsync("/users");

            response.EnsureSuccessStatusCode();
        }
    }
}