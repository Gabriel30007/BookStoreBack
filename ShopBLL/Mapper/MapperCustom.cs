using System;
using UserDal = Shop.DAL.Entities.User;
using UserBLl = ShopBLL.Models.User;

using ProductDal = Shop.DAL.Entities.Product;
using ProductBLl = ShopBLL.Models.Product;
using System.Text;

namespace ShopBLL.Mapper
{
    public abstract class MapperCustom
    {
        public static UserBLl GetUserBllFromDll(UserDal userDal)
        {
            return new UserBLl(userDal.ID,userDal.Name,userDal.Email,userDal.Password,userDal.Roles);
        }
        public static  UserDal GetUserDllFromBll(UserBLl userBll)
        {
            return new UserDal(userBll.Id, userBll.Name, userBll.Email, userBll.Password, userBll.Roles);
        }
        public static ProductBLl GetProductBllFromDll(ProductDal productDal)
        {
            return new ProductBLl(productDal.ID, productDal.CreatedOn, productDal.Name, productDal.Price,productDal.Description, GetImgForProduct(productDal.PhotoID), productDal.Genre, productDal.authorID);
        }

        public static async Task< List<ProductBLl>> GetListProductsBllFromDll(List<ProductDal> productDal)
        {
            List<ProductBLl> arr = new List<ProductBLl>();
            foreach(ProductDal x in productDal)
            {
                arr.Add(GetProductBllFromDll(x));
            }
            return arr;
        }

        public static async Task<List<UserBLl>> GetListUsersBllFromDll(List<UserDal> userDal)
        {
            List<UserBLl> arr = new List<UserBLl>();
            foreach (UserDal x in userDal)
            {
                arr.Add(GetUserBllFromDll(x));
            }
            return arr;
        }

        public static string GetImgForProduct(Guid ID)
        {
            try
            {
                var file = Convert.ToBase64String(File.ReadAllBytes("../../../ProductImg/" + ID.ToString() + ".jpg"));

                return file;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

    }
}
