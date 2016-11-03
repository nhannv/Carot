﻿using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Carot.ERP.Api.Models.AutoMapper;
using System.Globalization;
using Microsoft.Extensions.PlatformAbstractions;
using Carot.ERP.Web.Security;
using Carot.ERP.Database;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Carot.ERP.Plugins;
using Carot.ERP.WebHooks;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Internal;
using Carot.ERP.Web.Services;
using Carot.ERP.Notifications;

namespace Carot.ERP.Web
{
	public class Startup
	{
		private readonly IHostingEnvironment _hostingEnviroment;

		public IConfiguration Configuration { get; set; }

		public Startup(IHostingEnvironment env)
		{
			_hostingEnviroment = env;
			var configurationBuilder = new ConfigurationBuilder()
			   .SetBasePath(env.ContentRootPath)
			   .AddJsonFile("config.json")
			   .AddEnvironmentVariables();
			Configuration = configurationBuilder.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().AddCrmPlugins(_hostingEnviroment);
			services.AddScoped<IRequestService, RequestService>();
			services.AddSingleton<IErpService, ErpService>();
			services.AddSingleton<IPluginService, PluginService>();
			services.AddSingleton<INotificationService, NotificationService>();
			services.AddSingleton<IWebHookService, WebHookService>();
		}

		public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
		{
			//TODO Create db context
			CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");
			CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("en-US");
			Settings.Initialize(Configuration);

			try
			{
				DbContext.CreateContext(Settings.ConnectionString);

				IErpService service = app.ApplicationServices.GetService<IErpService>();
				AutoMapperConfiguration.Configure();
				service.InitializeSystemEntities();

				app.UseErpMiddleware();

				//IHostingEnvironment env = app.ApplicationServices.GetService<IHostingEnvironment>();
				//if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();
				
				IPluginService pluginService = app.ApplicationServices.GetService<IPluginService>();
				IHostingEnvironment hostingEnvironment = app.ApplicationServices.GetRequiredService<IHostingEnvironment>();
				pluginService.Initialize(serviceProvider);

				IWebHookService webHookService = app.ApplicationServices.GetService<IWebHookService>();
				webHookService.Initialize(pluginService);

				INotificationService notificationService = app.ApplicationServices.GetService<INotificationService>();
				notificationService.Initialize(serviceProvider);
				//sample test notification 
				//notificationService.SendNotification(new Notification { Channel = "*", Message = "ERP configuration loaded and completed." });

			}
			finally
			{
				DbContext.CloseContext();
			}

			//Enable CORS
			//app.Use((context, next) =>
			//{
			//	context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
			//	context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "*" });
			//	context.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "*" });
			//	return next();
			//});

			//app.Run(async context =>
			//{
			//    IErpService service = app.ApplicationServices.GetService<IErpService>();
			//    service.Run();
			//    context.Response.ContentType = "text/html";
			//    context.Response.StatusCode = 200;
			//    System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
			//    byte[] buffer = encoding.GetBytes("<h1>test</h1>");
			//    await context.Response.Body.WriteAsync(buffer, 0, buffer.Length);
			//});

			// Add the following to the request pipeline only in development environment.
			if (string.Equals(_hostingEnviroment.EnvironmentName, "Development", StringComparison.OrdinalIgnoreCase))
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// Add Error handling middleware which catches all application specific errors and
				// send the request to the following path or controller action.
				app.UseExceptionHandler("/Home/Error");
			}

            //TODO Check what was done here in RC1
			//app.UseIISPlatformHandler(options => options.AutomaticAuthentication = false);

			// Add static files to the request pipeline.
			app.UseStaticFiles();

			// Add MVC to the request pipeline.
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller}/{action}/{id?}",
					defaults: new { controller = "Home", action = "Index" });

				// Uncomment the following line to add a route for porting Web API 2 controllers.
				// routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
			});
		}

        // Entry point for the application.
        //public static void Main(string[] args) => WebApplication.Run<Startup>(args);
     
    }
}
