using Asp.Versioning;
using Bookify.Application.Users.GetLoggedInUser;
using Bookify.Application.Users.LoginUser;
using Bookify.Application.Users.RegisterUser;
using Bookify.Domain.Abstractions;
using Bookify.Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Users;

[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/users")]
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
    [MapToApiVersion(ApiVersions.V1)]
    [HasPermission(Permisions.UsersRead)]
    public async Task<IActionResult> GetLoggedInUserV1(CancellationToken cancellationToken)
    {
       var query = new GetLoggedInUserQuery();
       Result<UserResponse> result = await _sender.Send(query,cancellationToken);
       return Ok(result.Value);
    }

    [HttpGet("me")]
    [MapToApiVersion(ApiVersions.V2)]
    [HasPermission(Permisions.UsersRead)]
    public async Task<IActionResult> GetLoggedInUserV2(CancellationToken cancellationToken)
    {
        var query = new GetLoggedInUserQuery();
        Result<UserResponse> result = await _sender.Send(query, cancellationToken);
        return Ok(result.Value);
    }
}

