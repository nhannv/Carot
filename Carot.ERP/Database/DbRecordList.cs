﻿using System;
using System.Collections.Generic;
using Carot.ERP.Api.Models;
using Newtonsoft.Json;

namespace Carot.ERP.Database
{
	public class DbRecordList
	{
		public DbRecordList() {
			Id = Guid.NewGuid();
			Name = "";
			Label = "";
			Title = "";
			Default = false;
			System = false;
			Weight = 10;
			CssClass= "";
			IconName = "";
			VisibleColumnsCount = 5;
			PageSize = 10;
			DynamicHtmlTemplate = "";
			DataSourceUrl = "";
			ServiceCode = "";
			ColumnWidthsCSV = "";
		}


		[JsonProperty(PropertyName = "id")]
		public Guid Id { get; set; }

		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "label")]
		public string Label { get; set; }

		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "default")]
		public bool? Default { get; set; }

		[JsonProperty(PropertyName = "system")]
		public bool? System { get; set; }

		[JsonProperty(PropertyName = "weight")]
		public decimal? Weight { get; set; }

		[JsonProperty(PropertyName = "css_class")]
		public string CssClass { get; set; }

		[JsonProperty(PropertyName = "icon_name")]
        public string IconName { get; set; }

		[JsonProperty(PropertyName = "visible_columns_count")]
        public int VisibleColumnsCount { get; set; }

		[JsonProperty(PropertyName = "type")]
		public RecordListType Type { get; set; }

		[JsonProperty(PropertyName = "page_size")]
		public int PageSize { get; set; }

		[JsonProperty(PropertyName = "columns")]
		public List<DbRecordListItemBase> Columns { get; set; }

		[JsonProperty(PropertyName = "query")]
		public DbRecordListQuery Query { get; set; }

		[JsonProperty(PropertyName = "sorts")]
		public List<DbRecordListSort> Sorts { get; set; }

		[JsonProperty(PropertyName = "relation_options")]
        public List<DbEntityRelationOptions> RelationOptions { get; set; }

		[JsonProperty(PropertyName = "dynamic_html_template")]
		public string DynamicHtmlTemplate { get; set; }

		[JsonProperty(PropertyName = "data_source_url")]
		public string DataSourceUrl { get; set; }

		[JsonProperty(PropertyName = "action_items")]
		public List<ActionItem> ActionItems { get; set; }

		[JsonProperty(PropertyName = "service_code")]
		public string ServiceCode { get; set; }

		[JsonProperty(PropertyName = "column_widths_csv")]
		public string ColumnWidthsCSV { get; set; }
	}

	public class DbRecordListQuery
	{
		[JsonProperty(PropertyName = "query_type")]
		public QueryType QueryType { get; set; }

		[JsonProperty(PropertyName = "field_name")]
		public string FieldName { get; set; }

		[JsonProperty(PropertyName = "field_value")]
		public string FieldValue { get; set; }

		[JsonProperty(PropertyName = "sub_queries")]
		public List<DbRecordListQuery> SubQueries { get; set; }
	}

	public class DbRecordListSort
	{
		[JsonProperty(PropertyName = "field_name")]
		public string FieldName { get; set; }

		[JsonProperty(PropertyName = "sort_type")]
		public QuerySortType SortType { get; set; }
	}

	public abstract class DbRecordListItemBase
	{
		[JsonProperty(PropertyName = "entity_id")]
		public Guid EntityId { get; set; }
	}

	public class DbRecordListFieldItem : DbRecordListItemBase
	{
		[JsonProperty(PropertyName = "field_id")]
		public Guid FieldId { get; set; }
	}

	public class DbRecordListRelationFieldItem : DbRecordListItemBase
	{
		[JsonProperty(PropertyName = "relation_id")]
		public Guid RelationId { get; set; }

		[JsonProperty(PropertyName = "field_id")]
		public Guid FieldId { get; set; }

		[JsonProperty(PropertyName = "field_label")]
		public string FieldLabel { get; set; }

		[JsonProperty(PropertyName = "field_placeholder")]
		public string FieldPlaceholder { get; set; }

		[JsonProperty(PropertyName = "field_help_text")]
		public string FieldHelpText { get; set; }

		[JsonProperty(PropertyName = "field_required")]
		public bool FieldRequired { get; set; }

		[JsonProperty(PropertyName = "field_lookup_list")]
		public string FieldLookupList { get; set; }
	}

	public class DbRecordListViewItem : DbRecordListItemBase
	{
		[JsonProperty(PropertyName = "view_id")]
		public Guid ViewId { get; set; }
	}

	public class DbRecordListRelationViewItem : DbRecordListItemBase
	{
		[JsonProperty(PropertyName = "relation_id")]
		public Guid RelationId { get; set; }

		[JsonProperty(PropertyName = "view_id")]
		public Guid ViewId { get; set; }

		[JsonProperty(PropertyName = "field_label")]
		public string FieldLabel { get; set; }

		[JsonProperty(PropertyName = "field_placeholder")]
		public string FieldPlaceholder { get; set; }

		[JsonProperty(PropertyName = "field_help_text")]
		public string FieldHelpText { get; set; }

		[JsonProperty(PropertyName = "field_required")]
		public bool FieldRequired { get; set; }

		[JsonProperty(PropertyName = "field_lookup_list")]
		public string FieldLookupList { get; set; }

		[JsonProperty(PropertyName = "field_manage_view")]
		public string FieldManageView { get; set; }
	}

	public class DbRecordListListItem : DbRecordListItemBase
	{
		[JsonProperty(PropertyName = "listId")]
		public Guid ListId { get; set; }
	}

	public class DbRecordListRelationListItem : DbRecordListItemBase
	{
		[JsonProperty(PropertyName = "relation_id")]
		public Guid RelationId { get; set; }

		[JsonProperty(PropertyName = "list_id")]
		public Guid ListId { get; set; }

		[JsonProperty(PropertyName = "field_label")]
		public string FieldLabel { get; set; }

		[JsonProperty(PropertyName = "field_placeholder")]
		public string FieldPlaceholder { get; set; }

		[JsonProperty(PropertyName = "field_help_text")]
		public string FieldHelpText { get; set; }

		[JsonProperty(PropertyName = "field_required")]
		public bool FieldRequired { get; set; }

		[JsonProperty(PropertyName = "field_lookup_list")]
		public string FieldLookupList { get; set; }

		[JsonProperty(PropertyName = "field_manage_view")]
		public string FieldManageView { get; set; }
	}

	public class DbRecordListRelationTreeItem : DbRecordListItemBase
	{
		[JsonProperty(PropertyName = "relation_id")]
		public Guid RelationId { get; set; }

		[JsonProperty(PropertyName = "tree_id")]
		public Guid TreeId { get; set; }

		[JsonProperty(PropertyName = "field_label")]
		public string FieldLabel { get; set; }

		[JsonProperty(PropertyName = "field_placeholder")]
		public string FieldPlaceholder { get; set; }

		[JsonProperty(PropertyName = "field_help_text")]
		public string FieldHelpText { get; set; }

		[JsonProperty(PropertyName = "field_required")]
		public bool FieldRequired { get; set; }
	}
}