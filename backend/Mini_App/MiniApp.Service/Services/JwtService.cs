using MiniApp.Core.Entities;
using MiniApp.Core.Enum;
using MiniApp.Service.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniApp.Service.Services
{
	public class JwtService : IJwtService
	{
		private readonly IConfiguration _config;

		public JwtService(IConfiguration config)
		{
			_config = config;
		}

		public string GenerateToken(User user)
		{
			if (user == null)
				throw new ArgumentNullException(nameof(user));

			var jwtKey = _config["Jwt:Key"];
			if (string.IsNullOrEmpty(jwtKey))
				throw new Exception("JWT Key is missing");

			var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.Username ?? ""),
				new Claim(ClaimTypes.Role, (user.Role).ToString())
			};

			var expireMinutes = int.Parse(_config["Jwt:ExpireMinutes"] ?? "60");

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
				Issuer = _config["Jwt:Issuer"],
				Audience = _config["Jwt:Audience"],
				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(keyBytes),
					SecurityAlgorithms.HmacSha256
				)
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
