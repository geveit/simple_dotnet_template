using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleDotnetTemplate.Core.Users.Dto;

namespace SimpleDotnetTemplate.Core.Users.Interfaces
{
    public interface IAuthService
    {
        Task<AuthOutput> Authenticate(AuthInput input);
    }
}