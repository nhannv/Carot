﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Carot.ERP.Utilities;

namespace Carot.ERP.Api.Models
{
	public enum RecordViewType
	{
		General = 0,
		Quick_View = 1,
		Create = 2,
		Quick_Create = 3,
		Hidden = 4
	}

	public enum RecordViewItemType
	{
		Html,
		Field,
		FieldFromRelation,
		List,
		View
	}

	#region << Input Classes >>

	public class InputRecordView
	{
		[JsonProperty(PropertyName = "id")]
		public Guid? Id { get; set; }

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

		[JsonProperty(PropertyName = "cssClass")]
		public string CssClass { get; set; }

        [JsonProperty(PropertyName = "iconName")]
        public string IconName { get; set; }

        [JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		[JsonProperty(PropertyName = "dynamicHtmlTemplate")]
		public string DynamicHtmlTemplate { get; set; }

		[JsonProperty(PropertyName = "dataSourceUrl")]
		public string DataSourceUrl { get; set; }

		[JsonProperty(PropertyName = "serviceCode")]
		public string ServiceCode { get; set; }

		[JsonProperty(PropertyName = "regions")]
		public List<InputRecordViewRegion> Regions { get; set; }

        [JsonProperty(PropertyName = "relationOptions")]
        public List<EntityRelationOptionsItem> RelationOptions { get; set; }

        [JsonProperty(PropertyName = "sidebar")]
		public InputRecordViewSidebar Sidebar { get; set; }

		[JsonProperty(PropertyName = "actionItems")]
		public List<ActionItem> ActionItems { get; set; }

		public static InputRecordView Convert(JObject inputField)
		{
			JsonConverter itemConverter = new RecordViewItemConverter();
			JsonConverter sidebarItemConverter = new RecordViewSidebarItemConverter();
            InputRecordView view = JsonConvert.DeserializeObject<InputRecordView>(inputField.ToString(), new [] { itemConverter, sidebarItemConverter });

			return view;
		}
	}

    ////////////////////////
    public class InputRecordViewSidebar
	{
		public InputRecordViewSidebar() {
			Render = true;
			CssClass = "";
		}
	
		[JsonProperty(PropertyName = "render")]
		public bool? Render { get; set; }

		[JsonProperty(PropertyName = "cssClass")]
		public string CssClass { get; set; }

		[JsonProperty(PropertyName = "items")]
		public List<InputRecordViewSidebarItemBase> Items { get; set; }
	}

	////////////////////////
	public class InputRecordViewSidebarItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		[JsonProperty(PropertyName = "entityId")]
		public Guid? EntityId { get; set; }

		[JsonProperty(PropertyName = "entityName")]
		public string EntityName { get; set; }
	}

	public class InputRecordViewSidebarListItem : InputRecordViewSidebarItemBase
	{
		[JsonProperty(PropertyName = "listId")]
		public Guid? ListId { get; set; }

		[JsonProperty(PropertyName = "listName")]
		public string ListName { get; set; }
	}

	public class InputRecordViewSidebarViewItem : InputRecordViewSidebarItemBase
	{
		[JsonProperty(PropertyName = "viewId")]
		public Guid? ViewId { get; set; }

		[JsonProperty(PropertyName = "viewName")]
		public string ViewName { get; set; }
	}

	public class InputRecordViewSidebarRelationViewItem : InputRecordViewSidebarItemBase
	{
		[JsonProperty(PropertyName = "relationId")]
		public Guid? RelationId { get; set; }

		[JsonProperty(PropertyName = "relationName")]
		public string RelationName { get; set; }

		[JsonProperty(PropertyName = "viewId")]
		public Guid? ViewId { get; set; }

		[JsonProperty(PropertyName = "viewName")]
		public string ViewName { get; set; }

        [JsonProperty(PropertyName = "fieldLabel")]
        public string FieldLabel { get; set; }

        [JsonProperty(PropertyName = "fieldPlaceholder")]
        public string FieldPlaceholder { get; set; }

        [JsonProperty(PropertyName = "fieldHelpText")]
        public string FieldHelpText { get; set; }

        [JsonProperty(PropertyName = "fieldRequired")]
        public bool FieldRequired { get; set; }

        [JsonProperty(PropertyName = "fieldLookupList")]
        public string FieldLookupList { get; set; }

