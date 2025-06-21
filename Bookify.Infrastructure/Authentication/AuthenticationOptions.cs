using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Authentication;

public sealed class AuthenticationOptions
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string MetadataUrl { get; init; } = string.Empty;
    public bool RequireHttpsMetadata { get; init; }
    public int JwtExpirationInMinutes { get; set; } = 60;
    public string JwtRefreshTokenSecret { get; set; } = string.Empty;
    public int JwtRefreshTokenExpirationInDays { get; set; } = 30;
}

