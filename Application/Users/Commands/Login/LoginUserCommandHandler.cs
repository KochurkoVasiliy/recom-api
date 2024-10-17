using System.Net;
using Application.Interfaces;
using Application.Users.Commands.Register;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserRequest>
{
    private readonly IAppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    
    public LoginUserCommandHandler(IAppDbContext context, IPasswordHasher passwordHasher, IJwtProvider jwtProvider) =>
        (_context, _passwordHasher, _jwtProvider) = (context, passwordHasher, jwtProvider);
    
    public async Task<LoginUserRequest> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (userEntity is null)
        {
            return new LoginUserRequest()
            {
                StatusCode = HttpStatusCode.NotFound,
                Token = String.Empty
            };
        }
        
        if (!_passwordHasher.Validate(request.Password, userEntity.PasswordHash))
        {
            return new LoginUserRequest()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Token = String.Empty
            };
        }
        
        return new LoginUserRequest()
        {
            StatusCode = HttpStatusCode.OK,
            Token = _jwtProvider.GenerateToken(userEntity)
        };
    }
}