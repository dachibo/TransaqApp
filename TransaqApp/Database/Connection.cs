namespace TransaqApp.Database
{
    public class Connection
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string host { get; set; }
        public string port { get; set; }

        public Connection()
        {
        }

        public Connection(string login, string password, string host, string port)
        {
            this.login = login;
            this.password = password;
            this.host = host;
            this.port = port;
        }
    }
}