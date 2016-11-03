﻿using Npgsql;
using System;
using System.Data;
using System.IO;

namespace Carot.ERP.Database
{
	public class DbFile
	{
		public Guid Id { get; set; }
		public uint ObjectId { get; set; }
		public string FilePath { get; set; }
		public Guid? CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public Guid? LastModifiedBy { get; set; }
		public DateTime LastModificationDate { get; set; }

		internal DbFile(DataRow row)
		{
			Id = (Guid)row["id"];
			ObjectId = (uint)((decimal)row["object_id"]);
			FilePath = (string)row["filepath"];
			CreatedOn = (DateTime)row["created_on"];
			LastModificationDate = (DateTime)row["modified_on"];

			CreatedBy = null;
			if (row["created_by"] != DBNull.Value)
				CreatedBy = (Guid?)row["created_by"];

			LastModifiedBy = null;
			if (row["modified_by"] != DBNull.Value)
				LastModifiedBy = (Guid?)row["modified_by"];
		}

		private Stream GetContentStream(DbConnection connection)
		{
			var manager = new NpgsqlLargeObjectManager(connection.Connection);
			return manager.OpenReadWrite(ObjectId);
		}

		public byte[] GetBytes(DbConnection connection)
		{
			using (var contentStream = GetContentStream(connection))
			{
				return contentStream.Length == 0 ? null : new BinaryReader(contentStream).ReadBytes((int)contentStream.Length);
			}
		}

		public byte[] GetBytes()
		{
			using (DbConnection connection = DbContext.Current.CreateConnection())
			{
				try
				{
					connection.BeginTransaction();
					var result = GetBytes(connection);
					connection.CommitTransaction();
					return result;
				}
				catch
				{
					connection.RollbackTransaction();
					throw;
				}
			}
		}
	}
}
