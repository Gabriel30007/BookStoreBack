using Microsoft.EntityFrameworkCore;
using Shop.DAL.AppDbContexts;
using ShopBLL.Managers.IManagers;
using ShopBLL.Models;
using ShopBLL.Mapper;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace ShopBLL.Managers;

public class ProductManager : IProductManager
{
    private readonly ShopContext _db;
    public ProductManager(ShopContext db)
    {
        _db = db;
    }
    public async Task<List<Product>> GetAllProductAsync()
    {

        return await MapperCustom.GetListProductsBllFromDll(_db.Product.ToList());
    }
    public async Task<Product> GetSingleProductAsync(Guid ID)
    {
        return MapperCustom.GetProductBllFromDll(await _db.Product.FindAsync(ID));
    }

    public async Task<dynamic> GetProductExtendDataAsync(Dictionary<string, string> filters, int page, int itemsOnPage)
    {
        try
        {
            bool order = false;
            if (filters.ContainsKey("Order"))
            {
                order = filters["Order"] == "true" ? true : false;
            }

            var data = _db.Product.Join(
            _db.Author,
            p => p.authorID,
            a => a.Id,
            (p, a) => new
            {
                ID = p.ID,
                CreatedOn = p.CreatedOn,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                PhotoID = MapperCustom.GetImgForProduct(p.PhotoID),
                Genre = p.Genre,
                AuthorID = p.authorID,
                AuthorName = a.Name

            })
                .AsQueryable();

            if (filters.ContainsKey("Genre") && filters["Genre"] != "")
            {
                string [] genres = filters["Genre"].Split(";");
                genres = genres.Where(x => x != "").ToArray();                
                data = data.Where((p) => genres.Contains(p.Genre));
            }

            if (!order)
            {
                data = data.OrderBy(p => p.Price);
            }
            else
            {
                data = data.OrderByDescending(p => p.Price);
            }

            dynamic returnData = await data.Skip(page * itemsOnPage)
                .Take(itemsOnPage).ToArrayAsync();
            return returnData;
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    public async Task<int> GetCountOfProductsAsync(Dictionary<string, string> filters)
    {
        int count = 0;
        if (filters.ContainsKey("Genre") && filters["Genre"] != "")
        {
            string[] genres = filters["Genre"].Split(";");
            genres = genres.Where(x => x != "").ToArray();
            count =await _db.Product.Where((p) => genres.Contains(p.Genre)).CountAsync();
        }
        else
        {
            count = await _db.Product.CountAsync();
        }
        return count;
    }
    
}
