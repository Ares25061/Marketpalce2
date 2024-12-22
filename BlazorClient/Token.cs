using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Models
{
    public class TokenRefresher
    {
        private readonly IJSRuntime _jsRuntime;
        public TokenRefresher(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string?> NewTokenIfNeeded(string? accessToken)
        {
            var decodedToken = await _jsRuntime.InvokeAsync<object>("jwt_decode", new[] { accessToken });
            var TokenContent = JsonSerializer.Serialize(decodedToken, new JsonSerializerOptions { WriteIndented = true });
            var Token = JsonSerializer.Deserialize<JwtToken>(TokenContent);

            var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var remainingTime = Token.exp - currentTime;
            if (remainingTime <= 60 * 14.9) // 60 is const for because seconds
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "/Accounts/refresh-token");

                request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
                HttpResponseMessage response = await new HttpClient { BaseAddress = new Uri("https://oxygenmarketapi.onrender.com/") }.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return (await response.Content.ReadFromJsonAsync<AuthenticateResponse>()).JwtToken;
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