using ShopBLL.Models;
using System.Threading.Tasks;

namespace ShopBLL.Managers.IManagers;

public interface IUserManager
{
    Task<User> getUserByEmailAsync(string email, string password);
    Task<List<User>> GetAllUserAsync();
    Task SignUpAsync(User user);
    Task<User> GetUserByIdAsync(Guid id);
    Task SaveUserAsync(User user);
    Task DeleteUserAsync(Guid id);
    Task<User> SaveUserByOAuthAsync(User user);
    Task<User> CheckIsEmailRegistered(string email);
    Task RefreshTokenInDatabase(Guid userID, string refreshToken);
}
