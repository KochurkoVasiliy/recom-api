using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Application.Users.Commands.Login;
using Application.Users.Commands.Register;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.User;

namespace WebApp.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : BaseController
{
    private readonly IMapper _mapper;

    public UserController(IMapper mapper) => _mapper = mapper;

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Register([FromBody]RegisterUserDto registerUserDto)
    {
        var query = new RegisterUserCommand()
        {
            Username = registerUserDto.Username,
            Email = registerUserDto.Email,
            PasswordHash = registerUserDto.PasswordHash
        };

        var vm = await Mediator.Send(query);

        return Ok();
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
    {
        var query = new LoginUserCommand()
        {
            Email = loginUserDto.Email,
            Password = loginUserDto.PasswordHash
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