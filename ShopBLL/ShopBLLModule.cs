using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopBLL.Manager;
using ShopBLL.Managers;
using ShopBLL.Managers.IManagers;

namespace ShopBLL
{
    public class ShopBLLModule
    {
        public static void Load(IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IBucketManager, BucketManager>();
        }
    }
}