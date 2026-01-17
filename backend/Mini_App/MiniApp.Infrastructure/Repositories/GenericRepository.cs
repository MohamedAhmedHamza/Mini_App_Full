using Microsoft.EntityFrameworkCore;
using MiniApp.Core.Entities;
using MiniApp.Core.Interfaces;
using MiniApp.Core.Specifications;
using MiniApp.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp.Infrastructure.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		protected readonly AppDbContext _context;

		public GenericRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _context.Set<T>()
				.Where(e => !e.IsDeleted)
				.ToListAsync();
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await _context.Set<T>()
				.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
		}
		public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
		{
			IQueryable<T> query = _context.Set<T>();

			if (spec.Criteria != null)
				query = query.Where(spec.Criteria);

			
			query = spec.Includes
				.Aggregate(query, (current, include) => current.Include(include));

			if (spec.IsPagingEnabled)
				query = query.Skip(spec.Skip).Take(spec.Take);

			return await query.ToListAsync();
		}

		public async Task AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
		}

		public void Update(T entity)
		{
			_context.Set<T>().Update(entity);
		}

		public void Delete(T entity)
		{
			entity.IsDeleted = true;
			Update(entity);
		}
	}
}
