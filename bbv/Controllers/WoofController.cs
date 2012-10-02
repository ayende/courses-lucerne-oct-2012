using bbv.Models;

namespace bbv.Controllers
{
	public class WoofController : RavenController
	{
		public object Populate()
		{
			Session.Store(new Animal
				{
					Name = "Arava",
					Species = "Dog",
					Breed = "German Shepherd"
				});
			Session.Store(new Animal
				{
					Name = "Oscar",
					Species = "Dog",
					Breed = "Little Mixed"
				});

			Session.Store(new Animal
				{
					Name = "Meow",
					Species = "Cat",
					Breed = "Street"
				});

			return Json("Okay");
		}
	}
}