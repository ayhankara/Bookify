using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using MediatR;

namespace Bookify.Application.Users.GetLoggedInUser;

public sealed record GetLoggedInUserQuery :IQuery<UserResponse>;