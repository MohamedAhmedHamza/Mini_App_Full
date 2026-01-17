using AutoMapper;
using Microsoft.Extensions.Configuration;
using MiniApp.Core.DTOs;
using MiniApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MiniApp.Core.Mapping
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{

			CreateMap<UserRegisterDto, User>()
				.ForMember(U => U.PasswordHash,
						   opt => opt.Ignore())
				.ForMember(U => U.Id,
						   opt => opt.Ignore())
				.ForMember(U => U.CreatedAt,
						   opt => opt.Ignore())
				.ForMember(U => U.UpdatedAt,
						   opt => opt.Ignore())
				.ForMember(U => U.IsDeleted,
						   opt => opt.Ignore());


			CreateMap<User, UserRegisterDto>()
				.ForMember(U => U.Password,
						   opt => opt.Ignore());
		}
	}
}
