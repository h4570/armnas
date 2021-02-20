using Newtonsoft.Json;
using System.IO;
using WebApi;

namespace WebApiTests
{

    public class ConfigurationWrapper { public Configuration Configuration { get; set; } }

    public static class Utility
    {

        public static ConfigEnvironment GetConfig()
        {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent;
            if (directoryInfo?.Parent == null) return null;
            var path = directoryInfo?.Parent.FullName + "\\appsettings.json";
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<ConfigurationWrapper>(json).Configuration.Dev;
        }

    }
}
