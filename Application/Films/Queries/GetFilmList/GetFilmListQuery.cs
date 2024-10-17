using MediatR;

namespace Application.Films.Queries.GetFilmList;

public class GetFilmListQuery : IRequest<List<FilmListVm>>, IRequest<FilmListVm>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}