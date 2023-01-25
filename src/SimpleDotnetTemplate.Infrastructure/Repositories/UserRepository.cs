using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleDotnetTemplate.Core.Users;
using SimpleDotnetTemplate.Core.Users.Interfaces;
using SimpleDotnetTemplate.Infrastructure.Data;

namespace SimpleDotnetTemplate.Infrastructure.Repositories
{
    public class UserRepository : EntityFrameworkRepository, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }

        public void Add(User entity)
        {
            _context.Users.Add(entity);
        }

        public async Task<User> FindAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users.Where(user => user.Email == email).FirstOrDefaultAsync();
        }

        public void Remove(User entity)
        {
            _context.Users.Remove(entity);
        }

        public async Task<IEnumerable<User>> ToListAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}