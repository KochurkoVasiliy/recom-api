using MediatR;

namespace Application.Users.Commands.Register;

public class RegisterUserCommand : IRequest<Guid>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}