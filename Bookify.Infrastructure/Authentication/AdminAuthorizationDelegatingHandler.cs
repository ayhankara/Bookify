using Bookify.Infrastructure.Authentication.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Bookify.Infrastructure.Authentication
{
    public sealed class AdminAuthorizationDelegatingHandler : DelegatingHandler


    {
        private readonly KeycloakOptions _keycloakOptions;


        public AdminAuthorizationDelegatingHandler(IOptions<KeycloakOptions> keycloakOptions)
        {
            _keycloakOptions = keycloakOptions.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            try
            {
                var authorizationToken = await GetAuthorizationToken(cancellationToken);

                request.Headers.Authorization = new AuthenticationHeaderValue(
                    JwtBearerDefaults.AuthenticationScheme,
                    authorizationToken.AccessToken);

                var httpResponseMessage = await base.SendAsync(request, cancellationToken);

                httpResponseMessage.EnsureSuccessStatusCode();

                return httpResponseMessage;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request failed: {ex.Message}");
                Console.WriteLine($"Request URI: {request.RequestUri}");
                Console.WriteLine($"Request Method: {request.Method}");
                Console.WriteLine($"Status Code: {ex.StatusCode}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }

        private async Task<AuthorizationToken> GetAuthorizationToken(CancellationToken cancellationToken)
        {
            var authorizationRequestParameters = new KeyValuePair<string, string>[]
            {
                new("client_id", _keycloakOptions.AdminClientId),
                new("client_secret", _keycloakOptions.AdminClientSecret),
                new("scope", "openid email profile"),
                new("grant_type", "client_credentials")
            };

            var authorizationRequestContent = new FormUrlEncodedContent(authorizationRequestParameters); 

            using var authorizationRequest = new HttpRequestMessage(
                HttpMethod.Post,
                new Uri(_keycloakOptions.TokenUrl))
            {
                Content = authorizationRequestContent
            };
            authorizationRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            authorizationRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            HttpResponseMessage authorizationResponse = await base.SendAsync(authorizationRequest, cancellationToken);

            authorizationResponse.EnsureSuccessStatusCode();


            return await authorizationResponse.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken) ??
                   throw new ApplicationException();
        }
    }

}
