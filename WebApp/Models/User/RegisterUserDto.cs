using System.ComponentModel.DataAnnotations;
using Application.Common.Mappings;
using Application.Users.Commands.Register;
using AutoMapper;

namespace WebApp.Models.User;

public class RegisterUserDto : IMapWith<RegisterUserCommand>
{
    [Required] public string Username { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string PasswordHash { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RegisterUserDto, RegisterUserCommand>()
            .ForMember(userCommand => userCommand.Username,
                opt => opt.MapFrom(userDto => userDto.Username))
            .ForMember(userCommand => userCommand.Email,
                opt => opt.MapFrom(userDto => userDto.Email))
            .ForMember(userCommand => userCommand.PasswordHash,
                opt => opt.MapFrom(userDto => userDto.PasswordHash));
    }
}