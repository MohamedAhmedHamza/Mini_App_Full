using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniApp.Service.IServices;

namespace MiniApp.API.Controllers
{
	[Authorize(Roles = "Admin")]
	[ApiController]
	[Route("api/admin")]
	public class AdminController : ControllerBase
	{
		private readonly IAdminService _adminService;

		public AdminController(IAdminService adminService)
		{
			_adminService = adminService;
		}

		[HttpGet("tickets")]
		public async Task<IActionResult> GetAll()
			=> Ok(await _adminService.GetAllTicketsAsync());

		[HttpPost("/api/admin/tickets/{id}/approve")]
		public async Task<IActionResult> Approve(int id)
			=> Ok(await _adminService.ApproveTicketAsync(id));

		[HttpPost("/api/admin/tickets/{id}/reject")]
		public async Task<IActionResult> Reject(int id)
			=> Ok(await _adminService.RejectTicketAsync(id));
	}

}
