﻿using HexAsset.Data;
using HexAsset.Models.Dto;
using HexAsset.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HexAsset.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly AppDbContext dbContext;
		private readonly IConfiguration _config;

		public UserController(AppDbContext dbContext, IConfiguration config)
		{
			this.dbContext = dbContext;
			this._config = config;
		}


		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			await dbContext.SaveChangesAsync();
			return Ok(dbContext.AssetAllocations.ToList());
		}


		[HttpPost("Register")]
		public async Task<IActionResult> Register([FromBody] UserDto userdto)
		{
			if (await dbContext.Users.AnyAsync(u => u.Role == userdto.Role))
			{
				return Conflict("Username already exists.");
			}

			var user = new User
			{
				Role = userdto.Role,
				Name = userdto.Name,
				Email = userdto.Email,
				Password = BCrypt.Net.BCrypt.HashPassword(userdto.Password),
				ContactNumber = userdto.ContactNumber,
				Address = userdto.Address,
				DateCreated = userdto.DateCreated,
			};

			dbContext.Users.Add(user);
			await dbContext.SaveChangesAsync();
			return Ok("User registered successfully.");
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto userlogin)
		{
			var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == userlogin.Email);
			if (user == null || !BCrypt.Net.BCrypt.Verify(userlogin.Password, user.Password))
			{
				return Unauthorized("Invalid username or password.");
			}

			var token = GenerateJwtToken(user);
			return Ok(new { Token = token });
		}
		private string GenerateJwtToken(User user)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
			new Claim(JwtRegisteredClaimNames.Sub, user.Email),
			new Claim("role", user.Role),
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


		[HttpGet("{id}")]
		public async Task<IActionResult> GetUserById(int id)
		{
			var user = dbContext.Users.Find(id);
			if (user == null)
			{
				return NotFound();
			}
			await dbContext.SaveChangesAsync();
			return Ok(user);
		}


		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUserById(int id, [FromBody] UserDto userDto)
		{
			var user = dbContext.Users.Find(id);
			if (user == null)
				return NotFound();

			user.Role = userDto.Role;
			user.Name = userDto.Name;
			user.Email = userDto.Email;
			user.ContactNumber = userDto.ContactNumber;
			user.Address = userDto.Address;

			if (!string.IsNullOrWhiteSpace(userDto.Password))
				user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

			dbContext.Users.Update(user);
			await dbContext.SaveChangesAsync();

			return Ok(new { message = "User updated successfully!" });
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUserById(int id)
		{
			var user = dbContext.Users.Find(id);
			if (user == null)
				return NotFound();

			dbContext.Users.Remove(user);
			await dbContext.SaveChangesAsync();

			return Ok(new { message = "User deleted successfully!" });
		}
	}
}