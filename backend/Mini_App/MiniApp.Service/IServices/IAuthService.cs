using MiniApp.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp.Service.IServices
{
	public interface IAuthService
	{
		Task RegisterAsync(UserRegisterDto dto);
		Task<string> LoginAsync(UserLoginDto dto);
	}
}
