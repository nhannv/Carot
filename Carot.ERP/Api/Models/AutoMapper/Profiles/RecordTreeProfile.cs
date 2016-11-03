﻿using AutoMapper;
using System;
using System.Collections.Generic;
using Carot.ERP.Database;

namespace Carot.ERP.Api.Models.AutoMapper.Profiles
{
	internal class RecordTreeProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<RecordTree, InputRecordTree>();
			Mapper.CreateMap<InputRecordTree, RecordTree>()
				.ForMember(x => x.Id, opt => opt.MapFrom(y => (y.Id.HasValue) ? y.Id.Value : Guid.Empty));
			Mapper.CreateMap<RecordTree, DbRecordTree>()
				.ForMember(x => x.RootNodes, opt => opt.MapFrom(y => PopulateRootNodesToStorage(y)));
			Mapper.CreateMap<DbRecordTree, RecordTree>()
				.ForMember(x => x.RootNodes, opt => opt.MapFrom(y => PopulateRootNodes(y)));
		}

		protected List<RecordTreeNode> PopulateRootNodes(DbRecordTree storageTree)
		{
			List<RecordTreeNode> nodes = new List<RecordTreeNode>();
			if (storageTree.RootNodes == null)
				return nodes;

			foreach (var id in storageTree.RootNodes)
				nodes.Add(new RecordTreeNode { RecordId = id });

			return nodes;
		}

		protected List<Guid> PopulateRootNodesToStorage(RecordTree tree)
		{
			List<Guid> nodeIds = new List<Guid>();
			if (tree.RootNodes == null)
				return nodeIds;

			foreach (var node in tree.RootNodes)
				nodeIds.Add( node.RecordId );

			return nodeIds;
		}
	}
}
