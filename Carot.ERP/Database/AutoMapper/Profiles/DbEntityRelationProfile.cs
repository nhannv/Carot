//using AutoMapper;
//using Carot.ERP.Database;
//using Carot.ERP;
//using Carot.ERP.Api.Models;
//using Carot.ERP.Storage;

//namespace Carot.ERP.Database.AutoMapper.Profiles
//{
//	internal class DbEntityRelationProfile : Profile
//    {
//		IErpService service;

//		public DbEntityRelationProfile(IErpService service)
//		{
//			this.service = service;
//		}

//		protected override void Configure()
//        {
//			Mapper.CreateMap<EntityRelation, DbEntityRelation>();
//            Mapper.CreateMap<DbEntityRelation, EntityRelation>();
//			Mapper.CreateMap<DbEntityRelation, IStorageEntityRelation>().ConstructUsing(x => CreateEmptyEntityRelationObject(x));
//			Mapper.CreateMap<IStorageEntityRelation, DbEntityRelation>();
//		}

//		protected IStorageEntityRelation CreateEmptyEntityRelationObject(DbEntityRelation relation)
//		{
//			var storageService = service.StorageService;
//			return storageService.GetObjectFactory().CreateEmptyEntityRelationObject();
//		}
//	}
//}