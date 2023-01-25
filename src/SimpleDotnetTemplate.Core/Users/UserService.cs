using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SimpleDotnetTemplate.Core.Common.Exceptions;
using SimpleDotnetTemplate.Core.Users.Dto;
using SimpleDotnetTemplate.Core.Users.Interfaces;

namespace SimpleDotnetTemplate.Core.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public UserService(IMapper mapper, IUserRepository userRepository, IPasswordService passwordService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public async Task<UserOutput> CreateUserAsync(CreateUserInput input)
        {
            var user = _mapper.Map<User>(input);

            user.PasswordHash = _passwordService.HashPassword(input.Password);

            _userRepository.Add(user);

            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserOutput>(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.FindAsync(id);

            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            _userRepository.Remove(user);

            await _userRepository.SaveChangesAsync();
        }

        public async Task<UserOutput> GetUserAsync(int id)
        {
            var user = await _userRepository.FindAsync(id);

            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UserOutput>(user);;
        }

        public async Task<IEnumerable<UserOutput>> ListUsersAsync()
        {
            var users = await _userRepository.ToListAsync();

            return _mapper.Map<IEnumerable<UserOutput>>(users);
        }
    }
}