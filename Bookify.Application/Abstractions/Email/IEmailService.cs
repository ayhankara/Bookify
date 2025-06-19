namespace Bookify.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendEmailAsync(Domain.Users.Email recipient, string subject, string body, CancellationToken cancellationToken = default);
}