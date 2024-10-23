namespace Domain;

public class FacetWeights
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Dictionary<string, double> GenreWeights { get; set; }
    public Dictionary<int, double> YearWeights { get; set; } 
    public Dictionary<int, double> FilmLenghtWeights { get; set; }
    public Dictionary<string, double> AgeRatingWeights { get; set; }
}