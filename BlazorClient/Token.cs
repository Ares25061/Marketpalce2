using BlazorClient;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using System.Net.Http.Json;
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

        public async Task<UserInfo?> NewTokenIfNeeded(UserInfo user)
        {
            var newUser = user;
            var decodedToken = await _jsRuntime.InvokeAsync<object>("jwt_decode", new[] { user.JwtToken });
            var TokenContent = JsonSerializer.Serialize(decodedToken, new JsonSerializerOptions { WriteIndented = true });
            var Token = JsonSerializer.Deserialize<JwtToken>(TokenContent);

            var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var remainingTime = Token.exp - currentTime;
            if (remainingTime <= 60 * 2) // 60 is const for because seconds
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri("https://oxygenmarketapi.onrender.com/")
                    
                };
                var refreshRequest = new RefreshTokenRequest { Token = user.RefreshToken };
                var response = await client.PostAsJsonAsync<RefreshTokenRequest>($"Accounts/refresh-token", refreshRequest);
                if (response.IsSuccessStatusCode)
                {
                    var authenticateResponse = await response.Content.ReadFromJsonAsync<AuthenticateResponse>();
                    newUser.JwtToken = authenticateResponse.JwtToken;
                    newUser.RefreshToken = authenticateResponse.RefreshToken;
                    return newUser;
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                    return null;
                }

            }
            return newUser;
        }

    }

    public class RefreshTokenRequest
    {
        public string Token { get; set; }
    }

    public class JwtToken
    {
        public string? id { get; set; }
        public long nbf { get; set; }
        public long exp { get; set; }
        public long iat { get; set; }

    }
}