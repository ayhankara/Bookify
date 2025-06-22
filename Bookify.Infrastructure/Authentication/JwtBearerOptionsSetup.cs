using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Bookify.Infrastructure.Authentication;

internal sealed class JwtBearerOptionsSetup :IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly AuthenticationOptions _authenticationOptions;
    public JwtBearerOptionsSetup(IOptions<AuthenticationOptions> authenticationOptions)
    {
        _authenticationOptions = authenticationOptions.Value;
    }
    public void Configure(JwtBearerOptions options)
    {
       options.Audience = _authenticationOptions.Audience;
       options.MetadataAddress = _authenticationOptions.MetadataUrl;
       options.RequireHttpsMetadata = _authenticationOptions.RequireHttpsMetadata;
       options.TokenValidationParameters.ValidIssuer = _authenticationOptions.ValidIssuer;
       options.TokenValidationParameters.ValidateIssuer = true;
       options.TokenValidationParameters.ValidateAudience = true;
       options.TokenValidationParameters.ValidateLifetime = true;
       options.TokenValidationParameters.ValidateIssuerSigningKey = true;

    }
    public void Configure(string? name, JwtBearerOptions options) => Configure(options);

}