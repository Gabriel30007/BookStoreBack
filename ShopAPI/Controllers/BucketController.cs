using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.DTO;
using ShopBLL.Managers.IManagers;
using System.Security.Claims;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BucketController : ControllerBase
    {
        private readonly IBucketManager _bucketManager;
        public BucketController(IBucketManager bucketManager)
        {
            _bucketManager = bucketManager;
        }

        [HttpPost]
        public async Task SaveOrder(BucketDto bucket)
        {
            await _bucketManager.SaveOrderAsync(bucket.UserID,bucket.ProductID);
        }

        [HttpGet]
        public async Task<dynamic> GetBucketInformation(Guid id)
        {
            return await _bucketManager.GetBucketInformationAsync(id);
        }
    }
}
