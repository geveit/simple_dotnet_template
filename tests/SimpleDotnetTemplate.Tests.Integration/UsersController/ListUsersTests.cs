using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SimpleDotnetTemplate.Core.Users.Dto;
using SimpleDotnetTemplate.Tests.Integration.Common;

namespace SimpleDotnetTemplate.Tests.Integration.UsersController
{
    public class ListUsersTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public ListUsersTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_ListUsersReturnCorrectList()
        {
            var client = _factory.CreateAuthorizedClient();

            var response = await client.GetAsync("/users");

            response.EnsureSuccessStatusCode();

            var output = await response.Content.ReadFromJsonAsync<IEnumerable<UserOutput>>();

            Assert.Equal(3, output.Count());
        }
    }
}