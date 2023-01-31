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
using Microsoft.Extensions.Hosting;
using SimpleDotnetTemplate.Core.Users;
using SimpleDotnetTemplate.Core.Users.Interfaces;
using SimpleDotnetTemplate.Infrastructure.Data;

namespace SimpleDotnetTemplate.Tests.Integration.Common
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly object _lock = new();

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            var host = builder.Build();
            host.Start();

            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.PopulateDatabase();

                context.SaveChanges();
            }

            return host;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationContext>));
                services.Remove(dbContextDescriptor);

                string databaseName = Guid.NewGuid().ToString();

                services.AddDbContext<ApplicationContext>((container, options) =>
                {
                    options.UseInMemoryDatabase(databaseName);
                });
            });
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