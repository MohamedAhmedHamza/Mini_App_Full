using AutoMapper;
using MiniApp.Core.DTOs;
using MiniApp.Core.Entities;
using MiniApp.Core.Interfaces;
using MiniApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp.Service.Services
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public UserService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<UserRegisterDto> CreateAsync(UserRegisterDto userDto)
		{
			var user = _mapper.Map<User>(userDto);

			// Password Hashing
			user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

			await _unitOfWork.Users.AddAsync(user);
			await _unitOfWork.SaveAsync();

			return _mapper.Map<UserRegisterDto>(user);
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var user = await _unitOfWork.Users.GetByIdAsync(id);
			if (user == null) return false;

			_unitOfWork.Users.Delete(user);
			await _unitOfWork.SaveAsync();
			return true;
		}

		public async Task<IEnumerable<UserRegisterDto>> GetAllAsync()
		{
			var users = await _unitOfWork.Users.GetAllAsync();
			return _mapper.Map<IEnumerable<UserRegisterDto>>(users);
		}



		public async Task<UserRegisterDto?> GetByIdAsync(int id)
		{
			var user = await _unitOfWork.Users.GetByIdAsync(id);
			return user == null ? null : _mapper.Map<UserRegisterDto>(user);
		}

		public async Task<bool> UpdateAsync(int id, UserRegisterDto userDto)
		{
			var user = await _unitOfWork.Users.GetByIdAsync(id);
			if (user == null) return false;

			user.Username = userDto.Username;

			if (!string.IsNullOrEmpty(userDto.Password))
				user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

			_unitOfWork.Users.Update(user);
			await _unitOfWork.SaveAsync();
			return true;
		}

		public async Task<bool> VerifyPasswordAsync(string username, string password)
		{
			var user = (await _unitOfWork.Users.GetAllAsync())
					   .FirstOrDefault(u => u.Username == username);
			if (user == null) return false;

			return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
		}
	}
}
