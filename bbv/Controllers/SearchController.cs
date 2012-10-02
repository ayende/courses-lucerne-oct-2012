﻿using bbv.Infrastructure.Indexes;
using bbv.Models;
using Raven.Client;
using System.Linq;

namespace bbv.Controllers
{
    using Raven.Client.Linq;

    public class SearchController : RavenController
	{
		 public object Magic(string q)
		 {
			 var query = Session.Query<Students_Search.SearchResult, Students_Search>()
				 .Search(x => x.Query, q)
				 .As<Student>();

			 var results = query
				 .ToList();

			 if(results.Count == 0)
			 {
				 var sugguestions = query.Suggest();

				 switch (sugguestions.Suggestions.Length)
				 {
					case 0:
						 return Json("No idea what to do now...");
					case 1:
						 return Magic(sugguestions.Suggestions[0]);
					case 2: case 3: case 4:
						 return Json(new
							 {
								 sugguestions.Suggestions,
								 Results = sugguestions.Suggestions.Aggregate(
									 Session.Query<Students_Search.SearchResult, Students_Search>(),
									 (current, suggestion) => current.Search(x => x.Query, suggestion))
							             .As<Student>()
							             .ToList()
							 });
					 default:
						 return Json(new
							 {
								 DidYouMean = sugguestions.Suggestions
							 });
				 }
			 }

			 return Json(results);
		 }

        public object SearchRevenues(decimal value)
        {
            var query = Queryable.Where(this.Session.Query<Orders_Search.RevenueResult, Orders_Search>(), x => x.Revenue > value)
                .As<Order>();

            var results = query
                .ToList();

            return Json(results); 
        }
        
        public object CreateOrders()
        {
            for (int i = 0; i < 100; i++)
            {
                var order = new Order {
                                          Cost = 1,
                                          Participants = i
                                      };
                Session.Store(order);
            }
            return Json("Done");
        }

        public object StudentsByEmailDomain(string emailDomain)
        {
            var results = Session.Query<Students_ByEmailDomain.Result, Students_ByEmailDomain>()
                        .Where(x => x.EmailDomain == emailDomain);

            return Json(results);
        }
	}
}