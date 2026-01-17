using AutoMapper;
using MiniApp.Core.DTOs;
using MiniApp.Core.Entities;

namespace MiniApp.Core.Mapping
{
	public class TicketProfile : Profile
	{
		public TicketProfile()
		{
			CreateMap<Ticket, TicketResponseDto>()
		   .ForMember(
			   dest => dest.UserName,
			   opt => opt.MapFrom(src => src.User.Username)
		   )
		   .ForMember(
			   dest => dest.Status,
			   opt => opt.MapFrom(src => src.Status.ToString())
		   );
		}
	}
}

