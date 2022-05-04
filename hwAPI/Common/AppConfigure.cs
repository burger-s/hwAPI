using System.Net.NetworkInformation;
using Microsoft.Extensions.Configuration;
namespace hwAPI.Common
{
    public static class AppConfigure
    {
        private static string _connecttionString;
        private static string _db;
        private static string _CollectionName;
        public static void setConfig(IConfiguration configuration)
        {
            _connecttionString = configuration["MongoDB:ConnectionString"];
            _db= configuration["MongoDB:DatabaseName"];
            _CollectionName = configuration["MongoDB:CollectionName"];
        }
        internal static string getConnectionString() => _connecttionString;
        internal static string getdbString() => _db;
        internal static string getCollectionNameString() => _CollectionName;
    }
}
