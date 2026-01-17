using MiniApp.Core.Entities;
using MiniApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp.Core.Interfaces
{
	public interface IUnitOfWork
	{
		IGenericRepository<User> Users { get; }
		IGenericRepository<Ticket> Tickets { get; }
		Task<int> SaveAsync();
	}
}
