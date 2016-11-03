using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Diagnostics;
using Carot.ERP.Plugins;

namespace Carot.ERP.SamplePlugin
{
	[PluginStartup]
	public class Startup
	{
		public void Start(PluginStartArguments pluginStartArgs)
		{
			IHostingEnvironment hostingEnvironment = (IHostingEnvironment)pluginStartArgs.ServiceProvider.GetService(typeof(IHostingEnvironment));
			// Sample how to read configuration from erp config file
			// you can use your own config 
			var configurationBuilder = new ConfigurationBuilder()
				//.SetBasePath(PlatformServices.Default.Application.ApplicationBasePath)
				.SetBasePath(hostingEnvironment.ContentRootPath)
				.AddJsonFile("config.json");
			var configuration = configurationBuilder.Build();


			Debug.WriteLine("Carot Sample Plugin start called");
		}
	}
}
