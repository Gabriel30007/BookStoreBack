﻿using Shop.DAL.AppDbContexts;
using Shop.DAL.Entities;
using ShopBLL.Managers.IManagers;

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
            await _db.Bucket.AddAsync(new Bucket(Guid.NewGuid(), DateTime.Now,userID, productID));
            _db.SaveChanges();
        }

    }
}