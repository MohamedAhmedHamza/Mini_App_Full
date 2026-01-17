using Microsoft.EntityFrameworkCore;
using MiniApp.Core.Entities;
using MiniApp.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp.Infrastructure.Data.Context
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(
				typeof(AppDbContext).Assembly
			);

			base.OnModelCreating(modelBuilder);
		}
		public DbSet<User> Users { get; set; }
		public DbSet<Ticket> Tickets { get; set; }


	}
}
