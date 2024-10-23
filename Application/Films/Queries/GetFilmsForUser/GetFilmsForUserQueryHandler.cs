using Application.Films.Queries.GetFilmDetails;
using Application.Films.Queries.GetFilmsForUser;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Films.Queries.GetFilmsForUser;

public class GetFilmsForUserQueryHandler : IRequestHandler<GetFilmsForUserQuery, FilmsForUserVm>
{
    private readonly IAppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IRecommendationService _recommendationService;

    public GetFilmsForUserQueryHandler(IAppDbContext dbContext, IMapper mapper, IRecommendationService recommendationService) =>
        (_dbContext, _mapper, _recommendationService) = (dbContext, mapper, recommendationService);

    public async Task<FilmsForUserVm> Handle(GetFilmsForUserQuery request, CancellationToken cancellationToken)
    {
        var films = await _recommendationService.GetFilmsForUser(request.UserId, cancellationToken);
        var filmsDto = _mapper.Map<List<FilmsForUserLookupDto>>(films);
        
        
        return new FilmsForUserVm { Films = filmsDto };
    }
}