using System.Net;
using System.Net.Http.Json;
using Bookify.Application.Abstractions.Authentication;
using Bookify.Domain.Abstractions;
using Bookify.Infrastructure.Authentication.Models;
using Microsoft.Extensions.Options;

namespace Bookify.Infrastructure.Authentication;

internal sealed class JwtService : IJwtService
{
    private static readonly Error AuthenticationFailed = new ("Keycloak", "Authentication failed. Please check your credentials.");
    private readonly HttpClient _httpClient;
    private readonly KeycloakOptions _keycloakOptions;

    public JwtService(HttpClient httpClient, IOptions<KeycloakOptions> keycloakOptions)
    {
        _httpClient = httpClient;
        _keycloakOptions = keycloakOptions.Value;
    }

    public async Task<Result<string>> GenerateAccessTokenAsync(
        string email,
        string password, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var authRequestParameters = new KeyValuePair<string, string>[]
            {
                new("client_id", _keycloakOptions.AuthClientId),
                new("client_secret", _keycloakOptions.AuthClientSecret),
                new("username", email),
                new("password", password),
                new("grant_type", "password"),
                new("scope", "openid email")

            };

            var authorizationRequestContent = new FormUrlEncodedContent(authRequestParameters);
            var response = await _httpClient.PostAsync("", authorizationRequestContent, cancellationToken);

            response.EnsureSuccessStatusCode();

            var authorizationToken = await response.Content.ReadFromJsonAsync<AuthorizationToken>();
            if (authorizationToken is null || string.IsNullOrEmpty(authorizationToken.AccessToken))
            {
                return Result.Failure<string>(AuthenticationFailed);
            }

            return Result.Success(authorizationToken.AccessToken);

        }
        catch (HttpRequestException)
        {

            return Result.Failure<string>(AuthenticationFailed);
        }
      
    }
}