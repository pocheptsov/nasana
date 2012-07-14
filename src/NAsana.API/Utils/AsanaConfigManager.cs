namespace NAsana.API.v1.Utils
{
    using System.Configuration;

    public class AsanaConfigManager
    {
        public AsanaConfig GetConfig()
        {
            var appSettings = ConfigurationManager.AppSettings;
            return new AsanaConfig
                       {
                           ApiKey = appSettings["asana.api_key"],
                       };
        }
    }
}