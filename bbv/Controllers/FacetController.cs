using System.Diagnostics;
using Raven.Abstractions.Data;
using bbv.Models;
using Raven.Client.Linq;
using System.Linq;
using Raven.Client;

namespace bbv.Controllers
{
	public class FacetController : RavenController
	{
		public object Pop()
		{
			for (int i = 0; i < 25000; i++)
			{
				Session.Store(new Animal
					{
						Name = "Dog" + i,
						Species = "Dog",
						Breed = "German Shepherd",
						Age = 2
					});

				Session.Store(new Animal
				{
					Name = "Cat" + i,
					Species = "Cat",
					Breed = "Street",
					Age = 2
				});
			}
			return "ok";
		}

		 public object Create()
		 {
			 Session.Store(new FacetSetup
				 {
					 Id = "Facets/Animals",
					 Facets =
						 {
							 new Facet
								 {
									 Name = "Breed"
								 },
							new Facet
								{
									Name = "Species"
								}
						 }
				 });
			 return Json("Okay");
		 }

		 public object Query(int age, string[] options)
		 {
			 var query = Session.Query<Animal>("Animals/Search")
				.Where(x=>x.Age == age);

			 foreach (var option in options ?? Enumerable.Empty<string>())
			 {
				 query.Customize(customization =>
					 {
						 var documentQuery = ((IDocumentQuery<Animal>) customization);
						 documentQuery.AndAlso().WhereEquals("F", option, isAnalyzed: false);
					 });
			 }

			 var sp = Stopwatch.StartNew();
			 var facetResults = query.ToFacets("Facets/Animals").Results.Select(x=>new
				 {
					 Name = x.Key,
					 Results = x.Value.Values.Select(y=>new{ Name = y.Range, y.Hits})
				 });
			 return Json(new
				 {
					Time = sp.ElapsedMilliseconds,
					 Facets = facetResults,
					 Results = query.ToList(),
				 });
		 }
	}
}