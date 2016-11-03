using AutoMapper;
using Carot.ERP.Database;

namespace Carot.ERP.Api.Models.AutoMapper.Profiles
{
	internal class EntityRelationProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<EntityRelation, DbEntityRelation>();
			Mapper.CreateMap<DbEntityRelation, EntityRelation>();
		}
	}
}