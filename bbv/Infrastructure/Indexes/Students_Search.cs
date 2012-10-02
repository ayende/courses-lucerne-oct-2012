using System.Linq;
using Raven.Client.Indexes;
using bbv.Models;

namespace bbv.Infrastructure.Indexes
{
	public class Students_Search : AbstractIndexCreationTask<Student, Students_Search.SearchResult>
	{
		public class SearchResult
		{
			public string Query { get; set; }
			public bool Geek { get; set; }
		}

		public Students_Search()
		{
			Map = students => from s in students
			                  select new
				                  {
					                  Query = new object[]
						                  {
							                  s.FirstName,
							                  s.LastName,
							                  s.Email,
							                  s.Email.Split('@'),
						                  },
									  s.Geek
				                  };
		}
	}
}