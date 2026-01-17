using MiniApp.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp.Core.Entities
{
	public class User : BaseEntity
	{
		public string Username { get; set; }
		public string PasswordHash { get; set; }
		public UserRole Role { get; set; }
		public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
	}
}
	