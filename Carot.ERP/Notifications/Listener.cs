﻿using System;
using System.Reflection;

namespace Carot.ERP.Notifications
{
    internal class Listener
    {
		public string Channel { get; set; }
		public Object Instance { get; set; }
		public MethodInfo Method { get; set; }
	}
}
