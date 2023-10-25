using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.DAL.AppDbContexts;
using System;

namespace Shop.DAL;

public class ShopDalModule
{
    public static void Load(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ShopContext>(option => { 
            option.UseSqlServer(configuration.GetConnectionString("AngularStudyDbConnectionString"));
            option.EnableSensitiveDataLogging();
        }
            );
    }
}