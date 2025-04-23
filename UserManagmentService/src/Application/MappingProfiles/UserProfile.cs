using AutoMapper;
using Domain.Entities;
using Shared.DTOs.UserDTOs;

namespace Application.MappingProfles;

public class UserProfile : Profile {

    public UserProfile() {

        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();

    }

}