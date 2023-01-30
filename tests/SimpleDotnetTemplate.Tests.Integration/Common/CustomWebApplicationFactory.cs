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
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Development");
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