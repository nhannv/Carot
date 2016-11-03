﻿// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Carot.ERP.Web.Controllers
{
    public partial class ApiDevelopersController : ApiControllerBase
    {

        //EntityManager em;
        //EntityRelationManager rm;
        //RecordManager recMan;
        //IStorageFS fs;

        //public ApiDevelopersController(IErpService service) : base(service)
        //{
        //    em = new EntityManager(service.StorageService);
        //    rm = new EntityRelationManager(service.StorageService);
        //    recMan = new RecordManager(service);
        //    fs = service.StorageService.GetFS();
        //}
/*
        [AcceptVerbs(new[] { "POST" }, Route = "api/v1/en_US/upload/")]
        public IActionResult Upload([FromForm] IFormFile file)
        {
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"').ToLowerInvariant();
            
            var createdFile = fs.CreateTempFile( fileName, ReadFully(file.OpenReadStream()));
            return Json( new { Url = createdFile.FilePath });
        }


        private static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        */

        /*

                Guid userId = new Guid("f5588278-c0a1-4865-ac94-41dfa09bf8ac");
                Guid authorId = new Guid("031cca7c-1da4-48d3-a8e6-8e9315643b13");
                Guid cat1Id = new Guid("ca93490a-056d-4c61-9381-9f8283046066");
                Guid cat2Id = new Guid("01039a82-d507-4e80-b814-a9d74026bb6e");

                Guid postEntityId = new Guid("3fc00d85-7864-464c-8a8e-bfb85f7134e0");
                Guid authorEntityId = new Guid("59cc62ca-bf44-46f1-bdc8-daa4a538a567");
                Guid categoryEntityId = new Guid("8cf4c663-5029-4b18-b5df-108af707584c");

                Guid postCategoriesRelationId = new Guid("5f45fdde-b569-4b47-b667-63ff3c8bb423");
                Guid postAuthorRelationId = new Guid("2f7e77fd-a748-4cdd-8906-01bf8a46a664");


                [AcceptVerbs(new[] { "POST" }, Route = "api/v1/en_US/meta/developers/query/create-sample-query-data-structure")]
                public IActionResult CreateSampleQueryDataStructure()
                {
                    return Json("do nothing");

                    //delete entities and create new one
                    em.DeleteEntity(postEntityId);
                    em.DeleteEntity(categoryEntityId);
                    em.DeleteEntity(authorEntityId);
                    //drop all relations
                    rm.Delete(postCategoriesRelationId);
                    rm.Delete(postAuthorRelationId);




                    #region << Create Entities >>

                    //delete entities and create new one
                    em.DeleteEntity(postEntityId);
                    em.DeleteEntity(categoryEntityId);
                    em.DeleteEntity(authorEntityId);

                    InputEntity inputPostEntity = new InputEntity();
                    inputPostEntity.Id = postEntityId;
                    inputPostEntity.Name = "query_test_post";
                    inputPostEntity.Label = "Post";
                    inputPostEntity.LabelPlural = "Posts";
                    inputPostEntity.System = false;
                    inputPostEntity.RecordPermissions = new RecordPermissions();
                    inputPostEntity.RecordPermissions.CanCreate.Add(userId);
                    inputPostEntity.RecordPermissions.CanDelete.Add(userId);
                    inputPostEntity.RecordPermissions.CanRead.Add(userId);
                    inputPostEntity.RecordPermissions.CanUpdate.Add(userId);
                    Entity postEntity = null;
                    {
                        var result = em.CreateEntity(inputPostEntity);
                        if (!result.Success)
                            return DoResponse(result);

                        postEntity = result.Object;
                    }

                    InputEntity inputAuthorEntity = new InputEntity();
                    inputAuthorEntity.Id = authorEntityId;
                    inputAuthorEntity.Name = "query_test_author";
                    inputAuthorEntity.Label = "Author";
                    inputAuthorEntity.LabelPlural = "Authors";
                    inputAuthorEntity.System = false;
                    inputAuthorEntity.RecordPermissions = new RecordPermissions();
                    inputAuthorEntity.RecordPermissions.CanCreate.Add(userId);
                    inputAuthorEntity.RecordPermissions.CanDelete.Add(userId);
                    inputAuthorEntity.RecordPermissions.CanRead.Add(userId);
                    inputAuthorEntity.RecordPermissions.CanUpdate.Add(userId);
                    Entity authorEntity = null;
                    {
                        var result = em.CreateEntity(inputAuthorEntity);
                        if (!result.Success)
                            return DoResponse(result);

                        authorEntity = result.Object;
                    }

                    InputEntity inputCategoryEntity = new InputEntity();
                    inputCategoryEntity.Id = categoryEntityId;
                    inputCategoryEntity.Name = "query_test_category";
                    inputCategoryEntity.Label = "Category";
                    inputCategoryEntity.LabelPlural = "Categories";
                    inputCategoryEntity.System = false;
                    inputCategoryEntity.RecordPermissions = new RecordPermissions();
                    inputCategoryEntity.RecordPermissions.CanCreate.Add(userId);
                    inputCategoryEntity.RecordPermissions.CanDelete.Add(userId);
                    inputCategoryEntity.RecordPermissions.CanRead.Add(userId);
                    inputCategoryEntity.RecordPermissions.CanUpdate.Add(userId);
                    Entity categoryEntity = null;
                    {
                        var result = em.CreateEntity(inputCategoryEntity);
                        if (!result.Success)
                            return DoResponse(result);

                        categoryEntity = result.Object;
                    }

                    #endregion

                    #region << Create Author entity fields >>

                    InputTextField authorName = new InputTextField();
                    authorName.Id = Guid.NewGuid();
                    authorName.Name = "name";
                    authorName.Label = "Name";
                    authorName.Required = true;
                    authorName.Unique = true;
                    authorName.DefaultValue = string.Empty;
                    authorName.System = false;
                    {
                        var result = em.CreateField(authorEntity.Id, authorName);
                        if (!result.Success)
                            return DoResponse(result);
                    }

                    #endregion

                    #region << Create Post entity fields >>

                    InputTextField postTitle = new InputTextField();
                    postTitle.Id = Guid.NewGuid();
                    postTitle.Name = "title";
                    postTitle.Label = "Title";
                    postTitle.Required = true;
                    postTitle.Unique = false;
                    postTitle.DefaultValue = string.Empty;
                    postTitle.System = false;
                    {
                        var result = em.CreateField(postEntity.Id, postTitle);
                        if (!result.Success)
                            return DoResponse(result);
                    }

                    InputTextField postContent = new InputTextField();
                    postContent.Id = Guid.NewGuid();
                    postContent.Name = "content";
                    postContent.Label = "Content";
                    postContent.Required = false;
                    postContent.Unique = false;
                    postContent.DefaultValue = string.Empty;
                    postContent.System = false;
                    {
                        var result = em.CreateField(postEntity.Id, postContent);
                        if (!result.Success)
                            return DoResponse(result);
                    }

                    InputGuidField postAuthor = new InputGuidField();
                    postAuthor.Id = Guid.NewGuid();
                    postAuthor.Name = "author";
                    postAuthor.Label = "Author";
                    postAuthor.Required = true;
                    postAuthor.Unique = false;
                    postAuthor.DefaultValue = null;
                    postAuthor.GenerateNewId = true;
                    postAuthor.System = false;
                    {
                        var result = em.CreateField(postEntity.Id, postAuthor);
                        if (!result.Success)
                            return DoResponse(result);
                    }

                    #endregion

                    #region << Create Category entity fields >>

                    InputTextField categoryName = new InputTextField();
                    categoryName.Id = Guid.NewGuid();
                    categoryName.Name = "name";
                    categoryName.Label = "Name";
                    categoryName.Required = true;
                    categoryName.Unique = true;
                    categoryName.DefaultValue = string.Empty;
                    categoryName.System = false;
                    {
                        var result = em.CreateField(categoryEntity.Id, categoryName);
                        if (!result.Success)
                            return DoResponse(result);
                    }

                    #endregion

                    #region << Create relations >>

                    //reload entities with all fields
                    postEntity = em.ReadEntity(postEntityId).Object;
                    categoryEntity = em.ReadEntity(categoryEntityId).Object;
                    authorEntity = em.ReadEntity(authorEntityId).Object;



                    EntityRelation postCategoriesRelation = new EntityRelation();
                    postCategoriesRelation.Id = postCategoriesRelationId;
                    postCategoriesRelation.Name = "query_test_post_categories";
                    postCategoriesRelation.Label = "Post categories";
                    postCategoriesRelation.RelationType = EntityRelationType.ManyToMany;
                    postCategoriesRelation.TargetEntityId = postEntity.Id;
                    postCategoriesRelation.TargetFieldId = postEntity.Fields.Single(x => x.Name == "id").Id;
                    postCategoriesRelation.OriginEntityId = categoryEntity.Id;
                    postCategoriesRelation.OriginFieldId = categoryEntity.Fields.Single(x => x.Name == "id").Id;
                    {
                        var result = rm.Create(postCategoriesRelation);
                        if (!result.Success)
                            return DoResponse(result);
                    }


                    EntityRelation postAuthorRelation = new EntityRelation();
                    postAuthorRelation.Id = postAuthorRelationId;
                    postAuthorRelation.Name = "query_test_post_author";
                    postAuthorRelation.Label = "Post author";
                    postAuthorRelation.RelationType = EntityRelationType.OneToMany;
                    postAuthorRelation.OriginEntityId = authorEntity.Id;
                    postAuthorRelation.OriginFieldId = authorEntity.Fields.Single(x => x.Name == "id").Id;
                    postAuthorRelation.TargetEntityId = postEntity.Id;
                    postAuthorRelation.TargetFieldId = postAuthor.Id.Value;
                    {
                        var result = rm.Create(postAuthorRelation);
                        if (!result.Success)
                            return DoResponse(result);
                    }

                    #endregion

                    #region << Create records >>

                    EntityRecord author = new EntityRecord();
                    author["id"] = authorId;
                    author["name"] = "Test author name";
                    author["created_by"] = userId;
                    author["last_modified_by"] = userId;
                    author["created_on"] = DateTime.UtcNow;
                    {
                        QueryResponse result = recMan.CreateRecord("query_test_author", author);
                        if (!result.Success)
                            return DoResponse(result);
                    }

                    Guid[] postIds = new Guid[10];
                    for (int i = 0; i < 10; i++)
                    {

                        EntityRecord post = new EntityRecord();
                        post["id"] = Guid.NewGuid();
                        post["title"] = string.Format("post {0} title", i);
                        post["content"] = string.Format("post {0} content", i); ;
                        post["author"] = authorId;
                        post["created_by"] = userId;
                        post["last_modified_by"] = userId;
                        post["created_on"] = DateTime.UtcNow;
                        {
                            QueryResponse result = recMan.CreateRecord("query_test_post", post);
                            if (!result.Success)
                                return DoResponse(result);
                        }

                        postIds[i] = (Guid)post["id"];
                    }



                    EntityRecord cat1 = new EntityRecord();
                    cat1["id"] = cat1Id;
                    cat1["name"] = "category one";
                    cat1["created_by"] = userId;
                    cat1["last_modified_by"] = userId;
                    cat1["created_on"] = DateTime.UtcNow;
                    {
                        QueryResponse result = recMan.CreateRecord("query_test_category", cat1);
                        if (!result.Success)
                            return DoResponse(result);
                    }

                    EntityRecord cat2 = new EntityRecord();
                    cat2["id"] = cat2Id;
                    cat2["name"] = "category two";
                    cat2["created_by"] = userId;
                    cat2["last_modified_by"] = userId;
                    cat2["created_on"] = DateTime.UtcNow;
                    {
                        QueryResponse result = recMan.CreateRecord("query_test_category", cat2);
                        if (!result.Success)
                            return DoResponse(result);
                    }

                    recMan.CreateRelationManyToManyRecord(postCategoriesRelation.Id, cat1Id, postIds[0]);
                    recMan.CreateRelationManyToManyRecord(postCategoriesRelation.Id, cat2Id, postIds[0]);

                    //recMan.CreateRelationManyToManyRecord(postCategoriesRelation.Id, cat1Id, postIds[1]);
                    recMan.CreateRelationManyToManyRecord(postCategoriesRelation.Id, cat2Id, postIds[1]);

                    #endregion

                    return Json("Structure and data created successfully");
                }


                [AcceptVerbs(new[] { "POST" }, Route = "api/v1/en_US/meta/developers/query/execute-sample-query")]
                public IActionResult ExecuteSampleQuery()
                {
                    fs.Delete("file.tmp");
                    fs.Delete("file1.tmp");

                    int fileSize = 100;
                    byte[] buffer = new byte[fileSize];
                    for (int i = 0; i < fileSize; i++)
                        buffer[i] = (byte)(i % 256);

                    var file = fs.CreateTempFile(buffer, "tmp");
                    var bytes  = file.GetBytes();

                    fs.Delete(file.FilePath);

                    List<Guid> roles = new List<Guid>();
                    for (int i = 0; i < 5; i++)
                        roles.Add(Guid.NewGuid());

                    file = fs.Create("/file.tmp", buffer, DateTime.UtcNow, Guid.NewGuid(), roles);

                    file = fs.Copy("file.tmp", "file1.tmp");
                    file = fs.Copy("file1.tmp", "file.tmp", true );

                    fs.Delete("file.tmp");
                    fs.Delete("file1.tmp");

                    return Json(file.FilePath);
                } */

    }
}