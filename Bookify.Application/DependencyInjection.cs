using Bookify.Application.Abstractions.Behaviors;
using Bookify.Domain.Bookings;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bookify.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly,includeInternalTypes:true);

        services.AddTransient<PricingService>();

        return services;
    }

}