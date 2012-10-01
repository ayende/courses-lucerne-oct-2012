namespace bbv.Infrastructure.Indexes
{
    using System.Linq;

    using Raven.Client.Indexes;

    using bbv.Models;

    public class Orders_Search : AbstractIndexCreationTask<Order, Orders_Search.RevenueResult>
    {
        public class RevenueResult
        {
            public decimal Revenue { get; set; }
        }

        public Orders_Search()
        {
            this.Map = orders => from o in orders
                            select new {
                                           Revenue = (o.Cost * o.Participants)
                                       };
        }
    }
}