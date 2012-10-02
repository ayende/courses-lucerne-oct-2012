using System.Linq;
using Raven.Client.Indexes;
using bbv.Models;

namespace bbv.Infrastructure.Indexes
{
	public class Animals_MultiMap : AbstractMultiMapIndexCreationTask
	{
		public Animals_MultiMap()
		{
			AddMap<Cat>(cats =>
						from cat in cats
						select new { cat.Name, cat.Scratches, Barks = false }
			);

			AddMap<Dog>(dogs =>
						from dog in dogs
						select new { dog.Name, dog.Barks, Scratches = false }
			);


			//AddMapForAll<Animal>(animals =>
			//					 from animal in animals
			//					 select new
			//						 {
			//							 animal.Name,

			//						 }
			//	);
		}
	}
}