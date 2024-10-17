using Domain;

namespace Application.Interfaces;

public interface IJwtProvider
{
    public string GenerateToken(User user);
}