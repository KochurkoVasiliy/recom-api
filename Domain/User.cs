namespace Domain;

public class User
{
    private User(Guid id, string userName, string passwordHash, string email)
    {
        Id = id;
        UserName = userName;
        PasswordHash = passwordHash;    
        Email = email;
        Ratings = new List<Rating>();
    }
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public FacetWeights FacetWeights { get; set; }


    public static User Create(Guid id, string userName, string passwordHash, string email)
    {
        return new User(id, userName, passwordHash, email);
    }
}