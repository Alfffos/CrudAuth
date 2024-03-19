using AutoMapper;
using CrudAuth.Models.DTOs;
using CrudAuth.Models.Entities;

namespace CrudAuth.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateDTO, User>();
            CreateMap<User, UserGetDTO>();
            
        }

    }
}
