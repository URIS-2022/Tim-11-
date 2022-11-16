using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace WebVella.Erp.Web.Middleware
{
	public class ErpDebugLogMiddleware
	{
		
		RequestDelegate next;

		public ErpDebugLogMiddleware(RequestDelegate next)
		{
			this.next = next;
			
		}


		public async Task Invoke(HttpContext context)
		{
			try
			{
				//var httpFeature = context.GetFeature<IHttpConnectionFeature>();
				await next(context);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
