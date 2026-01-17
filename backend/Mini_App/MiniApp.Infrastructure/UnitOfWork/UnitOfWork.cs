using MiniApp.Core.Entities;
using MiniApp.Core.Interfaces;
using MiniApp.Infrastructure.Data.Context;
using MiniApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp.Infrastructure.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _context;

		public IGenericRepository<User> Users { get; }
		public IGenericRepository<Ticket> Tickets { get; }

		public UnitOfWork(AppDbContext context)
		{
			_context = context;
			Users = new GenericRepository<User>(_context);
			Tickets = new GenericRepository<Ticket>(_context);
		}
		public async Task<int> SaveAsync()
		{
			return await _context.SaveChangesAsync();
		}
	}

}
