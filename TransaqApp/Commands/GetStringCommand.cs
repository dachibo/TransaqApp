using System.IO;
using System.Xml.Serialization;


namespace TransaqApp
{
    public class ObjectToXmlGeneric<T> where T : class
    {
        public static string Serialize(T obj)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StringWriter sw = new StringWriter())
            {
                xs.Serialize(sw, obj);
                return sw.ToString();
                
            }
        }
    }
}