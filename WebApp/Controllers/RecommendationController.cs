using Application.Films.Queries.GetFilmsForUser;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/recommendations")]
public class RecommendationController : BaseController
{
    private readonly IMapper _mapper;

    public RecommendationController(IMapper mapper) => _mapper = mapper;
    
    [HttpGet("popular")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<string> GetMostPopularFilms()
    {
        return Ok(UserId);
    }
    
    [HttpGet("newer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<string> GetNewerFilms()
    {
        return Ok(UserId);
    }
    
    [Authorize]
    [HttpGet("for-user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> GetFilmsForUser()
    {
        
        var query = new GetFilmsForUserQuery()
        {
            UserId = this.UserId
        };
        
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }
}