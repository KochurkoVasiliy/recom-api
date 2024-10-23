namespace Persistence.Services.Models;

public class FilmContentModel
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public float RatingKinopoisk { get; set; }
    public float Year { get; set; }
    public float FilmLength { get; set; }
    public float RatingAgeLimits { get; set; }
}