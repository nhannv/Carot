using System;
using System.Collections.Generic;

namespace Carot.ERP.Plugins
{
    public interface IPluginService
	{
		List<Plugin> Plugins { get; }
		void Initialize(IServiceProvider serviceProvider);
	}
}