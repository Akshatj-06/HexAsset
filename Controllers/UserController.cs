using HexAsset.Data;
using HexAsset.Models.Dto;
using HexAsset.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HexAsset.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace HexAsset.Controllers
{
	[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;

    public UserController(IUserRepository userRepository, IConfiguration config)
    {
        _userRepository = userRepository;
        _config = config;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("GetUser")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserDto userDto)
    {
        try
        {
            if (await _userRepository.IsUserExistsAsync(userDto.Name))
            {
                return Conflict("Username already exists.");
            }

            var user = new User
            {
                Role = userDto.Role,
                Name = userDto.Name,
                Email = userDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                ContactNumber = userDto.ContactNumber,
                Address = userDto.Address,
                DateCreated = userDto.DateCreated,
            };

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();

            return Ok("User registered successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto userLogin)
    {
        try
        {
            if (userLogin == null || string.IsNullOrEmpty(userLogin.Email) || string.IsNullOrEmpty(userLogin.Password))
            {
                return BadRequest("Email or Password cannot be empty.");
            }

            var user = await _userRepository.GetUserByEmailAsync(userLogin.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password))
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
        }
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim("role", user.Role),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.NewPassword))
            {
                return BadRequest("Email or new password is missing.");
            }

            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return NotFound("User with the specified email does not exist.");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            await _userRepository.UpdateUserAsync(user);
            await _userRepository.SaveChangesAsync();

            return Ok(new { message = "Password updated successfully!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("GetUserById/{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("UpdateUser/{id}")]
    public async Task<IActionResult> UpdateUserById(int id, [FromBody] UserDto userDto)
    {
        try
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            user.Role = userDto.Role;
            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.ContactNumber = userDto.ContactNumber;
            user.Address = userDto.Address;

            if (!string.IsNullOrWhiteSpace(userDto.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            }

            await _userRepository.UpdateUserAsync(user);
            await _userRepository.SaveChangesAsync();

            return Ok(new { message = "User updated successfully!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("DeleteUser/{id}")]
    public async Task<IActionResult> DeleteUserById(int id)
    {
        try
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            await _userRepository.DeleteUserAsync(id);
            await _userRepository.SaveChangesAsync();

            return Ok(new { message = "User deleted successfully!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}

}
