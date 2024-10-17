using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Application.Users.Commands.Login;
using Application.Users.Commands.Register;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : BaseController
{
    private readonly IMapper _mapper;

    public UserController(IMapper mapper) => _mapper = mapper;

    [HttpGet("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Register(string username, string email, string password)
    {
        var query = new RegisterUserCommand()
        {
            Username = username,
            Email = email,
            PasswordHash = password
        };

        var vm = await Mediator.Send(query);

        return Ok();
    }

    [HttpGet("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(string email, string password)
    {
        var query = new LoginUserCommand()
        {
            Email = email,
            Password = password
        };

        var vm = await Mediator.Send(query);
        
        if (vm.StatusCode == HttpStatusCode.Unauthorized)
            return Unauthorized();
        if(vm.StatusCode == HttpStatusCode.NotFound)
            return NotFound();
        
        Response.Cookies.Append("authToken", vm.Token);
        return Ok();
    }
    
    [Authorize]
    [HttpGet("info")]
    public ActionResult<string> Info()
    {
        return Ok(UserId);
    }
}