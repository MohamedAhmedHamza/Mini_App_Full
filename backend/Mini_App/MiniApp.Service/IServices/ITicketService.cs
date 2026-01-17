using MiniApp.Core.DTOs;
using MiniApp.Core.Entities;
using MiniApp.Core.Enum;
using MiniApp.Core.Specifications.TicketSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp.Service.IServices
{
	public interface ITicketService	
	{
		Task<IReadOnlyList<TicketResponseDto>> GetTicketsAsync(int userId, UserRole role,TicketStatus? status, int page,int pageSize);

		Task<TicketDto> CreateAsync(TicketDto dto, int userId);

		Task<Ticket> ApproveTicketAsync(int id);

		Task<Ticket> RejectTicketAsync(int ticketId);
	}






}
