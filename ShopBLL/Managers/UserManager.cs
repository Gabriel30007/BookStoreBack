﻿using Microsoft.EntityFrameworkCore;
using Shop.DAL.AppDbContexts;
using Shop.DAL.Enums;
using ShopBLL.Managers.IManagers;
using ShopBLL.Models;
using ShopBLL.Mapper;
using System;
using System.Security.Cryptography.X509Certificates;
using Shop.DAL.Entities;
using UserDal = Shop.DAL.Entities.User;
using UserBLl = ShopBLL.Models.User;

namespace ShopBLL.Manager;

public class UserManager : IUserManager
{
    private readonly ShopContext _db;
    public UserManager(ShopContext db)
    {
        _db = db;
    }

    public async Task<UserBLl> getUserByEmailAsync(string email, string password)
    {
        try
        {
            //User user = new User(Guid.NewGuid(), "name", "email", "password", "User");
            dynamic user = MapperCustom.GetUserBllFromDll(await _db.User.Where(x => x.Email == email).DefaultIfEmpty().FirstAsync());

            if (user != null)
            {
                if (user.Password == password)
                {
                    return user;
                }

            }
            return new UserBLl(Guid.Empty, "", "", "", roles: Roles.NotLoggedIn.ToString());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task SignUpAsync(UserBLl user)
    {
        try
        {
            if (user.Name != null || user.Password != null || user.Email != null)
            {
                await _db.User.AddAsync(new UserDal
                {
                    ID = Guid.NewGuid(),
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Roles= Roles.ROLE_USER.ToString(),
                });
                _db.SaveChanges();
            }
            else
            {
                throw new Exception("User fields are required");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<List<UserBLl>> GetAllUserAsync()
    {
        return await MapperCustom.GetListUsersBllFromDll(_db.User.ToList());
    }


    public async Task<UserBLl> GetUserByIdAsync(Guid id)
    {
        UserDal user = await _db.User.FirstOrDefaultAsync(x => x.ID == id);
        return  MapperCustom.GetUserBllFromDll(user);
    }

    public async Task SaveUserAsync(UserBLl user)
    {
        _db.User.Update(MapperCustom.GetUserDllFromBll(user));
        await _db.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(string id)
    {
        try
        {
            UserDal user = new UserDal(Guid.Parse(id), "", "", "", "");
            _db.User.Remove(user);
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }
}