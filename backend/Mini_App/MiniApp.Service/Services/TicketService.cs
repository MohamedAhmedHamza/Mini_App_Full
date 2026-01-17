using AutoMapper;
using MiniApp.Core.DTOs;
using MiniApp.Core.Entities;
using MiniApp.Core.Enum;
using MiniApp.Core.Interfaces;
using MiniApp.Core.Specifications.TicketSpecifications;
using MiniApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApp.Service.Services
{
	public class TicketService : ITicketService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}


		// User sees only his tickets
		// Admin & Assistant see all
		public async Task<IReadOnlyList<TicketResponseDto>> GetTicketsAsync(
				 int userId,
				 UserRole role,
				 TicketStatus? status,
				 int page,
				 int pageSize)
		{
			var spec = new TicketsWithFiltersSpecification(
				userId, role, status, page, pageSize);

			var tickets = await _unitOfWork.Tickets.ListAsync(spec);

			// Mapper already maps Ticket -> TicketResponseDto, including Username
			return _mapper.Map<IReadOnlyList<TicketResponseDto>>(tickets);

		}
	






		
		public async Task<TicketDto> CreateAsync(TicketDto dto, int userId)
		{
			var ticket = _mapper.Map<Ticket>(dto);
			ticket.UserId = userId;

			try
			{
				await _unitOfWork.Tickets.AddAsync(ticket);
				await _unitOfWork.SaveAsync();
				return _mapper.Map<TicketDto>(ticket);
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Error saving ticket to database", ex);
			}
		}

		
		public async Task<Ticket> ApproveTicketAsync(int id)
		{
			var ticket = await _unitOfWork.Tickets.GetByIdAsync(id)
				?? throw new Exception("Ticket not found");

			if (ticket.Status != TicketStatus.Pending)
				throw new Exception("Invalid status transition");

			ticket.Status = TicketStatus.Approved;
			await _unitOfWork.SaveAsync();
			return ticket;
		}

		
		public async Task<Ticket> RejectTicketAsync(int ticketId)
		{
			var ticket = await _unitOfWork.Tickets.GetByIdAsync(ticketId);
			if (ticket == null)
				throw new Exception("Ticket not found");

			if (ticket.Status != TicketStatus.Pending)
				throw new Exception("Only pending tickets can be rejected");
				
			ticket.Status = TicketStatus.Rejected;	
			await _unitOfWork.SaveAsync();

			return ticket;
		}
	}
}

