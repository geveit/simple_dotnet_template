using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SimpleDotnetTemplate.Core.Users.Dto;

namespace SimpleDotnetTemplate.Core.Users
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserOutput>();
            CreateMap<CreateUserInput, User>();
        }
    }
}