using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleDotnetTemplate.Core.Users.Dto;
using SimpleDotnetTemplate.Core.Users.Exceptions;
using SimpleDotnetTemplate.Core.Users.Interfaces;

namespace SimpleDotnetTemplate.Core.Users
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, IPasswordService passwordService, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        public async Task<AuthOutput> Authenticate(AuthInput input)
        {
            var user = await _userRepository.FindByEmailAsync(input.Email);

            if (user == null)
            {
                throw new AuthFailedException();
            }

            bool passwordIsValid = _passwordService.Verify(input.Password, user.PasswordHash);

            if (!passwordIsValid)
            {
                throw new AuthFailedException();
            }

            var token = _tokenService.GenerateToken(user);

            return new AuthOutput()
            {
                Token = token
            };
        }
    }
}