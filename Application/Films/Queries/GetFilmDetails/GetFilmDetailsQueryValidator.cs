using FluentValidation;

namespace Application.Films.Queries.GetFilmDetails;

public class GetFilmDetailsQueryValidator: AbstractValidator<GetFilmDetailsQuery>
{
    public GetFilmDetailsQueryValidator()
    {
    }
}