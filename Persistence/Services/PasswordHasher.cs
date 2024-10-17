using Application.Interfaces;

namespace Persistence.Services;

public class PasswordHasher : IPasswordHasher
{
    public string Generate(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    public bool Validate(string password, string hashedPassword) => BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
}