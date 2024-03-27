using ShopBLL.Models;
using Microsoft.AspNetCore.Mvc;
using ShopBLL.Managers.IManagers;
using ShopAPI.DTO;
using Microsoft.Extensions.Primitives;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

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
    
    [HttpGet]
    public async Task<dynamic> GetGenres()
    {
        return await _productManager.GetGenresAsync();
    }    
    [HttpGet]
    public async Task<dynamic> GetAuthors()
    {
        return await _productManager.GetAuthorsAsync();
    }

    [HttpPost]
    public async Task<OkResult> SaveProduct([FromForm] IFormFile file)
    {
        try
        {
            StringValues id = "";
            StringValues name = "";
            StringValues description = "";
            StringValues price = "";
            StringValues genreID = "";
            StringValues authorID = "";
            StringValues photoIDstr = "";

            Request.Form.TryGetValue("id", out id);
            Request.Form.TryGetValue("name", out name);
            Request.Form.TryGetValue("description", out description);
            Request.Form.TryGetValue("price", out price);
            Request.Form.TryGetValue("genre", out genreID);
            Request.Form.TryGetValue("author", out authorID);
            Request.Form.TryGetValue("photoID", out photoIDstr);
             
            Guid photoID = Guid.Parse(photoIDstr);
          


            await _productManager.SaveProductAsync(new Product(Guid.Parse(id), DateTime.Now, name, Decimal.Parse(price), description, photoID.ToString(), Guid.Parse(genreID), Guid.Parse(authorID)));

            using (var stream = System.IO.File.Create("../../../ProductImg/" + photoID.ToString() + ".jpg"))
            {
                await file.CopyToAsync(stream);
            }
            return Ok();
        }
        catch(Exception ex)
        {
            throw ex;
        }
        
    }

    [HttpPost]
    public async Task<OkResult> DeleteProduct(Guid id)
    {
        await _productManager.DeleteProductAsync(id);
        return Ok();
    }
}
