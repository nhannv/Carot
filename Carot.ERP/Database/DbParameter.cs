using NpgsqlTypes;

namespace Carot.ERP.Database.Models
{
    public class DbParameter
    {
		public string Name { get; set; }

		public object Value { get; set; }

		public NpgsqlDbType Type { get; set; }
	}
}
