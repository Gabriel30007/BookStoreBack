using Microsoft.EntityFrameworkCore;
using Shop.DAL.AppDbContexts;
using ShopBLL.Managers.IManagers;
using ShopBLL.Models;
using ShopBLL.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
