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
}
