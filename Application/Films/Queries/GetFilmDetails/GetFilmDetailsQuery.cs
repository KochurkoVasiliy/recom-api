using MediatR;

namespace Application.Films.Queries.GetFilmDetails;

public class GetFilmDetailsQuery : IRequest<FilmDetailsVm?>
{
    public int Id {get; set;}
}