namespace WebApi
{

    public class Configuration
    {
        public ConfigEnvironment Dev { get; set; }
        public ConfigEnvironment Prd { get; set; }
    }

    public class SshCredentials
    {
        public string Username { get; set; }
        public string Host { get; set; }
    }

    public class ConfigEnvironment
    {
        public SshCredentials Ssh { get; set; }
        public string SqlliteDbName { get; set; }
        public Urls Urls { get; set; }
        public Paths Paths { get; set; }
    }

    public class Urls
    {
        public string Main { get; set; }
    }

    public class Paths
    {
        public string Path1 { get; set; }
    }

}
