using AutoMapper;
using Carot.ERP.Database;

namespace Carot.ERP.Api.Models.AutoMapper.Profiles
{
	internal class EntityRelationOptionsProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<EntityRelationOptionsItem, DbEntityRelationOptions>();
			Mapper.CreateMap<DbEntityRelationOptions, EntityRelationOptionsItem>();
		}
	}
}
