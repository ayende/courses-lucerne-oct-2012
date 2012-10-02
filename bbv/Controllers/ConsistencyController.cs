using Raven.Client;
using bbv.Models;
using System.Linq;

namespace bbv.Controllers
{
	public class ConsistencyController : RavenController
	{
		 public object Add()
		 {
			 Session.Store(new Animal
				 {
					 Age = 1
				 });
			 return Json("OK");
		 }

		 public object List()
		 {
			 RavenQueryStatistics stats;
			 var animals = Session.Query<Animal>("Animals/Search")
				.Statistics(out stats)
				.ToList();
			 return Json(new
				 {
					 Consistent = stats.IsStale == false,
					 Results = animals
				 });
		 }
	}
}