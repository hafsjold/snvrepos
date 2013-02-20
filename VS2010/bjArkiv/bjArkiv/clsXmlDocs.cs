using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using System.IO;

namespace bjArkiv
{    
    [Serializable()]
    public class xmldocs : List<xmldoc>, IXmlSerializable
    {
        public string path { get; set; }
 
        ~xmldocs() 
        {
            Save();
        }

        public static xmldocs Load(string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(xmldocs));
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    try
                    {
                        fs.Position = 0;
                        xmldocs Db =  (xmldocs)xs.Deserialize(fs);
                        Db.path = path;
                        return Db;
                    }
                    catch { }
                }
              }
            catch { }
            return null;
        }
        
        internal void Save()
        {
            XmlSerializer xs = new XmlSerializer(this.GetType());
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    try
                    {
                        xs.Serialize(fs, this);
                    }
                    catch { }
                }
            }
            catch { }
        }

        public XmlSchema GetSchema() { return null; }
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            foreach (xmldoc doc in this)
            {
                writer.WriteStartElement("xmldoc");
                doc.WriteXml(writer);
                writer.WriteEndElement();
            }
        }
        public void ReadXml(System.Xml.XmlReader reader)
        {
            int eventDepth = reader.Depth;
            reader.Read();
            while (reader.Depth > eventDepth)
            {
                if (reader.MoveToContent() == XmlNodeType.Element && reader.Name == "xmldoc")
                {
                    xmldoc doc = new xmldoc();
                    doc.ReadXml(reader);
                    this.Add(doc);
                }
                reader.Read();
            }
        }


    }

    [Serializable()]
    public class xmldoc : IXmlSerializable
    {
        public Guid id { get; set; }
        public int ref_nr { get; set; }
        public string virksomhed { get; set; }
        public string emne { get; set; }
        public string dokument_type { get; set; }
        public int år { get; set; }
        public string ekstern_kilde { get; set; }
        public string beskrivelse { get; set; }
        public string oprettes_af { get; set; }
        public DateTime oprettet_dato { get; set; }
        public string kilde_sti { get; set; }

        public XmlSchema GetSchema() { return null; }
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("id", id.ToString());
            writer.WriteAttributeString("ref_nr", ref_nr.ToString());
            writer.WriteAttributeString("virksomhed", virksomhed);
            writer.WriteAttributeString("emne", emne);
            writer.WriteAttributeString("dokument_type", dokument_type);
            writer.WriteAttributeString("år", år.ToString());
            writer.WriteAttributeString("ekstern_kilde", ekstern_kilde);
            writer.WriteAttributeString("beskrivelse", beskrivelse);
            writer.WriteAttributeString("oprettes_af", oprettes_af);
            writer.WriteAttributeString("oprettet_dato", oprettet_dato.ToString());
            writer.WriteAttributeString("kilde_sti", kilde_sti);
        }
        public void ReadXml(System.Xml.XmlReader reader)
        {
            reader.MoveToContent();
            id = Guid.Parse(reader.GetAttribute("id"));
            ref_nr = int.Parse(reader.GetAttribute("ref_nr"));
            virksomhed = reader.GetAttribute("virksomhed");
            emne = reader.GetAttribute("emne");
            dokument_type = reader.GetAttribute("dokument_type");
            år = int.Parse(reader.GetAttribute("år"));
            ekstern_kilde = reader.GetAttribute("ekstern_kilde");
            beskrivelse = reader.GetAttribute("beskrivelse");
            oprettes_af = reader.GetAttribute("oprettes_af");
            oprettet_dato = DateTime.Parse(reader.GetAttribute("oprettet_dato"));
            kilde_sti = reader.GetAttribute("kilde_sti");
        }
    }


}
