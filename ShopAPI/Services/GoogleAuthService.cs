using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using ShopAPI.DTO;
using ShopAPI.Helpers;

namespace ShopAPI.Services;

public class GoogleAuthService
{
    private static IConfiguration _configuration;

    private const string OAuthServerEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
    private const string TokenServerEndpoint = "https://oauth2.googleapis.com/token";
    private const string ProfileServerEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";

    public GoogleAuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static string GenerateOAuthRequestUrl(string scope, string redirectUrl, string codeChellange)
    {
        string ClientId = ConfigurationHelper.config.GetSection("GoogleSecrets:ClientId").Value;
        var queryParams = new Dictionary<string, string>
        {
            {"client_id", ClientId},
            { "redirect_uri", redirectUrl },
            { "response_type", "code" },
            { "scope", scope },
            { "code_challenge", codeChellange },
            { "code_challenge_method", "S256" },
            { "access_type", "offline" }
        };

        var url = QueryHelpers.AddQueryString(OAuthServerEndpoint, queryParams);
        return url;
    }

    public static async Task<TokenResult> ExchangeCodeOnTokenAsync(string code, string codeVerifier, string redirectUrl)
    {
        string ClientId = ConfigurationHelper.config.GetSection("GoogleSecrets:ClientId").Value;
        string ClientSecret = ConfigurationHelper.config.GetSection("GoogleSecrets:ClientSecret").Value;
        var authParams = new Dictionary<string, string>
        {
            { "client_id", ClientId },
            { "client_secret", ClientSecret },
            { "code", code },
            { "code_verifier", codeVerifier },
            { "grant_type", "authorization_code" },
            { "redirect_uri", redirectUrl }
        };

        var tokenResult = await HttpClientHelper.SendPostRequest<TokenResult>(TokenServerEndpoint, authParams);
        return tokenResult;
    }

    public static async Task<TokenResult> RefreshTokenAsync(string refreshToken)
    {
        string ClientId = ConfigurationHelper.config.GetSection("GoogleSecrets:ClientId").Value;
        string ClientSecret = ConfigurationHelper.config.GetSection("GoogleSecrets:ClientSecret").Value;
        var refreshParams = new Dictionary<string, string>
        {
            { "client_id", ClientId },
            { "client_secret", ClientSecret },
            { "grant_type", "refresh_token" },
            { "refresh_token", refreshToken }
        };

        var tokenResult = await HttpClientHelper.SendPostRequest<TokenResult>(TokenServerEndpoint, refreshParams);

        return tokenResult;
    }

    public static async Task<string> GetUserEmail(string accessToken)
    {
        string ClientId = ConfigurationHelper.config.GetSection("GoogleSecrets:ClientId").Value;
        string ClientSecret = ConfigurationHelper.config.GetSection("GoogleSecrets:ClientSecret").Value;
        
        var tokenResult = await HttpClientHelper.SendGetRequest<dynamic>(ProfileServerEndpoint, null,accessToken);


        return tokenResult;
    }
}
