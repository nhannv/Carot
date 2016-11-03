﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Carot.ERP.Utilities;

namespace Carot.ERP.Api.Models
{
	public enum RecordListType
	{
		General = 0,
        Lookup = 1,
		Hidden = 2
	}

	public enum RecordListItemType
	{
		Field,
		FieldFromRelation,
		View,
		ViewFromRelation,
		List,
		ListFromRelation
	}

	#region << Input classes >>

	public class InputRecordList
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

        [JsonProperty(PropertyName = "visibleColumnsCount")]
        public int? VisibleColumnsCount { get; set; }

        [JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		[JsonProperty(PropertyName = "pageSize")]
		public int? PageSize { get; set; }

		[JsonProperty(PropertyName = "columnWidthsCSV")]
		public string ColumnWidthsCSV { get; set; }

		[JsonProperty(PropertyName = "dynamicHtmlTemplate")]
		public string DynamicHtmlTemplate { get; set; }

		[JsonProperty(PropertyName = "dataSourceUrl")]
		public string DataSourceUrl { get; set; }

		[JsonProperty(PropertyName = "serviceCode")]
		public string ServiceCode { get; set; }

		[JsonProperty(PropertyName = "columns")]
		public List<InputRecordListItemBase> Columns { get; set; }

		[JsonProperty(PropertyName = "query")]
		public InputRecordListQuery Query { get; set; }

		[JsonProperty(PropertyName = "sorts")]
		public List<InputRecordListSort> Sorts { get; set; }

        [JsonProperty(PropertyName = "relationOptions")]
        public List<EntityRelationOptionsItem> RelationOptions { get; set; }

		[JsonProperty(PropertyName = "actionItems")]
		public List<ActionItem> ActionItems { get; set; }

		public static InputRecordList Convert(JObject inputList)
		{
			InputRecordList list = JsonConvert.DeserializeObject<InputRecordList>(inputList.ToString(), new RecordListItemConverter());

			return list;
		}
	}

	public class InputRecordListQuery
	{
		[JsonProperty(PropertyName = "queryType")]
		public string QueryType { get; set; }

		[JsonProperty(PropertyName = "fieldName")]
		public string FieldName { get; set; }

		[JsonProperty(PropertyName = "fieldValue")]
		public string FieldValue { get; set; }

		[JsonProperty(PropertyName = "subQueries")]
		public List<InputRecordListQuery> SubQueries { get; set; }
	}

	public class InputRecordListSort
	{
		[JsonProperty(PropertyName = "fieldName")]
		public string FieldName { get; set; }

		[JsonProperty(PropertyName = "sortType")]
		public string SortType { get; set; }
	}

	public class InputRecordListItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		[JsonProperty(PropertyName = "entityId")]
		public Guid? EntityId { get; set; }

