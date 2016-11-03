using AutoMapper;
using Carot.ERP.Database;

namespace Carot.ERP.Api.Models.AutoMapper.Profiles
{
    internal class FieldPermissionsProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<FieldPermissions, DbFieldPermissions>();
			Mapper.CreateMap<DbFieldPermissions, FieldPermissions>();
		}
	}
}
