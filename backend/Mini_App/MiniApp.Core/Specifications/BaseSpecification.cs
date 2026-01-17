using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp.Core.Specifications
{
	public abstract class BaseSpecification<T> : ISpecification<T>
	{

		protected BaseSpecification() { }

		protected BaseSpecification(Expression<Func<T, bool>> criteria)
		{
			Criteria = criteria;
		}
		public Expression<Func<T, bool>> Criteria { get; set; }
		public List<Expression<Func<T, object>>> Includes { get; set; } = new();
		public int Take { get;  set; }
		public int Skip { get;  set; }
		public bool IsPagingEnabled { get; set; }
		protected void ApplyPaging(int skip, int take)
		{
			Skip = skip;
			Take = take;
			IsPagingEnabled = true;
		}
		protected void AddInclude(Expression<Func<T, object>> include)
		{
			Includes.Add(include);
		}	
	}

}
