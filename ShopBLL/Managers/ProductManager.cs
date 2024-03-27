using Microsoft.EntityFrameworkCore;
using Shop.DAL.AppDbContexts;
using ShopBLL.Managers.IManagers;
using ShopBLL.Models;
using ShopBLL.Mapper;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using ProductDal = Shop.DAL.Entities.Product;
using ProductBLl = ShopBLL.Models.Product;

namespace ShopBLL.Managers;

public class ProductManager : IProductManager
{
    private readonly ShopContext _db;
    public ProductManager(ShopContext db)
    {
        _db = db;
    }
    public async Task<List<ProductBLl>> GetAllProductAsync()
    {

        return await MapperCustom.GetListProductsBllFromDll(_db.Product.ToList());
    }
    public async Task<ProductBLl> GetSingleProductAsync(Guid ID)
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
                GenreID = p.genreID,
                AuthorID = p.authorID,
                AuthorName = a.Name

            }).Join(_db.Genre, o => o.GenreID,
                                g => g.ID, (o,g)=> new
                                {
                                    ID = o.ID,
                                    CreatedOn = o.CreatedOn,
                                    Name = o.Name,
                                    Price = o.Price,
                                    Description = o.Description,
                                    PhotoID = o.PhotoID,
                                    GenreID = g.ID,
                                    Genre = g.Name,
                                    AuthorID = o.AuthorID,
                                    AuthorName = o.Name
                                })
                .AsQueryable();

            if (filters.ContainsKey("Genre") && filters["Genre"] != "")
            {
                string [] genres = filters["Genre"].Split(";");
                genres = genres.Where(x => x != "").ToArray();                
                data = data.Where((p) => genres.Contains(p.GenreID.ToString()));
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
            count =await _db.Product.Where((p) => genres.Contains(p.genreID.ToString())).CountAsync();
        }
        else
        {
            count = await _db.Product.CountAsync();
        }
        return count;
    }

    public async Task<dynamic> GetSingleExtendProductAsync(Guid id)
    {
        try
        {
           var data = await _db.Product.Join(
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
               PhotoID = p.PhotoID,
               PhotoStr = MapperCustom.GetImgForProduct(p.PhotoID),
               GenreID = p.genreID,
               AuthorID = p.authorID,
               AuthorName = a.Name

           }).Join(_db.Genre, o => o.GenreID,
                                g => g.ID, (o, g) => new
                                {
                                    ID = o.ID,
                                    CreatedOn = o.CreatedOn,
                                    Name = o.Name,
                                    Price = o.Price,
                                    Description = o.Description,
                                    PhotoID = o.PhotoID,
                                    PhotoStr = o.PhotoStr,
                                    GenreID = g.ID,
                                    Genre = g.Name,
                                    AuthorID = o.AuthorID,
                                    AuthorName = o.Name
                                }).Where(x => x.ID == id)
           .FirstOrDefaultAsync();

            if (data == null)
            {
                throw new Exception("Продукт не знайдено");
            }
            return data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
       

    }
    public async Task<Array> GetGenresAsync()
    {
        return await _db.Genre.ToArrayAsync();
    }    
    public async Task<Array> GetAuthorsAsync()
    {
        return await _db.Author.ToArrayAsync();
    }

    public async Task SaveProductAsync(ProductBLl product)
    {
        try
        {
            if (!(product is null))
            {
                _db.Product.Update(MapperCustom.GetProductDALFromBLL(product));
                await _db.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
        
    }

    public async Task DeleteProductAsync(Guid ID)
    {
        try
        {
            ProductDal product = await _db.Product.FindAsync(ID);

            if (product != null)
            {
                _db.Product.Remove(product);
                await _db.SaveChangesAsync();
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }

    }

    //public async Task UpdateProductAsync(ProductBLl product)
    //{
    //    try
    //    {
    //        _db.Product.Update(MapperCustom.GetProductDALFromBLL(product));
    //        await _db.SaveChangesAsync();
    //    }
    //    catch(Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //public async Task<Product> GetProductByIDAsync(Guid id)
    //{
    //    try
    //    {
    //        return MapperCustom.GetProductBllFromDll(await _db.Product.Where(x=>x.ID == id).FirstOrDefaultAsync());
    //    }
    //    catch(Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
}
