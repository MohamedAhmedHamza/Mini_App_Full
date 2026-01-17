using Microsoft.EntityFrameworkCore;
using MiniApp.Core.Entities;
using MiniApp.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp.Core.Interfaces
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T?> GetByIdAsync(int id);
		Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
		Task AddAsync(T entity);
		void Update(T entity);
		void Delete(T entity);

	}
}
