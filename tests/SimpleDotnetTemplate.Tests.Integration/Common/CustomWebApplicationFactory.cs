using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleDotnetTemplate.Core.Users;
using SimpleDotnetTemplate.Core.Users.Interfaces;
using SimpleDotnetTemplate.Infrastructure.Data;

namespace SimpleDotnetTemplate.Tests.Integration.Common
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private static readonly object _lock = new();
        private static bool _databaseInitialized = false;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationContext>));
                services.Remove(dbContextDescriptor);

                services.AddDbContext<ApplicationContext>((container, options) =>
                {
                    options.UseInMemoryDatabase("TestDatabase");
                });
            });

            builder.UseEnvironment("Test");
        }

        public void InitDatabase()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var scope = Services.CreateScope())
                    {
                        using (var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>())
                        {
                            context.Database.EnsureDeleted();
                            context.Database.EnsureCreated();

                            context.SeedUsers();

                            context.SaveChanges();
                        }
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public HttpClient CreateAuthorizedClient()
        {
            var tokenService = Services.GetService<ITokenService>();

            var token = tokenService.GenerateToken(new User() { Id = 1, Email = "email@email.com" });

            var client = CreateClient();

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            return client;
        }
    }
}