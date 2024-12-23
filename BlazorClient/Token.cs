using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text.Json;
using System.Net.Http;
using System.Text;

namespace Models
{
    public class TokenRefresher
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;

        public TokenRefresher(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
        }

        public async Task<string?> NewTokenIfNeeded(string? accessToken, string? refreshToken)
        {
            var decodedToken = await _jsRuntime.InvokeAsync<object>("jwt_decode", new[] { accessToken });
            var tokenContent = JsonSerializer.Serialize(decodedToken, new JsonSerializerOptions { WriteIndented = true });
            var token = JsonSerializer.Deserialize<JwtToken>(tokenContent);

            var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var remainingTime = token.exp - currentTime;

            if (remainingTime <= 60 * 14.9)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "/Accounts/refresh-token");
                request.Content = new StringContent(JsonSerializer.Serialize(new { RefreshToken = refreshToken }), Encoding.UTF8, "application/json");

                request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var authenticateResponse = await response.Content.ReadFromJsonAsync<AuthenticateResponse>();
                    return authenticateResponse?.JwtToken;
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                    return null;
                }
            }

            return accessToken;
        }
    }

    public class JwtToken
    {
        public string? id { get; set; }
        public long nbf { get; set; }
        public long exp { get; set; }
        public long iat { get; set; }
    }
}