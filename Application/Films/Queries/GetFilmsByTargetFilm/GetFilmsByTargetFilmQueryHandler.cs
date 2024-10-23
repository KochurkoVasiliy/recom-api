using Application.Films.Queries.GetFilmsForUser;
using MediatR;

namespace Application.Films.Queries.GetFilmsByTargetFilm;

public class GetFilmsByTargetFilmQueryHandler : IRequestHandler<GetFilmsByTargetFilmQuery, FilmsByTargetFilmVm>
{
    public Task<FilmsByTargetFilmVm> Handle(GetFilmsByTargetFilmQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}