using HexAsset.Data;
using HexAsset.Models;
using Microsoft.EntityFrameworkCore;

namespace HexAsset.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<bool> IsUserExistsAsync(string name)
    {
        return await _dbContext.Users.AnyAsync(u => u.Name == name);
    }

    public async Task AddUserAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task UpdateUserAsync(User user)
    {
        _dbContext.Users.Update(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user != null)
        {
            _dbContext.Users.Remove(user);
        }
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}