using Application.Interfaces;
using Domain;

namespace Persistence.Services;

public class FacetWeightService : IFacetWeightService
{
    /*public FacetWeights Calculate(List<Film> films)
    {
        return new FacetWeights()
        {
            GenreWeights = CalculateGenreWeights(films),
            YearWeights = CalculateYearWeights(films),
            FilmLenghtWeights = CalculateFilmLengthWeights(films),
            AgeRatingWeights = CalculateAgeRatingWeights(films)
        };
    }*/
    
    public FacetWeights Calculate(List<Film> films)
    {
        return new FacetWeights()
        {
            GenreWeights = CalculateWeights(films, film => film.Genres.Split(' ').Select(g => g.Trim())),
            YearWeights = CalculateWeights(films, film => new[] { film.Year }),
            FilmLenghtWeights = CalculateWeights(films, film => new[] { film.FilmLength ?? 0 }),
            AgeRatingWeights = CalculateWeights(films, film => new[] { film.RatingAgeLimits ?? "age18" })
        };
    }
    
    private Dictionary<T, double> CalculateWeights<T>(List<Film> films, Func<Film, IEnumerable<T>> attributeExtractor)
    {
        var attributeWeights = new Dictionary<T, double>();
        var totalRating = 0.0;

        foreach (var film in films)
        {
            var userRating = film.Ratings.FirstOrDefault()?.Score ?? 0;
            totalRating += userRating;

            var attributes = attributeExtractor(film);
            foreach (var attribute in attributes)
            {
                if (attributeWeights.ContainsKey(attribute))
                {
                    attributeWeights[attribute] += userRating;
                }
                else
                {
                    attributeWeights[attribute] = userRating;
                }
            }
        }
        
        if (totalRating != 0)
        {
            foreach (var attribute in attributeWeights.Keys.ToList())
            {
                attributeWeights[attribute] /= totalRating;
            }
        }
        else
        {
            foreach (var attribute in attributeWeights.Keys.ToList())
            {
                attributeWeights[attribute] = 0;
            }
        }

        return attributeWeights;
    }
    
    public List<Film> SortFilmsByUserFacets(IEnumerable<Film> films, FacetWeights userFacetWeights)
    {
        Dictionary<int, double> ratings = new Dictionary<int, double>();
        foreach (var film in films)
        {
            var genres = film.Genres?.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? new string[0];
            
            double totalGenreWeight = 0;
            int genreCount = 0;

            foreach (var genre in genres)
            {
                if (userFacetWeights.GenreWeights.TryGetValue(genre, out double genreWeight))
                {
                    totalGenreWeight += genreWeight;
                    genreCount++;
                }
            }

            double averageGenreWeight = genreCount > 0 ? totalGenreWeight / genreCount : 0;
            
            double yearWeight = userFacetWeights.YearWeights.GetValueOrDefault(film.Year, 0);
            double filmLengthWeight = userFacetWeights.FilmLenghtWeights.GetValueOrDefault(film.FilmLength ?? 0, 0);
            double ageRatingWeight =
                userFacetWeights.AgeRatingWeights.GetValueOrDefault(film.RatingAgeLimits ?? "age18", 0);
            ratings.Add(film.Id, averageGenreWeight * yearWeight * filmLengthWeight * ageRatingWeight);
        }

        var rankedFilms = films.OrderByDescending(f => ratings[f.Id]).ToList();

        return rankedFilms;
    }
    
