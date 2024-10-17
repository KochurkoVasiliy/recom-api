using Domain;
using MediatR;

namespace Application.Users.Commands.Login;

public class LoginUserCommand : IRequest<LoginUserRequest>
{
    public string Email { get; set; }
    public string Password { get; set; }
}