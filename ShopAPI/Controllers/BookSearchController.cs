using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Helpers;
using ShopBLL.Managers.IManagers;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookSearchController : ControllerBase
    {
        private readonly IBookSearchAI _bookSearchAIManager;
        public BookSearchController(IBookSearchAI bookSearchAIManager)
        {
            _bookSearchAIManager = bookSearchAIManager;
        }

        [HttpPost]
        public async Task<IActionResult> FindBook(string promt)
        {
            await _bookSearchAIManager.FindBookAsync(promt, ConfigurationHelper.config.GetSection("OpenAi:ApiKey").Value);
            return StatusCode(500);
        }
    }
}
