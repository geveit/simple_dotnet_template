using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleDotnetTemplate.Core.Users;
using SimpleDotnetTemplate.Infrastructure.Data;

namespace SimpleDotnetTemplate.Tests.Integration.Common
{
    public static class DatabaseSeedExtensions
    {
        public static void SeedUsers(this ApplicationContext context)
        {
            context.Users.AddRange(
                new User() { Name = "User 1", Email = "email@email.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password") },
                new User() { Name = "User 2", Email = "email2@email.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password") },
                new User() { Name = "User 3", Email = "email3@email.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password") }
            );
        }
    }
}