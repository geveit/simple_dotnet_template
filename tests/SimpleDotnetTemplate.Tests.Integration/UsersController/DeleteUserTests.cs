using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleDotnetTemplate.Infrastructure.Data;
using SimpleDotnetTemplate.Tests.Integration.Common;

namespace SimpleDotnetTemplate.Tests.Integration.UsersController
{
    public class DeleteUserTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public DeleteUserTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void Delete_DeleteUser()
        {
            var client = _factory.CreateAuthorizedClient();

            var response = await client.DeleteAsync("/users/3");

            response.EnsureSuccessStatusCode();

            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                var count = await context.Users.CountAsync();

                Assert.Equal(2, count);
            }
        }
    }
}