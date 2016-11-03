using AutoMapper;
using Carot.ERP.Database;

namespace Carot.ERP.Api.Models.AutoMapper.Profiles
{
	internal class RecordPermissionsProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<RecordPermissions, DbRecordPermissions>();
			Mapper.CreateMap<DbRecordPermissions, RecordPermissions>();
		}
	}
}
