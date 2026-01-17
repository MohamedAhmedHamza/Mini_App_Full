using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp.Core.Specifications
{

	public interface ISpecification<T>
	{
		Expression<Func<T, bool>> Criteria { get; set; }
		List<Expression<Func<T, object>>> Includes { get; set; }
		int Take { get; set; }
		int Skip { get; set; }
		bool IsPagingEnabled { get; set; }
	}
}
