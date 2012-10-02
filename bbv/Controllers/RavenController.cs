using System;
using System.Web.Mvc;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using bbv.Infrastructure;
using bbv.Infrastructure.Indexes;
using bbv.Models;

namespace bbv.Controllers
{
	public class RavenController : Controller
	{
		protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
		{
			return base.Json(data, contentType, contentEncoding, JsonRequestBehavior.AllowGet);
		}

		private static readonly Lazy<IDocumentStore> documentStore =
			new Lazy<IDocumentStore>(() =>
				{
					var store = new DocumentStore
						{
							Url = "http://localhost:8080",
							DefaultDatabase = "bbv",
							Conventions =
								{
									//FindTypeTagName = type =>
									//	{
									//		if (type.IsSubclassOf(typeof(Animal)))
									//			return "Animals";
									//		return DocumentConvention.DefaultTypeTagName(type);
									//	}
								}
						}
						.RegisterListener(new AuditListener())
						.Initialize();

					IndexCreation.CreateIndexes(typeof(Students_Search).Assembly, store);
					return store;
				});

		public static IDocumentStore DocumentStore
		{
			get
			{
				return documentStore.Value;
			}
		}

		public new IDocumentSession Session { get; set; }

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Session = DocumentStore.OpenSession();
		}

		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			using (Session)
			{
				if (Session == null)
					return;
				if (filterContext.Exception != null)
					return;
				Session.SaveChanges();
			}
		}
	}
}