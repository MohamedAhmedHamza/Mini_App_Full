using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp.Core.DTOs
{
	using MiniApp.Core.Enum;

	public class TicketResponseDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Status { get; set; }
		public int UserId { get; set; }
		public string UserName { get; set; }
	}
}
