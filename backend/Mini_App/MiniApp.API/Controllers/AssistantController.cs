using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniApp.Service.IServices;

namespace MiniApp.API.Controllers
{
	[Authorize(Roles = "Assistant")]
	[ApiController]
	[Route("api/assistant")]
	public class AssistantController : ControllerBase
	{
		private readonly IAdminService _adminService;

		public AssistantController(IAdminService adminService)
		{
			_adminService = adminService;
		}

		[HttpGet("tickets")]
		public async Task<IActionResult> GetAll()
			=> Ok(await _adminService.GetAllTicketsAsync());
	}

}
