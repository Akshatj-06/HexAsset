using HexAsset.Models;

namespace HexAsset.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task<bool> IsUserExistsAsync(string name);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
    Task<User> GetUserByEmailAsync(string email);
    Task SaveChangesAsync();
}