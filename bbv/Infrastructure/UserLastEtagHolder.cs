using System;
using System.Collections.Concurrent;
using System.Web;
using Raven.Client.Util;

namespace bbv.Infrastructure
{
	public class UserLastEtagHolder : ILastEtagHolder
	{
		static ConcurrentDictionary<string, Guid?>  memoryLeak = new ConcurrentDictionary<string, Guid?>();
		public void UpdateLastWrittenEtag(Guid? etag)
		{
			memoryLeak.AddOrUpdate(HttpContext.Current.Request.QueryString["userId"], etag, (s, guid) => etag);
		}

		public Guid? GetLastWrittenEtag()
		{
			Guid? guid;
			memoryLeak.TryGetValue(HttpContext.Current.Request.QueryString["userId"], out guid);
			return guid ?? Guid.Empty;
		}
	}
}