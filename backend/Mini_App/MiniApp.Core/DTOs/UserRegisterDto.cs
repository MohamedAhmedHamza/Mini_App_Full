using MiniApp.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp.Core.DTOs
{
	public class UserRegisterDto
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public UserRole Role { get; set; } = UserRole.User;
	}


}
