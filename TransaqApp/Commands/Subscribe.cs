using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TransaqApp
{
    public class Security
    {
        [XmlElement(ElementName = "board")] public string Board { get; set; }
        [XmlElement(ElementName = "seccode")] public string Seccode { get; set; }

    }

    [Serializable]
    [XmlRoot(ElementName = "command")]
    public class Subscribe
    {   
         private string IdCommand { get; set; } = "subscribe";
        
        [XmlArray(ElementName = "quotations")]
        [XmlArrayItem(ElementName = "security")] 
        public List<Security> Securitys { get; set; }
        
        [XmlAttribute(AttributeName = "id")]
        public string Id
        {
            get { return IdCommand; }
            set { IdCommand = value; }
        }

    }
}