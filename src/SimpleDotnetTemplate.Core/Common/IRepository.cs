using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleDotnetTemplate.Core.Common
{
    public interface IRepository
    {
        Task SaveChangesAsync();
    }
}