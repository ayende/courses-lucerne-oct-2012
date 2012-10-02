using Raven.Client;
using bbv.Models;
using Raven.Client.Linq;
using System.Linq;

namespace bbv.Controllers
{
	public class WoofController : RavenController
	{

		public object Query(string name)
		{
			RavenQueryStatistics stats;
			var r = Session.Query<Animal>("Animals/MultiMap")
				.Statistics(out stats)
				.Where(x => x.Name == name)
				.ToList();

			return Json(new
				{
					Results = r,
					stats.IndexName
				});
		}

		public object Populate()
		{
			Session.Store(new Dog
				{
					Name = "Arava",
					Species = "Dog",
					Breed = "German Shepherd"
				});
			Session.Store(new Dog
				{
					Name = "Oscar",
					Species = "Dog",
					Breed = "Little Mixed"
				});

			Session.Store(new Cat
				{
					Name = "Meow",
					Species = "Cat",
					Breed = "Street"
				});

			return Json("Okay");
		}
	}
}