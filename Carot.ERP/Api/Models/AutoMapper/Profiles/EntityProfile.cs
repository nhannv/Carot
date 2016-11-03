﻿using AutoMapper;
using System;
using Carot.ERP.Database;

namespace Carot.ERP.Api.Models.AutoMapper.Profiles
{
	internal class EntityProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<Entity, InputEntity>();
			Mapper.CreateMap<InputEntity, Entity>()
				.ForMember(x => x.Id, opt => opt.MapFrom(y => (y.Id.HasValue) ? y.Id.Value : Guid.Empty))
				.ForMember(x => x.System, opt => opt.MapFrom(y => (y.System.HasValue) ? y.System.Value : false))
				.ForMember(x => x.Weight, opt => opt.MapFrom(y => (y.Weight.HasValue) ? y.Weight.Value : 1));

			Mapper.CreateMap<Entity, DbEntity>();
			Mapper.CreateMap<DbEntity, Entity>();
		}
	}
}
