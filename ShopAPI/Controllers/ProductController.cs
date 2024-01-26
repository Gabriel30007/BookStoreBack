using ShopBLL.Models;
using Microsoft.AspNetCore.Mvc;
using ShopBLL.Managers.IManagers;
using ShopAPI.DTO;

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

    [HttpPost]
    public async Task<dynamic> GetProductExtendData(ProductFilterContainerDto filters)
    {
        return await _productManager.GetProductExtendDataAsync(filters.Filters, filters.Page, filters.ItemsOnPage);
    }

    [HttpPost]
    public async Task<int> GetCountOfProducts(ProductFilterContainerDto filters)
    {
        return await _productManager.GetCountOfProductsAsync(filters.Filters);
    }

    [HttpGet]
    public async Task<dynamic> GetSingleExtendProduct(Guid ID)
    {
        return await _productManager.GetSingleExtendProductAsync(ID);
    }
}
