using MiniApp.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp.Service.IServices
{
	public interface IUserService
	{
		Task<IEnumerable<UserRegisterDto>> GetAllAsync();
		Task<UserRegisterDto?> GetByIdAsync(int id);
		Task<UserRegisterDto> CreateAsync(UserRegisterDto userDto);
		Task<bool> UpdateAsync(int id, UserRegisterDto userDto);
		Task<bool> DeleteAsync(int id);
		Task<bool> VerifyPasswordAsync(string username, string password);
	}
}
