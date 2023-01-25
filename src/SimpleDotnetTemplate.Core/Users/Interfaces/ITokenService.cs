using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleDotnetTemplate.Core.Users.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}