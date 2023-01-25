using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleDotnetTemplate.Core.Users.Interfaces
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool Verify(string password, string hash);
    }
}