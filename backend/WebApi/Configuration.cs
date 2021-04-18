namespace WebApi
{

    public class Configuration
    {
        public ConfigEnvironment Dev { get; set; }
        public ConfigEnvironment Prd { get; set; }
    }

    public class ConfigEnvironment
    {
        public string PrivateKey { get; set; }
        public string Salt { get; set; }
        public bool UseSsh { get; set; }
        public string SqlliteDbName { get; set; }
    }

}
