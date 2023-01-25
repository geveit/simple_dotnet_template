using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleDotnetTemplate.Core.Common;

namespace SimpleDotnetTemplate.Core.Users.Interfaces
{
    public interface IUserRepository : ICrudRepository<User>
    {
        Task<User> FindByEmailAsync(string email);   
    }
}