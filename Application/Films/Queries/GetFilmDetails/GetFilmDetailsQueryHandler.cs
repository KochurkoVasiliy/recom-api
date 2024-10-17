using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Films.Queries.GetFilmDetails;

public class GetFilmDetailsQueryHandler
    : IRequestHandler<GetFilmDetailsQuery, FilmDetailsVm?>
{
    
    private readonly IAppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetFilmDetailsQueryHandler(IAppDbContext dbContext,
        IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);
    
    public async Task<FilmDetailsVm?> Handle(GetFilmDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Films.FirstOrDefaultAsync(film => film.Id == request.Id, cancellationToken);
        
        return _mapper.Map<FilmDetailsVm>(entity);

    }
}