    // public Dictionary<string, double> CalculateGenreWeights(List<Film> films)
    // {
    //     var genreWeights = new Dictionary<string, double>();
    //     var totalRating = 0.0;
    //
    //     Console.WriteLine(films.Count);
    //
    //     foreach (var film in films)
    //     {
    //         var userRating =
    //             film.Ratings.FirstOrDefault()?.Score ??
    //             0; // Предположим, что у каждого фильма есть только одна оценка пользователя
    //         totalRating += userRating;
    //         Console.WriteLine("sdgsdg" + userRating);
    //
    //         if (!string.IsNullOrEmpty(film.Genres))
    //         {
    //             var genres = film.Genres.Split(' ').Select(g => g.Trim());
    //             foreach (var genre in genres)
    //             {
    //                 if (genreWeights.ContainsKey(genre))
    //                 {
    //                     genreWeights[genre] += userRating;
    //                 }
    //                 else
    //                 {
    //                     genreWeights[genre] = userRating;
    //                 }
    //             }
    //         }
    //     }
    //
    //     // Нормализация весов
    //     foreach (var genre in genreWeights.Keys.ToList())
    //     {
    //         genreWeights[genre] /= totalRating;
    //     }
    //
    //     return genreWeights;
    // }
    //
    // public static Dictionary<int, double> CalculateYearWeights(List<Film> films)
    // {
    //     var yearWeights = new Dictionary<int, double>();
    //     var totalRating = 0.0;
    //
    //     foreach (var film in films)
    //     {
    //         var userRating = film.Ratings.FirstOrDefault()?.Score ?? 0;
    //         totalRating += userRating;
    //
    //         if (yearWeights.ContainsKey(film.Year))
    //         {
    //             yearWeights[film.Year] += userRating;
    //         }
    //         else
    //         {
    //             yearWeights[film.Year] = userRating;
    //         }
    //     }
    //
    //     // Проверка на ноль перед нормализацией
    //     if (totalRating != 0)
    //     {
    //         // Нормализация весов
    //         foreach (var year in yearWeights.Keys.ToList())
    //         {
    //             yearWeights[year] /= totalRating;
    //         }
    //     }
    //     else
    //     {
    //         // Если totalRating равен нулю, устанавливаем все веса равными нулю
    //         foreach (var year in yearWeights.Keys.ToList())
    //         {
    //             yearWeights[year] = 0;
    //         }
    //     }
    //
    //     return yearWeights;
    // }
    //
    // public static Dictionary<int, double> CalculateFilmLengthWeights(List<Film> films)
    // {
    //     var filmLengthWeights = new Dictionary<int, double>();
    //     var totalRating = 0.0;
    //
    //     foreach (var film in films)
    //     {
    //         var userRating = film.Ratings.FirstOrDefault()?.Score ?? 0;
    //         totalRating += userRating;
    //
    //         var filmLength = film.FilmLength ?? 0;
    //         if (filmLengthWeights.ContainsKey(filmLength))
    //         {
    //             filmLengthWeights[filmLength] += userRating;
    //         }
    //         else
    //         {
    //             filmLengthWeights[filmLength] = userRating;
    //         }
    //     }
    //
    //     // Проверка на ноль перед нормализацией
    //     if (totalRating != 0)
    //     {
    //         // Нормализация весов
    //         foreach (var length in filmLengthWeights.Keys.ToList())
    //         {
    //             filmLengthWeights[length] /= totalRating;
    //         }
    //     }
    //     else
    //     {
    //         // Если totalRating равен нулю, устанавливаем все веса равными нулю
    //         foreach (var length in filmLengthWeights.Keys.ToList())
    //         {
    //             filmLengthWeights[length] = 0;
    //         }
    //     }
    //
    //     return filmLengthWeights;
    // }
    //
    // public static Dictionary<string, double> CalculateAgeRatingWeights(List<Film> films)
    // {
    //     var ageRatingWeights = new Dictionary<string, double>();
    //     var totalRating = 0.0;
    //
    //     foreach (var film in films)
    //     {
    //         var userRating = film.Ratings.FirstOrDefault()?.Score ?? 0;
    //         totalRating += userRating;
    //
    //         var ageRating = film.RatingAgeLimits ?? "0+";
    //         if (ageRatingWeights.ContainsKey(ageRating))
    //         {
    //             ageRatingWeights[ageRating] += userRating;
    //         }
    //         else
    //         {
    //             ageRatingWeights[ageRating] = userRating;
    //         }
    //     }
    //
    //     // Проверка на ноль перед нормализацией
    //     if (totalRating != 0)
    //     {
    //         // Нормализация весов
    //         foreach (var rating in ageRatingWeights.Keys.ToList())
    //         {
    //             ageRatingWeights[rating] /= totalRating;
    //         }
    //     }
    //     else
    //     {
    //         // Если totalRating равен нулю, устанавливаем все веса равными нулю
    //         foreach (var rating in ageRatingWeights.Keys.ToList())
    //         {
    //             ageRatingWeights[rating] = 0;
    //         }
    //     }
    //
    //     return ageRatingWeights;
    // }
}