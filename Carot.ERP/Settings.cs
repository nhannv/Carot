using Microsoft.Extensions.Configuration;

namespace Carot.ERP
{
    public static class Settings
    {
        public static string EncriptionKey { get; private set; }
		public static string ConnectionString { get; private set; }
		public static string CompanyName { get; private set; }
		public static string CompanyLogo { get; private set; }
		public static string Lang { get; private set; }

		public static void Initialize(IConfiguration configuration)
        {
            EncriptionKey = configuration["Settings:EncriptionKey"];
			ConnectionString = configuration["Settings:ConnectionString"];
			CompanyLogo = configuration["Settings:CompanyLogo"];
			CompanyName = configuration["Settings:CompanyName"];
			Lang = configuration["Settings:Lang"];
		}
    }
}
