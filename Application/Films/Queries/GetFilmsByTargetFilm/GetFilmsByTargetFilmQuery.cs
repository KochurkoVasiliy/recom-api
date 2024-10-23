using MediatR;

namespace Application.Films.Queries.GetFilmsByTargetFilm;

public class GetFilmsByTargetFilmQuery : IRequest<FilmsByTargetFilmVm>
{
    public Guid? UserId { get; set; }
    public int FilmId { get; set; }
}