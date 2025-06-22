using Bookify.Application.Users.GetLoggedInUser;
using Bookify.Application.Users.LoginUser;
using Bookify.Application.Users.RegisterUser;
using Bookify.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Users;

[Route("api/users")]
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

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LogInUserRequest request, 
        CancellationToken cancellationToken = default)
    {
        var command = new LogInUserCommand(request.Email, request.Password);
        var result = await _sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpGet("me")]
    [Authorize(Roles = Roles.Registered)]
    public async Task<IActionResult> GetLoggedInUser(CancellationToken cancellationToken)
    {
       var query = new GetLoggedInUserQuery();
       Result<UserResponse> result = await _sender.Send(query,cancellationToken);
       return Ok(result.Value);
    }   
}

