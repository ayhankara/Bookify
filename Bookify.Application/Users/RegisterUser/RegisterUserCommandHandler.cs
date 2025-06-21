using Bookify.Application.Abstractions.Authentication;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Users;

namespace Bookify.Application.Users.RegisterUser;

public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthenticationService _authhenticationService;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork, 
        IAuthenticationService authhenticationService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _authhenticationService = authhenticationService;
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
      var user = User.Create(
          new FirstName(request.FirstName),
          new LastName(request.LastName),
          new Email(request.Email)
          );

       var identityId = await _authhenticationService.RegisterAsync(user, request.Password, cancellationToken);
       user.SetIdentityId(identityId);

       _userRepository.Add(user);
       
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}