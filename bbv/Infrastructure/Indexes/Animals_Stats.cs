using System.Collections.Generic;
using System.Linq;
using Raven.Client.Indexes;
using bbv.Models;

namespace bbv.Infrastructure.Indexes
{
	public class Animals_Stats : AbstractIndexCreationTask<Animal, Animals_Stats.ReduceResult>
	{
		public class ReduceResult
		{
			public string Species { get; set; }
			public int Count { get; set; }
			public BreedStats[] Breeds { get; set; }

			public class BreedStats
			{
				public string Breed { get; set; }
				public int Count { get; set; }
			}
		}

		public Animals_Stats()
		{
			Map = animals =>
			      from animal in animals
			      select new
				      {
					      animal.Species,
					      Count = 1,
					      Breeds = new [] {new {animal.Breed, Count = 1}}
				      };
			Reduce = animals =>
			         from r in animals
			         group r by r.Species
			         into g
			         select new
				         {
					         Species = g.Key,
					         Count = g.Sum(x => x.Count),
					         Breeds = from breed in g.SelectMany(x => x.Breeds)
					                  group breed by breed.Breed
					                  into gb
					                  select new {Breed = gb.Key, Count = gb.Sum(x => x.Count)}
				         };

		}
	}
}