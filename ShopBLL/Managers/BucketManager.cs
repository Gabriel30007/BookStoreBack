using Microsoft.EntityFrameworkCore;
using Shop.DAL.AppDbContexts;
using Shop.DAL.Entities;
using ShopBLL.Managers.IManagers;
using ShopBLL.Mapper;

namespace ShopBLL.Managers
{
    public class BucketManager : IBucketManager
    {
        private readonly ShopContext _db;
        public BucketManager(ShopContext db)
        {
            _db = db;
        }
        
        public async Task SaveOrderAsync(Guid userID, Guid productID)
        { 
            decimal price = _db.Product.FindAsync(productID).Result.Price;
            await _db.Bucket.AddAsync(new Bucket(Guid.NewGuid(), DateTime.Now,userID, productID, price));
            _db.SaveChanges();
        }

        public async Task<dynamic> GetBucketInformationAsync(Guid userID)
        {
            dynamic data = await(from b in _db.Bucket
                           join p in _db.Product on b.productID equals p.ID
                           join a in _db.Author on p.authorID equals a.Id
                           select new 
                           {
                               ID = b.ID,
                               CreatedOn = b.CreatedOn,
                               ProductID = p.ID,
                               ProductName = p.Name,
                               ProductPhoto = MapperCustom.GetImgForProduct(p.PhotoID),
                               Price = b.Price,
                               AuthorID = a.Id,
                               AuthorName = a.Name
                           }).ToArrayAsync();
            return data;
        }
    }
}
    