﻿using Microsoft.AspNetCore.Mvc;

namespace Carot.ERP.SamplePlugin
{
	public class SampleController : Controller
	{
		[AcceptVerbs(new[] { "GET" }, Route = "/plugins/webvella-sample-plugin/api/")]
		public IActionResult Index()
		{
			return Json(new { Message = "This is a sample json response from webvella erp sample plugin controller." });
		}
	}
}
