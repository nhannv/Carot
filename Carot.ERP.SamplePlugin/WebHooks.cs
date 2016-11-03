using System;
using System.Diagnostics;
using Carot.ERP.Api.Models;
using Carot.ERP.WebHooks;

namespace Carot.ERP.SamplePlugin
{
    public class WebHooks
    {
		[WebHook("patch_record_success_action", "wv_project")]
		public void PatchRecordAction( dynamic data )
		{
			Debug.WriteLine("PatchRecordActionHook");
		}

		[WebHook("patch_record_input_filter", "wv_project")]
		public dynamic PatchRecordInput(dynamic data)
		{
			//EntityRecord record = (EntityRecord)data.record;
			//record["billable_hour_price"] = int.Parse(record["billable_hour_price"].ToString()) + 1;
			//Debug.WriteLine("PatchRecordInput");
			//Debug.WriteLine(record["billable_hour_price"]);
			return data;
		}

		[WebHook("patch_record_input_filter", "wv_project", 11)]
		public dynamic PatchRecordInput2(dynamic data)
		{
			//EntityRecord record = (EntityRecord)data.record;
			//record["billable_hour_price"] = int.Parse(record["billable_hour_price"].ToString()) - 1;
			//Debug.WriteLine(record["billable_hour_price"]);
			//throw new Exception("test");
			Debug.WriteLine("PatchRecordInput2");
			return data;
		}

		[WebHook("patch_record_validation_errors_filter", "wv_project")]
		public dynamic PatchRecordValidationErrors(dynamic data)
		{
			Debug.WriteLine("PatchRecordValidationErrors");
			return data;
		}

		[WebHook("patch_record_pre_save_filter", "wv_project")]
		public dynamic PatchRecordPreSave(dynamic data)
		{
			Debug.WriteLine("PatchRecordPreSave");
			return data;
		}
	}
}
