using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sketchpad
{
    class FileManager
    {
        public static Document read(String filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Document));
            Document document;

            FileStream fs = new FileStream(@filename, FileMode.Open);
            document = (Document)serializer.Deserialize(fs);
            fs.Close();
            return document;
        }

        public static void write(String filename, Document document)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Document));
            FileStream fs = new FileStream(@filename, FileMode.Create);
            serializer.Serialize(fs, document);
            fs.Close();
        }
    }
}
