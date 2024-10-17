using FluentValidation;

namespace Application.Users.Commands.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(registerUserCommand =>
            registerUserCommand.Email).NotEmpty();
        RuleFor(registerUserCommand =>
            registerUserCommand.Username).NotEmpty();
        RuleFor(registerUserCommand =>
            registerUserCommand.PasswordHash).NotEmpty();
    }
}