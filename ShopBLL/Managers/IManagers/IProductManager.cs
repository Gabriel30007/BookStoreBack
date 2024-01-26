using ShopBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBLL.Managers.IManagers;

public interface IProductManager
{
    Task<List<Product>> GetAllProductAsync();
    Task<Product> GetSingleProductAsync(Guid ID);
    Task<dynamic> GetProductExtendDataAsync(Dictionary<string, string> filters, int page, int itemsOnPage);
    Task<int> GetCountOfProductsAsync(Dictionary<string, string> filters);
    Task<dynamic> GetSingleExtendProductAsync(Guid id);
}
