using Application.Interfaces;
using Domain;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Text;
using Persistence.Services.Models;

namespace Persistence.Services;

public class KNearestNeighborsService : IKNearestNeighborsService
{
    /// <summary>
    /// Returns a list of IDs of the k nearest neighbors to the target film.
    /// </summary>
    /// <param name="targetFilm">The target film.</param>
    /// <param name="films">The list of films to compare.</param>
    /// <param name="k">The number of nearest neighbors.</param>
    /// <returns>A list of IDs of the k nearest neighbors.</returns>
    public List<int> GetNearestNeighbors(Film targetFilm, List<Film> films, int k = 25)
    {
        var distances = new List<Tuple<Film, float>>();

        foreach (var film in films)
        {
            float distance = ComputeEuclideanDistance(targetFilm, film);
            distances.Add(new Tuple<Film, float>(film, distance));
        }

        return distances.OrderBy(x => x.Item2).Take(k).Select(x => x.Item1.Id).ToList();
    }
    
    private float ComputeEuclideanDistance(Film film1, Film film2)
    {
        var norm1 = NormalizeData(film1);
        var norm2 = NormalizeData(film2);

        float sum = 0;
        for (int i = 0; i < norm1.Count; i++)
        {
            sum += (float)Math.Pow(norm1[i] - norm2[i], 2);
        }

        return (float)Math.Sqrt(sum);
    }
    
    private List<float> NormalizeData(Film film)
    {
        return new List<float>()
        {
            Normalize(film.RatingKinopoisk.HasValue ? (float)film.RatingKinopoisk.Value : 0f, 0f, 10f),
            Normalize((float)film.Year, 1900f, 2024f),
            Normalize(film.FilmLength.HasValue ? (float)film.FilmLength.Value : 40f, 40f, 260f),
            Normalize(ExtractAgeLimit(film.RatingAgeLimits), 0f, 18f)
        };
    }
    
    private float ExtractAgeLimit(string? ageRating)
    {
        if (string.IsNullOrEmpty(ageRating))
        {
            return 18f;
        }
        
        string numericPart = ageRating.Substring(3);
        
        if (float.TryParse(numericPart, out float ageLimit))
        {
            return ageLimit;
        }

        return 18f;
    }

    private float Normalize(float value, float min, float max)
    {
        if (max - min == 0)
        {
            return 0f;
        }
        return (value - min) / (max - min);
    }
}