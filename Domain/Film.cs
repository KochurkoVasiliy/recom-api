namespace Domain;

public class Film
{
    public int Id { get; set; }
    public string PosterUrl { get; set; }
    public string WebUrl { get; set; }
    public string? NameOriginal { get; set; }
    public double? RatingImdb { get; set; }
    public string? Slogan { get; set; }
    public int RatingImdbVoteCount { get; set; }
    public string Description { get; set; }
    public double? RatingFilmCritics { get; set; }
    public double? RatingKinopoisk { get; set; }
    public int KinopoiskId { get; set; }
    public int RatingCriticsVoteCount { get; set; }
    public string ShortDescription { get; set; }
    public string? ImdbId { get; set; }
    public int Year { get; set; }
    public string? RatingAgeLimits { get; set; }
    public string NameRu { get; set; }
    public int? FilmLength { get; set; }
    public string Genres { get; set; }
}