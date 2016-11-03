﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Carot.ERP.Database;
using Carot.ERP.Api;
using Carot.ERP.Web.Security;
using System;

namespace Carot.ERP.Web.Services
{
	public static class AppBuilderExtensions
	{
		public static void UseErpMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ErpMiddleware>();
		}
		public static void UseDebugLogMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<DebugLogMiddleware>();
		}
	}

	public class ErpMiddleware
	{
		RequestDelegate next;


		public ErpMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task Invoke(HttpContext context, IRequestService requestService)
		{
			var dbCtx = DbContext.CreateContext(Settings.ConnectionString);
			requestService.AddObjectToDispose(dbCtx);

			WebSecurityUtil.Authenticate(context);
			var identity = (context.User as ErpPrincipal)?.Identity as ErpIdentity;
			if (identity != null)
			{
				var securityContext = SecurityContext.OpenScope(identity.User);
				requestService.AddObjectToDispose(securityContext);
			}

			await next(context);
		}
	}


	public class DebugLogMiddleware
	{
		IErpService service;
		RequestDelegate next;

		public DebugLogMiddleware(RequestDelegate next, IErpService service)
		{
			this.next = next;
			this.service = service;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				//var httpFeature = context.GetFeature<IHttpConnectionFeature>();
				await next(context);
			}
			catch (Exception ex)
			{
				var exception = ex;
				throw ex;
			}
		}
	}
}