using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBLL.Managers.IManagers
{
    public interface IBookSearchAI
    {
        Task FindBookAsync(string promt, string apikey);
    }
}
