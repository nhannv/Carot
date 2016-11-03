using System;

namespace Carot.ERP.Database
{
	public class DbException : Exception
	{
		public DbException(string message) : base(message)
		{
		}
	}
}
