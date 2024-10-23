using Application.Interfaces;
using Domain;

namespace Persistence.Services;

public class TrainingData
{
    public int ID { get; set; }
    public string Genre { get; set; }
    public int Year { get; set; }
    public int FilmLength { get; set; }
    public string AgeRating { get; set; }
    public bool Liked { get; set; } // Целевая переменная
}

public class RecommendationService : IRecommendationService
{
    private readonly IAppDbContext _context;
    private readonly IRecommendationModelService _recommendationModelService;
    private readonly IFacetWeightService _facetWeightService;

    public RecommendationService(
        IAppDbContext context,
        IRecommendationModelService recommendationModelService,
        IFacetWeightService facetWeightService
    )
    {
        _context = context;
        _recommendationModelService = recommendationModelService;
        _facetWeightService = facetWeightService;
    }

    public async Task<IList<Film>> GetFilmsForUser(Guid userId, CancellationToken cancellationToken)
    {
        var films = _context.Films.Take(100).ToList();
        var movies = _recommendationModelService.GetTopRecommendationsWithTitle(films, userId.GetHashCode(), 25);
        var f = _context.FacetWeights.FirstOrDefault(f => f.UserId == userId);
        return _facetWeightService.SortFilmsByUserFacets(movies, f);;
    }

    public Task<IList<Film>> GetRecentFilms(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IList<Film>> GetTopRatedFilms(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<Film>> GetFilmsRelatedToFilm(Guid? userId, int filmId, CancellationToken cancellationToken)
    {
        return new List<Film>();
    }
}