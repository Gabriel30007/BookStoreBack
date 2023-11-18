
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
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
    private const string GoogleScope = "https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email";
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
    public async Task<User> Code(OAuthDTO dto)
    {
        try
        {

            string headerValues = Request.Headers.Authorization;
          
            var codeVerifier = HttpContext.Session.GetString(PkceSessionKey);
            
            var tokenResult = await GoogleAuthService.ExchangeCodeOnTokenAsync(dto.Code, headerValues, RedirectUrl);
            UserInfoDTO userInfo = await GoogleAuthService.GetUserEmail(tokenResult.AccessToken);

            User userDB = await _userManager.CheckIsEmailRegistered(userInfo.Email);

            if(userDB != null)
            {
                await _userManager.RefreshTokenInDatabase(userDB.Id, tokenResult.RefreshToken);
            }
            else
            {
                userDB = await _userManager.SaveUserByOAuthAsync(new User(Guid.NewGuid(), userInfo.Name, userInfo.Email, tokenResult.RefreshToken));
            }

            // оновлюємо Токен Доступу за допомогою Refresh-токена.
            //var refreshedTokenResult = await GoogleAuthService.RefreshTokenAsync(tokenResult.RefreshToken);

            return userDB;
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
