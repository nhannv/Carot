//using AutoMapper;
//using Carot.ERP.Database;
//using Carot.ERP;
//using Carot.ERP.Storage;

//namespace Carot.ERP.Database.AutoMapper.Profiles
//{
//	internal class DbFieldPermissionsProfile : Profile
//	{
//		IErpService service;

//		public DbFieldPermissionsProfile(IErpService service)
//		{
//			this.service = service;
//		}

//		protected override void Configure()
//		{
//			Mapper.CreateMap<FieldPermissions, DbFieldPermissions>();
//			Mapper.CreateMap<DbFieldPermissions, FieldPermissions>();
//			Mapper.CreateMap<DbFieldPermissions, IStorageFieldPermissions>().ConstructUsing(x => CreateEmptyFieldPermissionsObject(x));
//			Mapper.CreateMap<IStorageFieldPermissions, DbFieldPermissions>();
//		}

//		protected IStorageFieldPermissions CreateEmptyFieldPermissionsObject(DbFieldPermissions permissions)
//		{
//			var storageService = service.StorageService;
//			return storageService.GetObjectFactory().CreateEmptyFieldPermissionsObject();
//		}
//	}
//}