		[JsonProperty(PropertyName = "fieldManageView")]
		public string FieldManageView { get; set; }
	}

	public class InputRecordViewSidebarRelationListItem : InputRecordViewSidebarItemBase
	{
		[JsonProperty(PropertyName = "relationId")]
		public Guid? RelationId { get; set; }

		[JsonProperty(PropertyName = "relationName")]
		public string RelationName { get; set; }

		[JsonProperty(PropertyName = "listId")]
		public Guid? ListId { get; set; }

		[JsonProperty(PropertyName = "listName")]
		public string ListName { get; set; }

        [JsonProperty(PropertyName = "fieldLabel")]
        public string FieldLabel { get; set; }

        [JsonProperty(PropertyName = "fieldPlaceholder")]
        public string FieldPlaceholder { get; set; }

        [JsonProperty(PropertyName = "fieldHelpText")]
        public string FieldHelpText { get; set; }

        [JsonProperty(PropertyName = "fieldRequired")]
        public bool FieldRequired { get; set; }

        [JsonProperty(PropertyName = "fieldLookupList")]
        public string FieldLookupList { get; set; }

		[JsonProperty(PropertyName = "fieldManageView")]
		public string FieldManageView { get; set; }
	}

	public class InputRecordViewSidebarRelationTreeItem : InputRecordViewSidebarItemBase
	{
		[JsonProperty(PropertyName = "relationId")]
		public Guid? RelationId { get; set; }

		[JsonProperty(PropertyName = "relationName")]
		public string RelationName { get; set; }

		[JsonProperty(PropertyName = "treeId")]
		public Guid? TreeId { get; set; }

		[JsonProperty(PropertyName = "treeName")]
		public string TreeName { get; set; }

		[JsonProperty(PropertyName = "fieldLabel")]
		public string FieldLabel { get; set; }

		[JsonProperty(PropertyName = "fieldPlaceholder")]
		public string FieldPlaceholder { get; set; }

		[JsonProperty(PropertyName = "fieldHelpText")]
		public string FieldHelpText { get; set; }

		[JsonProperty(PropertyName = "fieldRequired")]
		public bool FieldRequired { get; set; }
	}

