using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Carot.ERP.Database
{
	public class DbContext : IDisposable
	{
		public static DbContext Current => _current.Value;
	    private static AsyncLocal<DbContext> _current = new AsyncLocal<DbContext>();
		private static string _connectionString;

		public DbRecordRepository RecordRepository { get; private set; }
		public DbEntityRepository EntityRepository { get; private set; }
		public DbRelationRepository RelationRepository { get; private set; }
		public DbSystemSettingsRepository SettingsRepository { get; private set; }

		private readonly Stack<DbConnection> _connectionStack;
		private NpgsqlTransaction _transaction;

		#region <--- Context and Connection --->

		private DbContext()
		{
			_connectionStack = new Stack<DbConnection>();
			RecordRepository = new DbRecordRepository();
			EntityRepository = new DbEntityRepository();
			RelationRepository = new DbRelationRepository();
			SettingsRepository = new DbSystemSettingsRepository();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public DbConnection CreateConnection()
		{
			DbConnection con = null;
			if (_transaction != null)
				con = new DbConnection(_transaction);
			else
				con = new DbConnection(_connectionString);

			_connectionStack.Push(con);

			return con;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="conn"></param>
		public bool CloseConnection(DbConnection conn)
		{
			var dbConn = _connectionStack.Peek();
			if (dbConn != conn)
				throw new DbException("You are trying to close connection, before closing inner connections.");

			_connectionStack.Pop();
			return _connectionStack.Count == 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="transaction"></param>
		internal void EnterTransactionalState(NpgsqlTransaction transaction)
		{
			this._transaction = transaction;
		}

		/// <summary>
		/// 
		/// </summary>
		internal void LeaveTransactionalState()
		{
			this._transaction = null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="connString"></param>
		public static DbContext CreateContext(string connString)
		{
			_connectionString = connString;

			if (_current.Value == null)
				_current.Value = new DbContext();

			return _current.Value;
		}


		public static void CloseContext()
		{
			if (_current.Value != null)
			{
				if (_current.Value._transaction != null)
				{
					_current.Value._transaction.Rollback();
					throw new DbException("Trying to release database context in transactional state. There is open transaction in created connections.");
				}

				if (_current.Value._connectionStack.Count > 0)
					throw new DbException("Trying to release database context with already opened connection. Close connection before");

			}

			_current.Value = null;
		}


		#endregion


		#region <--- Dispose --->

		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="disposing"></param>
		public void Dispose(bool disposing)
		{
			if (disposing)
			{
				CloseContext();
			}
		}
		#endregion
	}
}
