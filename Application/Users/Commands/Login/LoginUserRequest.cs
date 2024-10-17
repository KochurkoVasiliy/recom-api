using System.Net;

namespace Application.Users.Commands.Login;

public class LoginUserRequest
{
    public HttpStatusCode StatusCode { get; set; }
    public string Token { get; set; }
}