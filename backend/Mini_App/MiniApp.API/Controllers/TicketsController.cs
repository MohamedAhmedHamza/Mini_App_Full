using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniApp.Core.DTOs;
using MiniApp.Core.Enum;
using MiniApp.Service.IServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MiniApp.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/tickets")]
	public class TicketsController : ControllerBase
	{
		private readonly ITicketService _ticketService;

		public TicketsController(ITicketService ticketService)
		{
			_ticketService = ticketService;
		}
		[HttpGet]
		public async Task<IActionResult> GetTickets(
					 [FromQuery] TicketStatus? status = null,
					 [FromQuery] int page = 1,
					 [FromQuery] int pageSize = 10)
		{
			var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			var role = Enum.Parse<UserRole>(User.FindFirstValue(ClaimTypes.Role));

			var tickets = await _ticketService.GetTicketsAsync(
				userId, role, status, page, pageSize
			);

			return Ok(tickets); 
		}


		[Authorize(Roles = "User")]
		[HttpPost]
		public async Task<IActionResult> Create(TicketDto dto)
		{
			try
			{
				var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
				var ticket = await _ticketService.CreateAsync(dto, userId);
				return Ok(ticket);
			}
			catch (System.Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
	}
}
