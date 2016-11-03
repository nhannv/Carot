﻿using System;
using System.Collections.Generic;
using System.Linq;
using Carot.ERP.Api.Models;
using Carot.ERP.Api.Models.AutoMapper;
using Carot.ERP.Database;

namespace Carot.ERP.Api
{
    public class SecurityManager
    {
        const string fieldsToQuery = @"id,username,email,password,first_name,last_name,image,created_on,created_by,last_modified_on,last_modified_by, last_logged_in,enabled, $user_role.id, $user_role.name";

        public SecurityManager()
        {
        }

        public ErpUser GetUser(Guid userId)
        {
            RecordManager recMan = new RecordManager(true);
            EntityQuery query = new EntityQuery("user", fieldsToQuery, EntityQuery.QueryEQ("id", userId), null, null, null);
            var result = recMan.Find(query);
            if (!result.Success)
                throw new Exception(result.Message);

            if (!result.Object.Data.Any())
                return null;

            var record = result.Object.Data.Single();
            return record.DynamicMapTo<ErpUser>();

        }

        public ErpUser GetUser(string email)
        {
            RecordManager recMan = new RecordManager(true);
            EntityQuery query = new EntityQuery("user", fieldsToQuery, EntityQuery.QueryEQ("email", email), null, null, null);
            var result = recMan.Find(query);
            if (!result.Success)
                throw new Exception(result.Message);

            if (!result.Object.Data.Any())
                return null;

            var record = result.Object.Data.Single();
            return record.DynamicMapTo<ErpUser>();
        }

        public ErpUser GetUser(string email, string password)
        {
            var query = EntityQuery.QueryAND(EntityQuery.QueryEQ("email", email), EntityQuery.QueryEQ("password", password));
            var result = new RecordManager(true).Find(new EntityQuery("user", fieldsToQuery, query));

            if (!result.Success)
                throw new Exception(result.Message);

            ErpUser user = null;
            if (result.Object.Data != null && result.Object.Data.Any())
                user = result.Object.Data[0].DynamicMapTo<ErpUser>();

            return user;
        }

        public void UpdateUserLastLoginTime(Guid userId)
        {
            List<KeyValuePair<string, object>> storageRecordData = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("id", userId),
                new KeyValuePair<string, object>("last_logged_in", DateTime.UtcNow)
            };
            DbContext.Current.RecordRepository.Update("user", storageRecordData);
        }
    }
}
