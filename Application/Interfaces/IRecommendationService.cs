using Domain;

namespace Application.Interfaces;

public interface IRecommendationService
{
    Task<IList<Film>> GetFilmsForUser(Guid userId, CancellationToken cancellationToken);
    Task<IList<Film>> GetRecentFilms(CancellationToken cancellationToken);
    Task<IList<Film>> GetTopRatedFilms(CancellationToken cancellationToken);
    Task<IList<Film>> GetFilmsRelatedToFilm(Guid? userId, int filmId, CancellationToken cancellationToken);
}