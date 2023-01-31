using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SimpleDotnetTemplate.Core.Users.Dto;
using SimpleDotnetTemplate.Infrastructure.Data;
using SimpleDotnetTemplate.Tests.Integration.Common;

namespace SimpleDotnetTemplate.Tests.Integration.UsersController
{
    public class CreateUserTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public CreateUserTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Post_CreateUser()
        {
            var client = _factory.CreateAuthorizedClient();

            var input = new CreateUserInput()
            {
                Name = "New User",
                Email = "newuser@email.com",
                Password = "password"
            };

            var content = JsonContent.Create<CreateUserInput>(input);

            var response = await client.PostAsync("/users", content);

            response.EnsureSuccessStatusCode();

            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                var user = await context.Users.FindAsync(4);

                Assert.NotNull(user);
                Assert.Equal("New User", user.Name);
                Assert.Equal("newuser@email.com", user.Email);
            }
        }
    }
}