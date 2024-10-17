using Application.Films.Queries.GetFilmDetails;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Films.Queries.GetFilmList;

public class GetFilmListQueryHandler : IRequestHandler<GetFilmListQuery, List<FilmListVm>>
{
    private readonly IAppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetFilmListQueryHandler(IAppDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<FilmListVm>> Handle(GetFilmListQuery request, CancellationToken cancellationToken)
    {
        List<Film> films = await _dbContext.Films
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        
        var filmListVm = _mapper.Map<List<FilmListVm>>(films);

        return filmListVm;
    }
}