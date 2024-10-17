using Application.Films.Queries.GetFilmDetails;
using Application.Films.Queries.GetFilmList;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebApp.Controllers;

[ApiController]
[Route("api/films")]
public class FilmsController : BaseController
{
    
    private readonly IMapper _mapper;

    public FilmsController(IMapper mapper) => _mapper = mapper;

    [HttpGet("details")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<FilmDetailsVm>> Get(int id)
    {
        var query = new GetFilmDetailsQuery
        {
            Id = id
        };
        
        var vm = await Mediator.Send(query);

        if (vm is null)
        {
            Log.Information($"{typeof(FilmsController)}/details: No film details found for id: {id}");
            return NoContent();
        }
        return Ok(vm);
    }
    
    [HttpGet("films")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<FilmListVm>>> GetAll(int page = 1, int pageSize= 25)
    {
        var query = new GetFilmListQuery()
        {
            Page = page,
            PageSize = pageSize
        };
        
        var vm = await Mediator.Send(query);
        
        return Ok(vm);
    }
    
    // [HttpGet("rated")]
    // public async Task<ActionResult<List<string>>> GetRatedFilms()
    // {
    //     var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
    // }
}