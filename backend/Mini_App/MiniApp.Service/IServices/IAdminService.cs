using MiniApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp.Service.IServices
{
	public interface IAdminService
	{
		Task<IEnumerable<Ticket>> GetAllTicketsAsync();
		Task<Ticket> ApproveTicketAsync(int ticketId);
		Task<Ticket> RejectTicketAsync(int ticketId);
	}
}
