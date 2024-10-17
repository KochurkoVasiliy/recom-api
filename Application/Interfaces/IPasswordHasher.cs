namespace Application.Interfaces;

public interface IPasswordHasher
{
    public string Generate(string password);
    public bool Validate(string password, string hashedPassword);
}