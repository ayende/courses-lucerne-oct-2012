namespace bbv.Infrastructure.Indexes
{
	using System.Linq;

	using Raven.Client.Indexes;

	using Models;

	public class Orders_Search : AbstractIndexCreationTask<Order, Orders_Search.RevenueResult>
	{
		public class RevenueResult
		{
			public decimal Total { get; set; }
		}

		public Orders_Search()
		{
			Map = orders => from o in orders
							select new RevenueResult
								 {
									 Total = o.Lines.Sum(x=>x.Qty * x.Cost)
								 };
		}
	}
}