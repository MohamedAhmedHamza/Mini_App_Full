using MiniApp.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp.Core.DTOs
{
	public class TicketDto
	{
		public string Title { get; set; }
		public string Description { get; set; }
		[EnumDataType(typeof(TicketStatus))]
		public TicketStatus? Status { get; set; }
		public string Username { get; set; }
	}
}
