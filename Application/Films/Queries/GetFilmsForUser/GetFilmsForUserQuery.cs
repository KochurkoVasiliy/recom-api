using Application.Films.Queries.GetFilmsForUser;
using MediatR;

namespace Application.Films.Queries.GetFilmsForUser;

public class GetFilmsForUserQuery : IRequest<FilmsForUserVm>
{
    public Guid UserId { get; set; }
}