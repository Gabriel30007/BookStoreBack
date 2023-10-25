using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBLL.Managers.IManagers
{
    public interface IBucketManager
    {
        Task SaveOrderAsync(Guid userID, Guid productID);
    }
}
