using HexAsset.Models;
using HexAsset.Repositories;

namespace HexAsset.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task<bool> IsUserExistsAsync(string name)
    {
        return await _userRepository.IsUserExistsAsync(name);
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetUserByEmailAsync(email);
    }

    public async Task AddUserAsync(User user)
    {
        // Additional business logic (e.g., validation) can be added here
        if (await _userRepository.IsUserExistsAsync(user.Name))
        {
            throw new InvalidOperationException("User already exists.");
        }

        await _userRepository.AddUserAsync(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(user.UserId);
        if (existingUser == null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        // Update other properties as needed

        await _userRepository.UpdateUserAsync(existingUser);
        await _userRepository.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        await _userRepository.DeleteUserAsync(id);
        await _userRepository.SaveChangesAsync();
    }
}
