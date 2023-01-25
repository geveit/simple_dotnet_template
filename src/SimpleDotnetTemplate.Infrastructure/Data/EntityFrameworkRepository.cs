using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleDotnetTemplate.Core.Common;

namespace SimpleDotnetTemplate.Infrastructure.Data
{
    public class EntityFrameworkRepository : IRepository
    {
        protected readonly ApplicationContext _context;

        public EntityFrameworkRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}