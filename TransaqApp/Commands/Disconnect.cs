using System;
using System.Xml.Serialization;

namespace TransaqApp
{   
    [Serializable]
    [XmlRoot("command")]
    public class Disconnect
    {
        private string IdCommand { get; set; } = "disconnect";
        
        [XmlAttribute("id")]
        public string Id
        {
            get { return IdCommand; }
            set { IdCommand = value; }
        }
        
    }
}