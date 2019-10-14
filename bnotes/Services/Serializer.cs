using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Text;

namespace bnotes.Services
{
    public sealed class StringWriterWithEncoding : StringWriter
    {
        public override Encoding Encoding { get; }
    
        public StringWriterWithEncoding (Encoding encoding)
        {
            Encoding = encoding;
        }    
    }

    public class Serializer
    {       
        public T Deserialize<T>(string input) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public string Serialize<T>(T ObjectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

            using (StringWriterWithEncoding textWriter = new StringWriterWithEncoding(Encoding.UTF8))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");                    
                xmlSerializer.Serialize(textWriter, ObjectToSerialize, ns);

                return textWriter.ToString();
            }
        }

        public XElement ToXElement<T>(T ObjectToSerialize)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    var xmlSerializer = new XmlSerializer(typeof(T));
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    xmlSerializer.Serialize(streamWriter, ObjectToSerialize, ns);
                    return XElement.Parse(Encoding.UTF8.GetString(memoryStream.ToArray()));
                }
            }
        }

        public T FromXElement<T>(XElement xElement)  where T : class
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            return (T)xmlSerializer.Deserialize(xElement.CreateReader());
        }
    }
}