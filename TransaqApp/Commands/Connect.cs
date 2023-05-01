using System;
using System.Xml.Serialization;

namespace TransaqApp
{
    [Serializable]
    [XmlRoot("command")]
    public class Connect
    {
        private string IdCommand { get; set; } = "connect";
        public string? login { get; set; }
        public string? password { get; set; }
        public string? host { get; set; }
        public string? port { get; set; }
        public string language { get; set; } = "ru";
        public string autopos { get; set; } = "false";
        public string milliseconds { get; set; } = "true";
        public string utc_time { get; set; } = "true";

        [XmlAttribute("id")]
        public string Id
        {
            get { return IdCommand; }
            set { IdCommand = value; }
        }
        
        public Connect() { }
        public Connect(string login, string password, string host, string port)
        {
            this.login = login;
            this.password = password;
            this.host = host;
            this.port = port;
        }
    }
}

