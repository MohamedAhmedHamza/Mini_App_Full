using MiniApp.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp.Core.Entities
{
	public class Ticket : BaseEntity
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public TicketStatus Status { get; set; } = TicketStatus.Pending;
		public int UserId { get; set; }
		public User User { get; set; }
	}
}
