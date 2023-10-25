using ShopBLL.Models;
using Microsoft.AspNetCore.Mvc;
using ShopBLL.Managers.IManagers;

namespace ShopAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductManager _productManager;
    public ProductController(IProductManager productManager)
    {
        _productManager = productManager;
    }

    [HttpGet]
    public async Task<List<Product>> GetAllProducts()
    {

       return await _productManager.GetAllProductAsync();
    }
    [HttpGet]
    public async Task<Product> GetSingleProduct(Guid ID)
    {
        return await _productManager.GetSingleProductAsync(ID);
    }
}
