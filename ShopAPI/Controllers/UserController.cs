using Microsoft.AspNetCore.Mvc;
using ShopAPI.Services;
using ShopBLL.Managers.IManagers;
using ShopBLL.Models;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserManager _userManager;
    private readonly NotificationService _notificationService;

    public UserController(IUserManager userManager, NotificationService notificationService)
    {
        _userManager = userManager;
        _notificationService = notificationService;
    }

    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    [HttpGet]
    public async Task<List<User>> GetAllUsers()
    {
        return await _userManager.GetAllUserAsync();
    }

    [HttpGet]
    public async Task<User> GetUserById(string id)
    {
        return await _userManager.GetUserByIdAsync(Guid.Parse(id));
    }

    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    [HttpPost]
    public async Task<OkResult> SaveUser(User user)
    {
        await _userManager.SaveUserAsync(user);
        _notificationService.SendNotification(user.Name);
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            await _userManager.DeleteUserAsync(id);
            return Ok();
        }
        catch(Exception ex)
        {
            return StatusCode(500);
        }
    }
}
