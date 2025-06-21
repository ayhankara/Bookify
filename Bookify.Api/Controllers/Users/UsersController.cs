using Bookify.Application.Users.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Users;

[Route("api/user")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;
    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterUserRequest request
        ,CancellationToken cancellationToken = default
        )

    {
        var command = new RegisterUserCommand(
            request.Email, 
            request.Password, 
            request.FirstName,
            request.LastName);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }    
        return Ok(result.Value);
    }
}

