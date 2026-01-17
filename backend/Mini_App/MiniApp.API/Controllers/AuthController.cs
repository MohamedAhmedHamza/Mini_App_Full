using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniApp.Core.DTOs;
using MiniApp.Service.IServices;

namespace MiniApp.API.Controllers
{
	[ApiController]
	[Route("api/auth")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(UserLoginDto dto)
		{
			try
			{
				var token = await _authService.LoginAsync(dto);
				return Ok(new { token });
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}



}
