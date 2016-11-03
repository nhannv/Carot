using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using Carot.ERP.Plugins;

namespace Carot.ERP.Project
{
	public static class StaticContext
	{
		public static Plugin Plugin { get; private set; }

		public static IServiceProvider ServiceProvider { get; private set; }

		public static IConfiguration Configuration { get; private set; }

		internal static void Initialize(Plugin plugin,IServiceProvider serviceProvider)
		{
			Plugin = plugin;

			IHostingEnvironment hostingEnvironment = (IHostingEnvironment)serviceProvider.GetService(typeof(IHostingEnvironment));

			var configurationBuilder = new ConfigurationBuilder()
			   .SetBasePath(hostingEnvironment.ContentRootPath)
				.AddJsonFile("webvella-projects.config.json");
			Configuration = configurationBuilder.Build();
		}
	}
}
