using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using ShopAPI.DTO;
using ShopAPI.Helpers;
using ShopAPI.Services;
using ShopBLL.Managers.IManagers;
using ShopBLL.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IUserManager _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly NotificationService _notificationService;
    private const string RedirectUrl = "http://localhost:4200/home";
    private const string GoogleScope = "https://www.googleapis.com/auth/userinfo.profile";
    private const string PkceSessionKey = "codeVerifier";
    public AuthenticationController(IUserManager userManager, IHttpContextAccessor httpContextAccessor, NotificationService notificationService)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _notificationService = notificationService;
    }
    [HttpPost]
    public string RedirectOnOAuthServer(string CodeVerifier)
    {
        try
        {
            var codeChellange = Sha256Helper.ComputeHash(CodeVerifier);
     

            var url = GoogleAuthService.GenerateOAuthRequestUrl(GoogleScope, RedirectUrl, codeChellange);
            return "\"" + url+"\"";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        // PCKE.
    }
    [HttpPost]
    public async Task<IActionResult> Code(OAuthDTO dto)
    {
        try
        {

            string headerValues = Request.Headers.Authorization;
          
            var codeVerifier = HttpContext.Session.GetString(PkceSessionKey);
            
            var tokenResult = await GoogleAuthService.ExchangeCodeOnTokenAsync(dto.Code, headerValues, RedirectUrl);

            //var myChannelId = await YoutubeService.GetMyChannelIdAsync(tokenResult.AccessToken);


            //await YoutubeService.UpdateChannelDescriptionAsync(tokenResult.AccessToken, myChannelId, newDescription);

            // Почекаємо 3600 секунд
            // (саме стільки можна використовувати AccessToken, поки його термін придатності не спливе).

            // І оновлюємо Токен Доступу за допомогою Refresh-токена.
            var refreshedTokenResult = await GoogleAuthService.RefreshTokenAsync(tokenResult.RefreshToken);

            return Ok();
        }
        catch(Exception ex)
        {
            throw ex;
        }
        
    }

    [HttpGet]
    public string test()
    {
        _notificationService.SendNotification("test");
        HttpContext.Session.Set("What", new byte[] { 1, 2, 3, 4, 5 });
        return "Api is working correct!";

    }

    [HttpPost]
    public async Task<User> signin(User user)
    {
        return await _userManager.getUserByEmailAsync(user.Email, user.Password);
    }

    [HttpPost]
    public IActionResult signout()
    {
        return Ok();
    }

    [HttpPost]
    public async Task signUp(User user)
    {
        try
        {
            await _userManager.SignUpAsync(user);
        }
        catch(Exception ex) 
        {
            throw ex;
        }   
    }
}
