using Bookify.Application.Abstractions.Email;

namespace Bookify.Infrastructure.Email;

internal sealed class EmailService : IEmailService
{
    public Task SendEmailAsync(Domain.Users.Email recipient, string subject, string body, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask; // Placeholder for actual email sending logic
    }
}