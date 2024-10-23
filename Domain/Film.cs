using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Domain;

public class Film
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("poster_url")]
    public string? PosterUrl { get; set; }
    [JsonProperty("web_url")]
    public string? WebUrl { get; set; }
    [JsonProperty("name_original")]
    public string? NameOriginal { get; set; }
    [JsonProperty("rating_imdb")]
    public double? RatingImdb { get; set; }
    [JsonProperty("slogan")]
    public string? Slogan { get; set; }
    [JsonProperty("rating_imdb_vote_count")]
    public int RatingImdbVoteCount { get; set; }
    [JsonProperty("description")]
    public string? Description { get; set; }
    [JsonProperty("rating_film_critics")]
    public double? RatingFilmCritics { get; set; }
    [JsonProperty("rating_kinopoisk")]
    public double? RatingKinopoisk { get; set; }
    [JsonProperty("kinopoisk_id")]
    public int KinopoiskId { get; set; }
    [JsonProperty("rating_critics_vote_count")]
    public int RatingCriticsVoteCount { get; set; }
    [JsonProperty("short_desription")]
    public string? ShortDescription { get; set; }
    [JsonProperty("imdb_id")]
    public string? ImdbId { get; set; }
    [JsonProperty("year")]
    public int Year { get; set; }
    [JsonProperty("rating_age_limits")]
    public string? RatingAgeLimits { get; set; }
    [JsonProperty("name_ru")]
    public string? NameRu { get; set; }
    [JsonProperty("film_length")]
    public int? FilmLength { get; set; }
    [JsonProperty("genres")]
    public string? Genres { get; set; }
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}