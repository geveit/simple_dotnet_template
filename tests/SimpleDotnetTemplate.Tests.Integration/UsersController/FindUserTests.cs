using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SimpleDotnetTemplate.Core.Users.Dto;
using SimpleDotnetTemplate.Tests.Integration.Common;

namespace SimpleDotnetTemplate.Tests.Integration.UsersController
{
    public class FindUserTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public FindUserTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_FindUserReturnCorrectUser()
        {
            var client = _factory.CreateAuthorizedClient();

            var response = await client.GetAsync("/users/1");

            response.EnsureSuccessStatusCode();

            var output = await response.Content.ReadFromJsonAsync<UserOutput>();

            Assert.Equal(1, output.Id);
            Assert.Equal("User 1", output.Name);
            Assert.Equal("email@email.com", output.Email);
        }
    }
}