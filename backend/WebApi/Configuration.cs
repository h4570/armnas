namespace WebApi
{

    public class Configuration
    {
        public ConfigEnvironment Dev { get; set; }
        public ConfigEnvironment Prd { get; set; }
    }

    public class ConfigEnvironment
    {
        public bool UseSsh { get; set; }
        public string SqlliteDbName { get; set; }
    }

}
