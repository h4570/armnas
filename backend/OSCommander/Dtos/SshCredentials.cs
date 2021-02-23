namespace OSCommander.Dtos
{
    public class SshCredentials
    {

        public SshCredentials(string host, string username, string password)
        {
            Host = host;
            Username = username;
            Password = password;
        }

        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
