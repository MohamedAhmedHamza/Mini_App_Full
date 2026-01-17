using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniApp.Core.DTOs;
using MiniApp.Service.IServices;

namespace MiniApp.API.Controllers
{
	[ApiController]
	[Route("api/users")]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;

		public UsersController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _userService.GetAllAsync());
		}

		[HttpPost]
		public async Task<IActionResult> Create(UserRegisterDto dto)
		{
			await _userService.CreateAsync(dto);
			return Ok();
		}
	}

}
	