	////////////////////////
	public class InputRecordViewRegion
	{
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "label")]
		public string Label { get; set; }

		[JsonProperty(PropertyName = "render")]
		public bool? Render { get; set; }

		[JsonProperty(PropertyName = "cssClass")]
		public string CssClass { get; set; }

		[JsonProperty(PropertyName = "weight")]
		public decimal? Weight { get; set; }

		[JsonProperty(PropertyName = "sections")]
		public List<InputRecordViewSection> Sections { get; set; }
	}

	////////////////////////
	public class InputRecordViewSection
	{
		[JsonProperty(PropertyName = "id")]
		public Guid? Id { get; set; }

		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "label")]
		public string Label { get; set; }

		[JsonProperty(PropertyName = "cssClass")]
		public string CssClass { get; set; }

		[JsonProperty(PropertyName = "showLabel")]
		public bool? ShowLabel { get; set; }

		[JsonProperty(PropertyName = "collapsed")]
		public bool? Collapsed { get; set; }

		[JsonProperty(PropertyName = "weight")]
		public decimal? Weight { get; set; }

		[JsonProperty(PropertyName = "tabOrder")]
		public string TabOrder { get; set; }

		[JsonProperty(PropertyName = "rows")]
		public List<InputRecordViewRow> Rows { get; set; }

	}

	////////////////////////
	public class InputRecordViewRow
	{
		[JsonProperty(PropertyName = "id")]
		public Guid? Id { get; set; }

		[JsonProperty(PropertyName = "weight")]
		public decimal? Weight { get; set; }

		[JsonProperty(PropertyName = "columns")]
		public List<InputRecordViewColumn> Columns { get; set; }
	}

	////////////////////////
	public class InputRecordViewColumn
	{
		//[JsonConverter(typeof(RecordViewItemConverter))]
		[JsonProperty(PropertyName = "items")]
		public List<InputRecordViewItemBase> Items { get; set; }

		[JsonProperty(PropertyName = "gridColCount")]
		public int? GridColCount { get; set; }
	}



	////////////////////////
	public class InputRecordViewItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		[JsonProperty(PropertyName = "entityId")]
		public Guid? EntityId { get; set; }

		[JsonProperty(PropertyName = "entityName")]
		public string EntityName { get; set; }
	}

	public class InputRecordViewFieldItem : InputRecordViewItemBase
	{
		[JsonProperty(PropertyName = "fieldId")]
		public Guid? FieldId { get; set; }

		[JsonProperty(PropertyName = "fieldName")]
		public string FieldName { get; set; }
	}

	public class InputRecordViewListItem : InputRecordViewItemBase
	{
		[JsonProperty(PropertyName = "listId")]
		public Guid? ListId { get; set; }

		[JsonProperty(PropertyName = "listName")]
		public string ListName { get; set; }
	}

	public class InputRecordViewViewItem : InputRecordViewItemBase
	{
		[JsonProperty(PropertyName = "viewId")]
		public Guid? ViewId { get; set; }

		[JsonProperty(PropertyName = "viewName")]
		public string ViewName { get; set; }
	}

	public class InputRecordViewRelationFieldItem : InputRecordViewItemBase
	{
		[JsonProperty(PropertyName = "relationId")]
		public Guid? RelationId { get; set; }

		[JsonProperty(PropertyName = "relationName")]
		public string RelationName { get; set; }

		[JsonProperty(PropertyName = "fieldId")]
		public Guid? FieldId { get; set; }

		[JsonProperty(PropertyName = "fieldName")]
		public string FieldName { get; set; }

        [JsonProperty(PropertyName = "fieldLabel")]
        public string FieldLabel { get; set; }

        [JsonProperty(PropertyName = "fieldPlaceholder")]
        public string FieldPlaceholder { get; set; }

        [JsonProperty(PropertyName = "fieldHelpText")]
        public string FieldHelpText { get; set; }

        [JsonProperty(PropertyName = "fieldRequired")]
        public bool FieldRequired { get; set; }

		[JsonProperty(PropertyName = "fieldLookupList")]
		public string FieldLookupList { get; set; }
	}

	public class InputRecordViewRelationViewItem : InputRecordViewItemBase
	{
		[JsonProperty(PropertyName = "relationId")]
		public Guid? RelationId { get; set; }

		[JsonProperty(PropertyName = "relationName")]
		public string RelationName { get; set; }

		[JsonProperty(PropertyName = "viewId")]
		public Guid? ViewId { get; set; }

		[JsonProperty(PropertyName = "viewName")]
		public string ViewName { get; set; }

        [JsonProperty(PropertyName = "fieldLabel")]
        public string FieldLabel { get; set; }

        [JsonProperty(PropertyName = "fieldPlaceholder")]
        public string FieldPlaceholder { get; set; }

        [JsonProperty(PropertyName = "fieldHelpText")]
        public string FieldHelpText { get; set; }

        [JsonProperty(PropertyName = "fieldRequired")]
        public bool FieldRequired { get; set; }

        [JsonProperty(PropertyName = "fieldLookupList")]
        public string FieldLookupList { get; set; }

		[JsonProperty(PropertyName = "fieldManageView")]
		public string FieldManageView { get; set; }
	}

	public class InputRecordViewRelationListItem : InputRecordViewItemBase
	{
		[JsonProperty(PropertyName = "relationId")]
		public Guid? RelationId { get; set; }

		[JsonProperty(PropertyName = "relationName")]
		public string RelationName { get; set; }

		[JsonProperty(PropertyName = "listId")]
		public Guid? ListId { get; set; }

		[JsonProperty(PropertyName = "listName")]
		public string ListName { get; set; }

        [JsonProperty(PropertyName = "fieldLabel")]
        public string FieldLabel { get; set; }

        [JsonProperty(PropertyName = "fieldPlaceholder")]
        public string FieldPlaceholder { get; set; }

        [JsonProperty(PropertyName = "fieldHelpText")]
        public string FieldHelpText { get; set; }

        [JsonProperty(PropertyName = "fieldRequired")]
        public bool FieldRequired { get; set; }

        [JsonProperty(PropertyName = "fieldLookupList")]
        public string FieldLookupList { get; set; }

		[JsonProperty(PropertyName = "fieldManageView")]
		public string FieldManageView { get; set; }
	}

	public class InputRecordViewRelationTreeItem : InputRecordViewItemBase
	{
		[JsonProperty(PropertyName = "relationId")]
		public Guid? RelationId { get; set; }

		[JsonProperty(PropertyName = "relationName")]
		public string RelationName { get; set; }

		[JsonProperty(PropertyName = "treeId")]
		public Guid? TreeId { get; set; }

		[JsonProperty(PropertyName = "treeName")]
		public string TreeName { get; set; }

		[JsonProperty(PropertyName = "fieldLabel")]
		public string FieldLabel { get; set; }

		[JsonProperty(PropertyName = "fieldPlaceholder")]
		public string FieldPlaceholder { get; set; }

		[JsonProperty(PropertyName = "fieldHelpText")]
		public string FieldHelpText { get; set; }

		[JsonProperty(PropertyName = "fieldRequired")]
		public bool FieldRequired { get; set; }
	}

	public class InputRecordViewHtmlItem : InputRecordViewItemBase
	{
		[JsonProperty(PropertyName = "tag")]
		public string Tag { get; set; }

		[JsonProperty(PropertyName = "content")]
		public string Content { get; set; }
	}

	#endregion

	#region << Default Classes >>

	[Serializable]
	public class RecordView 
	{
		public RecordView()
		{
			Id = Guid.NewGuid();
			Name = "";
			Label = "";
			Title = "";
			Default = false;
			System = false;
			Weight = 1;
			CssClass = "";
            IconName = "";
			Type = Enum.GetName(typeof(RecordViewType), RecordViewType.General).ToLower();
			Regions = new List<RecordViewRegion>();
			Sidebar = new RecordViewSidebar();
            RelationOptions = new List<EntityRelationOptions>();
			DynamicHtmlTemplate = "";
			DataSourceUrl = "";
			ActionItems = new List<ActionItem>();
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

		[JsonProperty(PropertyName = "cssClass")]
		public string CssClass { get; set; }

        [JsonProperty(PropertyName = "iconName")]
        public string IconName { get; set; }

        [JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		[JsonProperty(PropertyName = "dynamicHtmlTemplate")]
		public string DynamicHtmlTemplate { get; set; }

		[JsonProperty(PropertyName = "dataSourceUrl")]
		public string DataSourceUrl { get; set; }

		[JsonProperty(PropertyName = "serviceCode")]
		public string ServiceCode { get; set; }

		[JsonProperty(PropertyName = "regions")]
		public List<RecordViewRegion> Regions { get; set; }

        [JsonProperty(PropertyName = "relationOptions")]
        public List<EntityRelationOptions> RelationOptions { get; set; }

        [JsonProperty(PropertyName = "sidebar")]
		public RecordViewSidebar Sidebar { get; set; }

		[JsonProperty(PropertyName = "actionItems")]
		public List<ActionItem> ActionItems { get; set; }

	}

	////////////////////////
	[Serializable]
	public class RecordViewSidebar
	{
		public RecordViewSidebar()
		{
			Render = false;
			CssClass = "";
			Items = new List<RecordViewSidebarItemBase>();
		}

		[JsonProperty(PropertyName = "render")]
		public bool Render { get; set; }

		[JsonProperty(PropertyName = "cssClass")]
		public string CssClass { get; set; }

		[JsonProperty(PropertyName = "items")]
		public List<RecordViewSidebarItemBase> Items { get; set; }
	}

	////////////////////////
	[Serializable]
	public class RecordViewSidebarItemBase
	{
		[JsonProperty(PropertyName = "dataName")]
		public string DataName { get; set; }

		[JsonProperty(PropertyName = "entityId")]
		public Guid EntityId { get; set; }

		[JsonProperty(PropertyName = "entityLabel")]
		public string EntityLabel { get; set; }

		[JsonProperty(PropertyName = "entityName")]
		public string EntityName { get; set; }

		[JsonProperty(PropertyName = "entityLabelPlural")]
		public string EntityLabelPlural { get; set; }
	}

	[Serializable]
	public class RecordViewSidebarListItem : RecordViewSidebarItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public static string ItemType { get { return Enum.GetName(typeof(RecordViewItemType), RecordViewItemType.List).ToLower(); } }

		[JsonProperty(PropertyName = "listId")]
		public Guid ListId { get; set; }

		[JsonProperty(PropertyName = "listName")]
		public string ListName { get; set; }

		[JsonProperty(PropertyName = "meta")]
		public RecordList Meta { get; set; }
	}

	[Serializable]
	public class RecordViewSidebarViewItem : RecordViewSidebarItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public static string ItemType { get { return Enum.GetName(typeof(RecordViewItemType), RecordViewItemType.View).ToLower(); } }

		[JsonProperty(PropertyName = "viewId")]
		public Guid ViewId { get; set; }

		[JsonProperty(PropertyName = "viewName")]
		public string ViewName { get; set; }

		[JsonProperty(PropertyName = "meta")]
		public RecordView Meta { get; set; }
	}

	[Serializable]
	public class RecordViewSidebarRelationViewItem : RecordViewSidebarItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public static string ItemType { get { return "viewFromRelation"; } }

		[JsonProperty(PropertyName = "relationId")]
		public Guid RelationId { get; set; }

		[JsonProperty(PropertyName = "relationName")]
		public string RelationName { get; set; }

        [JsonProperty(PropertyName = "relationDirection")]
        public string RelationDirection { get; set; }

        [JsonProperty(PropertyName = "viewId")]
		public Guid ViewId { get; set; }

		[JsonProperty(PropertyName = "viewName")]
		public string ViewName { get; set; }

		[JsonProperty(PropertyName = "meta")]
		public RecordView Meta { get; set; }

        [JsonProperty(PropertyName = "fieldLabel")]
        public string FieldLabel { get; set; }

        [JsonProperty(PropertyName = "fieldPlaceholder")]
        public string FieldPlaceholder { get; set; }

        [JsonProperty(PropertyName = "fieldHelpText")]
        public string FieldHelpText { get; set; }

        [JsonProperty(PropertyName = "fieldRequired")]
        public bool FieldRequired { get; set; }

        [JsonProperty(PropertyName = "fieldLookupList")]
        public string FieldLookupList { get; set; }

		[JsonProperty(PropertyName = "fieldManageView")]
		public string FieldManageView { get; set; }
	}

	[Serializable]
	public class RecordViewSidebarRelationListItem : RecordViewSidebarItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public static string ItemType { get { return "listFromRelation"; } }

		[JsonProperty(PropertyName = "relationId")]
		public Guid RelationId { get; set; }

		[JsonProperty(PropertyName = "relationName")]
		public string RelationName { get; set; }

        [JsonProperty(PropertyName = "relationDirection")]
        public string RelationDirection { get; set; }

        [JsonProperty(PropertyName = "listId")]
		public Guid ListId { get; set; }

		[JsonProperty(PropertyName = "listName")]
		public string ListName { get; set; }

		[JsonProperty(PropertyName = "meta")]
		public RecordList Meta { get; set; }

        [JsonProperty(PropertyName = "fieldLabel")]
        public string FieldLabel { get; set; }

        [JsonProperty(PropertyName = "fieldPlaceholder")]
        public string FieldPlaceholder { get; set; }

        [JsonProperty(PropertyName = "fieldHelpText")]
        public string FieldHelpText { get; set; }

        [JsonProperty(PropertyName = "fieldRequired")]
        public bool FieldRequired { get; set; }

        [JsonProperty(PropertyName = "fieldLookupList")]
        public string FieldLookupList { get; set; }

		[JsonProperty(PropertyName = "fieldManageView")]
		public string FieldManageView { get; set; }
	}

	[Serializable]
	public class RecordViewSidebarRelationTreeItem : RecordViewSidebarItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public static string ItemType { get { return "treeFromRelation"; } }

		[JsonProperty(PropertyName = "relationId")]
		public Guid RelationId { get; set; }

		[JsonProperty(PropertyName = "relationName")]
		public string RelationName { get; set; }

		[JsonProperty(PropertyName = "treeId")]
		public Guid TreeId { get; set; }

		[JsonProperty(PropertyName = "treeName")]
		public string TreeName { get; set; }

		[JsonProperty(PropertyName = "meta")]
		public RecordTree Meta { get; set; }

		[JsonProperty(PropertyName = "fieldLabel")]
		public string FieldLabel { get; set; }

		[JsonProperty(PropertyName = "fieldPlaceholder")]
		public string FieldPlaceholder { get; set; }

		[JsonProperty(PropertyName = "fieldHelpText")]
		public string FieldHelpText { get; set; }

		[JsonProperty(PropertyName = "fieldRequired")]
		public bool FieldRequired { get; set; }
	}

	////////////////////////
	[Serializable]
	public class RecordViewRegion
	{
		public RecordViewRegion()
		{
			Label = "";
			Name = "";
			Render = true;
			CssClass = "";
			Sections = new List<RecordViewSection>();
			Weight = 10;
		}

		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "label")]
		public string Label { get; set; }

		[JsonProperty(PropertyName = "render")]
		public bool Render { get; set; }

		[JsonProperty(PropertyName = "cssClass")]
		public string CssClass { get; set; }

		[JsonProperty(PropertyName = "weight")]
		public decimal? Weight { get; set; }

		[JsonProperty(PropertyName = "sections")]
		public List<RecordViewSection> Sections { get; set; }
	}

	////////////////////////
	[Serializable]
	public class RecordViewSection
	{

		public RecordViewSection()
		{
			Id = Guid.NewGuid();
			Name = "";
			Label = "";
			CssClass = "";
			ShowLabel = true;
			Collapsed = false;
			Weight = 1;
			TabOrder = "left-right";
			Rows = new List<RecordViewRow>();
		}

		[JsonProperty(PropertyName = "id")]
		public Guid Id { get; set; }

		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "label")]
		public string Label { get; set; }

		[JsonProperty(PropertyName = "cssClass")]
		public string CssClass { get; set; }

		[JsonProperty(PropertyName = "showLabel")]
		public bool ShowLabel { get; set; }

		[JsonProperty(PropertyName = "collapsed")]
		public bool Collapsed { get; set; }

		[JsonProperty(PropertyName = "weight")]
		public decimal? Weight { get; set; }

		[JsonProperty(PropertyName = "tabOrder")]
		public string TabOrder { get; set; }

		[JsonProperty(PropertyName = "rows")]
		public List<RecordViewRow> Rows { get; set; }

	}

	////////////////////////
	[Serializable]
	public class RecordViewRow
	{
		public RecordViewRow()
		{
			Id = Guid.NewGuid();
			Weight = 1;
			Columns = new List<RecordViewColumn>();
		}

		[JsonProperty(PropertyName = "id")]
		public Guid Id { get; set; }

		[JsonProperty(PropertyName = "weight")]
		public decimal? Weight { get; set; }

		[JsonProperty(PropertyName = "columns")]
		public List<RecordViewColumn> Columns { get; set; }

	}

	////////////////////////
	[Serializable]
	public class RecordViewColumn
	{
		public RecordViewColumn()
		{
			Items = new List<RecordViewItemBase>();
			GridColCount = 0;
		}

		[JsonProperty(PropertyName = "items")]
		public List<RecordViewItemBase> Items { get; set; }

		[JsonProperty(PropertyName = "gridColCount")]
		public int GridColCount { get; set; }
	}

	////////////////////////
	[Serializable]
	public abstract class RecordViewItemBase
	{
		[JsonProperty(PropertyName = "dataName")]
		public string DataName { get; set; }

		[JsonProperty(PropertyName = "entityId")]
		public Guid EntityId { get; set; }

		[JsonProperty(PropertyName = "entityLabel")]
		public string EntityLabel { get; set; }

		[JsonProperty(PropertyName = "entityName")]
		public string EntityName { get; set; }

		[JsonProperty(PropertyName = "entityLabelPlural")]
		public string EntityLabelPlural { get; set; }
	}

	[Serializable]
	public class RecordViewFieldItem : RecordViewItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public static string ItemType { get { return Enum.GetName(typeof(RecordViewItemType), RecordViewItemType.Field).ToLower(); } }

		[JsonProperty(PropertyName = "fieldId")]
		public Guid FieldId { get; set; }

		[JsonProperty(PropertyName = "fieldName")]
		public string FieldName { get; set; }

		[JsonProperty(PropertyName = "meta")]
		public Field Meta { get; set; }
	}

	[Serializable]
	public class RecordViewListItem : RecordViewItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public static string ItemType { get { return Enum.GetName(typeof(RecordViewItemType), RecordViewItemType.List).ToLower(); } }

		[JsonProperty(PropertyName = "listId")]
		public Guid ListId { get; set; }

		[JsonProperty(PropertyName = "listName")]
		public string ListName { get; set; }

		[JsonProperty(PropertyName = "meta")]
		public RecordList Meta { get; set; }
	}

	[Serializable]
	public class RecordViewViewItem : RecordViewItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public static string ItemType { get { return Enum.GetName(typeof(RecordViewItemType), RecordViewItemType.View).ToLower(); } }

		[JsonProperty(PropertyName = "viewId")]
		public Guid ViewId { get; set; }

		[JsonProperty(PropertyName = "viewName")]
		public string ViewName { get; set; }

		[JsonProperty(PropertyName = "meta")]
		public RecordView Meta { get; set; }
	}

	[Serializable]
	public class RecordViewRelationFieldItem : RecordViewItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public static string ItemType { get { return "fieldFromRelation"; } }

		[JsonProperty(PropertyName = "relationId")]
		public Guid RelationId { get; set; }

		[JsonProperty(PropertyName = "relationName")]
		public string RelationName { get; set; }

        [JsonProperty(PropertyName = "relationDirection")]
        public string RelationDirection { get; set; }

        [JsonProperty(PropertyName = "fieldId")]
		public Guid FieldId { get; set; }

		[JsonProperty(PropertyName = "fieldName")]
		public string FieldName { get; set; }

		[JsonProperty(PropertyName = "meta")]
		public Field Meta { get; set; }

        [JsonProperty(PropertyName = "fieldLabel")]
        public string FieldLabel { get; set; }

        [JsonProperty(PropertyName = "fieldPlaceholder")]
        public string FieldPlaceholder { get; set; }

        [JsonProperty(PropertyName = "fieldHelpText")]
        public string FieldHelpText { get; set; }

        [JsonProperty(PropertyName = "fieldRequired")]
        public bool FieldRequired { get; set; }

        [JsonProperty(PropertyName = "fieldLookupList")]
        public string FieldLookupList { get; set; }
    }

	[Serializable]
	public class RecordViewRelationViewItem : RecordViewItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public static string ItemType { get { return "viewFromRelation"; } }

		[JsonProperty(PropertyName = "relationId")]
		public Guid RelationId { get; set; }

		[JsonProperty(PropertyName = "relationName")]
		public string RelationName { get; set; }

        [JsonProperty(PropertyName = "relationDirection")]
        public string RelationDirection { get; set; }

        [JsonProperty(PropertyName = "viewId")]
		public Guid ViewId { get; set; }

		[JsonProperty(PropertyName = "viewName")]
		public string ViewName { get; set; }

		[JsonProperty(PropertyName = "meta")]
		public RecordView Meta { get; set; }

        [JsonProperty(PropertyName = "fieldLabel")]
        public string FieldLabel { get; set; }

        [JsonProperty(PropertyName = "fieldPlaceholder")]
        public string FieldPlaceholder { get; set; }

        [JsonProperty(PropertyName = "fieldHelpText")]
        public string FieldHelpText { get; set; }

        [JsonProperty(PropertyName = "fieldRequired")]
        public bool FieldRequired { get; set; }

        [JsonProperty(PropertyName = "fieldLookupList")]
        public string FieldLookupList { get; set; }

        [JsonProperty(PropertyName = "fieldManageView")]
        public string FieldManageView { get; set; }
    }

	[Serializable]
	public class RecordViewRelationListItem : RecordViewItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public static string ItemType { get { return "listFromRelation"; } }

		[JsonProperty(PropertyName = "relationId")]
		public Guid RelationId { get; set; }

		[JsonProperty(PropertyName = "relationName")]
		public string RelationName { get; set; }

        [JsonProperty(PropertyName = "relationDirection")]
        public string RelationDirection { get; set; }

        [JsonProperty(PropertyName = "listId")]
		public Guid ListId { get; set; }

		[JsonProperty(PropertyName = "listName")]
		public string ListName { get; set; }

		[JsonProperty(PropertyName = "meta")]
		public RecordList Meta { get; set; }

        [JsonProperty(PropertyName = "fieldLabel")]
        public string FieldLabel { get; set; }

        [JsonProperty(PropertyName = "fieldPlaceholder")]
        public string FieldPlaceholder { get; set; }

        [JsonProperty(PropertyName = "fieldHelpText")]
        public string FieldHelpText { get; set; }

        [JsonProperty(PropertyName = "fieldRequired")]
        public bool FieldRequired { get; set; }

        [JsonProperty(PropertyName = "fieldLookupList")]
        public string FieldLookupList { get; set; }

        [JsonProperty(PropertyName = "fieldManageView")]
        public string FieldManageView { get; set; }
    }

	[Serializable]
	public class RecordViewRelationTreeItem : RecordViewItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public static string ItemType { get { return "treeFromRelation"; } }

		[JsonProperty(PropertyName = "relationId")]
		public Guid RelationId { get; set; }

		[JsonProperty(PropertyName = "relationName")]
		public string RelationName { get; set; }

		[JsonProperty(PropertyName = "treeId")]
		public Guid TreeId { get; set; }

		[JsonProperty(PropertyName = "treeName")]
		public string TreeName { get; set; }

		[JsonProperty(PropertyName = "meta")]
		public RecordTree Meta { get; set; }

		[JsonProperty(PropertyName = "fieldLabel")]
		public string FieldLabel { get; set; }

		[JsonProperty(PropertyName = "fieldPlaceholder")]
		public string FieldPlaceholder { get; set; }

		[JsonProperty(PropertyName = "fieldHelpText")]
		public string FieldHelpText { get; set; }

		[JsonProperty(PropertyName = "fieldRequired")]
		public bool FieldRequired { get; set; }
	}

	[Serializable]
	public class RecordViewHtmlItem : RecordViewItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public static string ItemType { get { return Enum.GetName(typeof(RecordViewItemType), RecordViewItemType.Html).ToLower(); } }

		[JsonProperty(PropertyName = "tag")]
		public string Tag { get; set; }

		[JsonProperty(PropertyName = "content")]
		public string Content { get; set; }
	}

	#endregion

	[Serializable]
	public class RecordViewCollection
	{
		[JsonProperty(PropertyName = "recordViews")]
		public List<RecordView> RecordViews { get; set; }
	}

	public class RecordViewResponse : BaseResponseModel
	{
		[JsonProperty(PropertyName = "object")]
		public RecordView Object { get; set; }
	}

	public class RecordViewCollectionResponse : BaseResponseModel
	{
		[JsonProperty(PropertyName = "object")]
		public RecordViewCollection Object { get; set; }
	}

	public class RecordViewRecordResponse : BaseResponseModel
	{
		[JsonProperty(PropertyName = "object")]
		public object Object { get; set; }
	}

	//public class RecordViewRecord
	//{
	//	[JsonProperty(PropertyName = "data")]
	//	public object Data { get; set; }

	//	[JsonProperty(PropertyName = "meta")]
	//	public RecordView Meta { get; set; }
	//}

	public class RecordViewItemConverter : JsonCreationConverter<InputRecordViewItemBase>
	{
		protected override InputRecordViewItemBase Create(Type objectType, JObject jObject)
		{
			string type = jObject["type"].ToString().ToLower();

			if (type == "list")
				return new InputRecordViewListItem();
			if (type == "view")
				return new InputRecordViewViewItem();
			if (type == "fieldfromrelation")
				return new InputRecordViewRelationFieldItem();
			if (type == "viewfromrelation")
				return new InputRecordViewRelationViewItem();
			if (type == "listfromrelation")
				return new InputRecordViewRelationListItem();
			if (type == "treefromrelation")
				return new InputRecordViewRelationTreeItem();
			if (type == "html")
				return new InputRecordViewHtmlItem();

			return new InputRecordViewFieldItem();
		}
	}

	public class RecordViewSidebarItemConverter : JsonCreationConverter<InputRecordViewSidebarItemBase>
	{
		protected override InputRecordViewSidebarItemBase Create(Type objectType, JObject jObject)
		{
			string type = jObject["type"].ToString().ToLower();

			if (type == "view")
				return new InputRecordViewSidebarViewItem();
			if (type == "viewfromrelation")
				return new InputRecordViewSidebarRelationViewItem();
			if (type == "listfromrelation")
				return new InputRecordViewSidebarRelationListItem();
			if (type == "treefromrelation")
				return new InputRecordViewSidebarRelationTreeItem();

			return new InputRecordViewSidebarListItem();
		}
	}


}