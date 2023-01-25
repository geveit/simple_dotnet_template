using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleDotnetTemplate.Core.Users.Dto;

namespace SimpleDotnetTemplate.Core.Users.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserOutput>> ListUsersAsync();
        Task<UserOutput> GetUserAsync(int id);
        Task<UserOutput> CreateUserAsync(CreateUserInput input);
        Task DeleteUserAsync(int id);
    }
}