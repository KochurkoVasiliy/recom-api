using System.ComponentModel.DataAnnotations;
using Application.Common.Mappings;
using Application.Users.Commands.Login;
using Application.Users.Commands.Register;
using AutoMapper;

namespace WebApp.Models.User;

public class LoginUserDto: IMapWith<LoginUserCommand>
{
    [Required] public string Email { get; set; }
    [Required] public string PasswordHash { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RegisterUserDto, LoginUserCommand>()
            .ForMember(userCommand => userCommand.Email,
                opt => opt.MapFrom(userDto => userDto.Email))
            .ForMember(userCommand => userCommand.Password,
                opt => opt.MapFrom(userDto => userDto.PasswordHash));
    }
}