		[JsonProperty(PropertyName = "entityName")]
		public string EntityName { get; set; }
	}

	public class InputRecordListFieldItem : InputRecordListItemBase
	{
		[JsonProperty(PropertyName = "fieldId")]
		public Guid? FieldId { get; set; }

		[JsonProperty(PropertyName = "fieldName")]
		public string FieldName { get; set; }
	}

	public class InputRecordListRelationFieldItem : InputRecordListItemBase
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

	public class InputRecordListViewItem : InputRecordListItemBase
	{
		[JsonProperty(PropertyName = "viewId")]
		public Guid? ViewId { get; set; }

		[JsonProperty(PropertyName = "viewName")]
		public string ViewName { get; set; }
	}

	public class InputRecordListRelationViewItem : InputRecordListItemBase
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

	public class InputRecordListListItem : InputRecordListItemBase
	{
		[JsonProperty(PropertyName = "listId")]
		public Guid? ListId { get; set; }

		[JsonProperty(PropertyName = "listName")]
		public string ListName { get; set; }
	}

	public class InputRecordListRelationListItem : InputRecordListItemBase
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

	public class InputRecordListRelationTreeItem : InputRecordListItemBase
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

	#endregion

	#region << Default classes >>

	[Serializable]
	public class RecordList
	{
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

        [JsonProperty(PropertyName = "visibleColumnsCount")]
        public int VisibleColumnsCount { get; set; }

        [JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		[JsonProperty(PropertyName = "pageSize")]
		public int PageSize { get; set; }

		[JsonProperty(PropertyName = "columnWidthsCSV")]
		public string ColumnWidthsCSV { get; set; }

		[JsonProperty(PropertyName = "dynamicHtmlTemplate")]
		public string DynamicHtmlTemplate { get; set; }
		
		[JsonProperty(PropertyName = "dataSourceUrl")]
		public string DataSourceUrl { get; set; }

		[JsonProperty(PropertyName = "serviceCode")]
		public string ServiceCode { get; set; }

		[JsonProperty(PropertyName = "columns")]
		public List<RecordListItemBase> Columns { get; set; }

		[JsonProperty(PropertyName = "query")]
		public RecordListQuery Query { get; set; }

		[JsonProperty(PropertyName = "sorts")]
		public List<RecordListSort> Sorts { get; set; }

        [JsonProperty(PropertyName = "relationOptions")]
        public List<EntityRelationOptions> RelationOptions { get; set; }

		[JsonProperty(PropertyName = "actionItems")]
		public List<ActionItem> ActionItems { get; set; }
	}

	[Serializable]
	public class RecordListQuery
	{
		[JsonProperty(PropertyName = "queryType")]
		public string QueryType { get; set; }

		[JsonProperty(PropertyName = "fieldName")]
		public string FieldName { get; set; }

		[JsonProperty(PropertyName = "fieldValue")]
		public string FieldValue { get; set; }

		[JsonProperty(PropertyName = "subQueries")]
		public List<RecordListQuery> SubQueries { get; set; }

		public static QueryObject ConvertQuery(RecordListQuery query)
		{
			QueryObject queryObj = new QueryObject();

			QueryType type;
			if (Enum.TryParse<QueryType>(query.QueryType, true, out type))
			{
				queryObj.FieldName = query.FieldName;
				queryObj.FieldValue = query.FieldValue;
				queryObj.QueryType = type;

				if (query.SubQueries != null)
				{
					queryObj.SubQueries = new List<QueryObject>();
					foreach (var subQuery in query.SubQueries)
					{
						QueryObject subQueryObj = ConvertQuery(subQuery);
						queryObj.SubQueries.Add(subQueryObj);
					}
				}
			}
			return queryObj;
		}
	}

	[Serializable]
	public class RecordListSort
	{
		[JsonProperty(PropertyName = "fieldName")]
		public string FieldName { get; set; }

		[JsonProperty(PropertyName = "sortType")]
		public string SortType { get; set; }
	}

	[Serializable]
	public abstract class RecordListItemBase
	{
		[JsonProperty(PropertyName = "dataName")]
		public string DataName { get; set; }

		[JsonProperty(PropertyName = "entityId")]
		public Guid EntityId { get; set; }

		[JsonProperty(PropertyName = "entityName")]
		public string EntityName { get; set; }

		[JsonProperty(PropertyName = "entityLabel")]
		public string EntityLabel { get; set; }

		[JsonProperty(PropertyName = "entityLabelPlural")]
		public string EntityLabelPlural { get; set; }
	}

	[Serializable]
	public class RecordListFieldItem : RecordListItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public static string ItemType { get { return Enum.GetName(typeof(RecordListItemType), RecordListItemType.Field).ToLower(); } }

		[JsonIgnore]
		[JsonProperty(PropertyName = "fieldId")]
		public Guid FieldId { get; set; }

		[JsonProperty(PropertyName = "fieldName")]
		public string FieldName { get; set; }

		[JsonProperty(PropertyName = "meta")]
		public Field Meta { get; set; }
	}

	[Serializable]
	public class RecordListRelationFieldItem : RecordListItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public static string ItemType { get { return "fieldFromRelation" /*Enum.GetName(typeof(RecordListItemType), RecordListItemType.FieldFromRelation).ToLower()*/; } }

		[JsonIgnore]
		[JsonProperty(PropertyName = "fieldId")]
		public Guid FieldId { get; set; }

		[JsonProperty(PropertyName = "fieldName")]
		public string FieldName { get; set; }

		[JsonIgnore]
		[JsonProperty(PropertyName = "relationId")]
		public Guid RelationId { get; set; }

		[JsonProperty(PropertyName = "relationName")]
		public string RelationName { get; set; }

        [JsonProperty(PropertyName = "relationDirection")]
        public string RelationDirection { get; set; }

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
	public class RecordListViewItem : RecordListItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public static string ItemType { get { return Enum.GetName(typeof(RecordListItemType), RecordListItemType.View).ToLower(); } }

		[JsonIgnore]
		[JsonProperty(PropertyName = "viewId")]
		public Guid ViewId { get; set; }

		[JsonProperty(PropertyName = "viewName")]
		public string ViewName { get; set; }

		[JsonProperty(PropertyName = "meta")]
		public RecordView Meta { get; set; }
	}

	[Serializable]
	public class RecordListRelationViewItem : RecordListItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public static string ItemType { get { return "viewFromRelation" /*Enum.GetName(typeof(RecordListItemType), RecordListItemType.ViewFromRelation).ToLower()*/; } }

		[JsonIgnore]
		[JsonProperty(PropertyName = "viewId")]
		public Guid ViewId { get; set; }

		[JsonProperty(PropertyName = "viewName")]
		public string ViewName { get; set; }

		[JsonIgnore]
		[JsonProperty(PropertyName = "relationId")]
		public Guid RelationId { get; set; }

		[JsonProperty(PropertyName = "relationName")]
		public string RelationName { get; set; }

        [JsonProperty(PropertyName = "relationDirection")]
        public string RelationDirection { get; set; }

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
	public class RecordListListItem : RecordListItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public static string ItemType { get { return Enum.GetName(typeof(RecordListItemType), RecordListItemType.List).ToLower(); } }

		[JsonIgnore]
		[JsonProperty(PropertyName = "listId")]
		public Guid ListId { get; set; }

		[JsonProperty(PropertyName = "listName")]
		public string ListName { get; set; }

		[JsonProperty(PropertyName = "meta")]
		public RecordList Meta { get; set; }
	}

	[Serializable]
	public class RecordListRelationListItem : RecordListItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public static string ItemType { get { return "listFromRelation" /*Enum.GetName(typeof(RecordListItemType), RecordListItemType.ListFromRelation).ToLower()*/; } }

		[JsonIgnore]
		[JsonProperty(PropertyName = "listId")]
		public Guid ListId { get; set; }

		[JsonProperty(PropertyName = "listName")]
		public string ListName { get; set; }

		[JsonIgnore]
		[JsonProperty(PropertyName = "relationId")]
		public Guid RelationId { get; set; }

		[JsonProperty(PropertyName = "relationName")]
		public string RelationName { get; set; }

        [JsonProperty(PropertyName = "relationDirection")]
        public string RelationDirection { get; set; }

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
	public class RecordListRelationTreeItem : RecordListItemBase
	{
		[JsonProperty(PropertyName = "type")]
		public static string ItemType { get { return "treeFromRelation" ; } }

		[JsonProperty(PropertyName = "treeId")]
		public Guid TreeId { get; set; }

		[JsonProperty(PropertyName = "treeName")]
		public string TreeName { get; set; }

		[JsonProperty(PropertyName = "relationId")]
		public Guid RelationId { get; set; }

		[JsonProperty(PropertyName = "relationName")]
		public string RelationName { get; set; }

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

	#endregion

	[Serializable]
	public class RecordListCollection
	{
		[JsonProperty(PropertyName = "recordLists")]
		public List<RecordList> RecordLists { get; set; }
	}

	public class RecordListResponse : BaseResponseModel
	{
		[JsonProperty(PropertyName = "object")]
		public RecordList Object { get; set; }
	}

	public class RecordListCollectionResponse : BaseResponseModel
	{
		[JsonProperty(PropertyName = "object")]
		public RecordListCollection Object { get; set; }
	}

	public class RecordListRecordResponse : BaseResponseModel
	{
		[JsonProperty(PropertyName = "object")]
		public object Object { get; set; }
	}

	//public class RecordListRecord
	//{
	//	[JsonProperty(PropertyName = "data")]
	//	public object Data { get; set; }

	//	[JsonProperty(PropertyName = "meta")]
	//	public RecordList Meta { get; set; }
	//}

	public class RecordListItemConverter : JsonCreationConverter<InputRecordListItemBase>
	{
		protected override InputRecordListItemBase Create(Type objectType, JObject jObject)
		{
			string type = jObject["type"].ToString().ToLower();

			if (type == "fieldfromrelation")
				return new InputRecordListRelationFieldItem();

			if (type == "view")
				return new InputRecordListViewItem();

			if (type == "viewfromrelation")
				return new InputRecordListRelationViewItem();

			if (type == "list")
				return new InputRecordListListItem();

			if (type == "listfromrelation")
				return new InputRecordListRelationListItem();

			if (type == "treefromrelation")
				return new InputRecordListRelationTreeItem();

			return new InputRecordListFieldItem();
		}
	}
}