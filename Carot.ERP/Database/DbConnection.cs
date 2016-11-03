using System;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Carot.ERP.Database
{
	public class DbConnection : IDisposable
	{
		private readonly Stack<string> _transactionStack = new Stack<string>();
		internal NpgsqlTransaction Transaction;
		internal NpgsqlConnection Connection;
		private bool _initialTransactionHolder = false;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="transaction"></param>
		internal DbConnection(NpgsqlTransaction transaction)
		{
			this.Transaction = transaction;
			Connection = transaction.Connection;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		internal DbConnection(string connectionString)
		{
			Transaction = null;
			Connection = new NpgsqlConnection(connectionString);
			Connection.Open();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="code"></param>
		/// <param name="commandType"></param>
		/// <returns></returns>
		public NpgsqlCommand CreateCommand(string code, CommandType commandType = CommandType.Text)
		{
			NpgsqlCommand command = null;
			command = Transaction != null ? new NpgsqlCommand(code, Connection, Transaction) : new NpgsqlCommand(code, Connection);

			command.CommandType = commandType;
			return command;
		}

		/// <summary>
		/// 
		/// </summary>
		public void BeginTransaction()
		{
			if (Transaction == null)
			{
				_initialTransactionHolder = true;
				Transaction = Connection.BeginTransaction();
				DbContext.Current.EnterTransactionalState(Transaction);
			}
			else
			{
				string savePointName = "tr_" + (Guid.NewGuid().ToString().Replace("-", ""));
				Transaction.Save(savePointName);
				_transactionStack.Push(savePointName);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void CommitTransaction()
		{
			if (Transaction == null)
				throw new Exception("Trying to commit non existent transaction.");

			if (_transactionStack.Any())
			{
				_transactionStack.Pop();
			}
			else
			{
				DbContext.Current.LeaveTransactionalState();
				if (!_initialTransactionHolder)
				{
					Transaction.Rollback();
					Transaction = null;
					throw new Exception("Trying to commit transaction started from another connection. The transaction is rolled back.");
				}
				Transaction.Commit();
				Transaction = null;

			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void RollbackTransaction()
		{
			if (Transaction == null)
				throw new Exception("Trying to rollback non existent transaction.");

			if (_transactionStack.Any())
			{
				var savepointName = _transactionStack.Pop();
				Transaction.Rollback(savepointName);
			}
			else
			{
				Transaction.Rollback();
				DbContext.Current.LeaveTransactionalState();
				Transaction = null;
				if (!_initialTransactionHolder)
					throw new Exception("Trying to rollback transaction started from another connection.The transaction is rolled back, but this exception is thrown to notify.");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void Close()
		{
			if (Transaction != null && _initialTransactionHolder)
			{
				Transaction.Rollback();
				throw new Exception("Trying to close connection with pending transaction. The transaction is rolled back.");
			}

			if ( _transactionStack.Count > 0)
				throw new Exception("Trying to close connection with pending transaction. The transaction is rolled back.");

			DbContext.Current.CloseConnection(this);
			if (Transaction == null)
				Connection.Close();
		}

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
				Close();
			}
		}

	}
}
