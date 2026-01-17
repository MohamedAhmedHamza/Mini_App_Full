using MiniApp.Core.Entities;
using MiniApp.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp.Core.Specifications.TicketSpecifications
{
	public class TicketsWithFiltersSpecification : BaseSpecification<Ticket>
	{
		public TicketsWithFiltersSpecification(
				int userId,
				UserRole role,
				TicketStatus? status,
				int page,
				int pageSize)
				: base(t =>
					(role == UserRole.Admin || role == UserRole.Assistant || t.UserId == userId) &&
					(!status.HasValue || t.Status == status))
		{
			AddInclude(t => t.User);

			ApplyPaging((page - 1) * pageSize, pageSize);
		}
	}


}
