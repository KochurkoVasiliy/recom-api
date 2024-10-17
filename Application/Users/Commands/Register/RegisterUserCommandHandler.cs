using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Users.Commands.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IAppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    
    public RegisterUserCommandHandler(IAppDbContext context, IPasswordHasher passwordHasher) =>
        (_passwordHasher, _context) = (passwordHasher, context);
    
    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = _passwordHasher.Generate(request.PasswordHash);

        var user = User.Create(Guid.NewGuid(), request.Username, hashedPassword, request.Email);
        
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}