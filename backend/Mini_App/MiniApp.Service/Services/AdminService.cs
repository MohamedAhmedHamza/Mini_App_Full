using MiniApp.Core.Entities;
using MiniApp.Core.Enum;
using MiniApp.Core.Interfaces;
using MiniApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp.Service.Services
{
	public class AdminService : IAdminService
	{
		private readonly IUnitOfWork _unitOfWork;

		public AdminService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
			=> await _unitOfWork.Tickets.GetAllAsync();

		public async Task<Ticket> ApproveTicketAsync(int id)
		{
			var ticket = await _unitOfWork.Tickets.GetByIdAsync(id)
				?? throw new Exception("Ticket not found");

			ticket.Status = TicketStatus.Approved;
			_unitOfWork.Tickets.Update(ticket);
			await _unitOfWork.SaveAsync();
			return ticket;
		}

		public async Task<Ticket> RejectTicketAsync(int id)
		{
			var ticket = await _unitOfWork.Tickets.GetByIdAsync(id)
				?? throw new Exception("Ticket not found");

			ticket.Status = TicketStatus.Rejected;
			_unitOfWork.Tickets.Update(ticket);
			await _unitOfWork.SaveAsync();
			return ticket;
		}
	}

}
