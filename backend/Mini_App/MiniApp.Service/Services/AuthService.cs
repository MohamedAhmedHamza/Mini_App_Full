using MiniApp.Core.DTOs;
using MiniApp.Core.Entities;
using MiniApp.Service.IServices;
using MiniApp.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApp.Service.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IJwtService _jwtService;

		public AuthService(IUnitOfWork unitOfWork, IJwtService jwtService)
		{
			_unitOfWork = unitOfWork;
			_jwtService = jwtService;
		}

		public async Task RegisterAsync(UserRegisterDto dto)
		{
			var exists = (await _unitOfWork.Users.GetAllAsync())
				.Any(u => u.Username == dto.Username);

			if (exists)
				throw new Exception("Username already exists");

			var user = new User
			{
				Username = dto.Username,
				PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
				Role = dto.Role
			};

			await _unitOfWork.Users.AddAsync(user);
			await _unitOfWork.SaveAsync();
		}

		public async Task<string> LoginAsync(UserLoginDto dto)
		{
			var user = (await _unitOfWork.Users.GetAllAsync())
				.FirstOrDefault(u => u.Username == dto.Username);

			if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
				throw new Exception("Invalid credentials");

			return _jwtService.GenerateToken(user);
		}
	}
}
