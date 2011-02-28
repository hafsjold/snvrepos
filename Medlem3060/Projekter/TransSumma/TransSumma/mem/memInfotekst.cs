using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace nsPuls3060
{
    public class recMemInfotekst
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public string Msgtext { get; set; }
    }

    public class MemInfotekst : List<recMemInfotekst>
    {
        private string lf = "\n";
        private string crlf = "\r\n";

        public MemInfotekst()
        {
            open();
        }
        private void open()
        {
            clsRest objRest = new clsRest();
            string strxmldata = objRest.HttpGet2(clsRest.urlBaseType.data, "infotekst");
            XDocument xdoc = XDocument.Parse(strxmldata);
            var list = from infotekst in xdoc.Descendants("Infotekst") select infotekst;
            foreach (var infotekst in list)
            {
                recMemInfotekst rec = new recMemInfotekst();
                rec.Id = (int)clsPassXmlDoc.attr_val_int(infotekst, "Id");
                rec.Navn = clsPassXmlDoc.attr_val_string(infotekst, "Navn");
                string Msgtext = clsPassXmlDoc.attr_val_string(infotekst, "Msgtext");
                rec.Msgtext = Msgtext.Replace(lf, crlf);
                this.Add(rec);
            }
        }

        public void save()
        {
            string ModelName = "Infotekst";
            var qry = from r in Program.memInfotekst select r;
            foreach (var r in qry)
            {
                clsRest objRest = new clsRest();
                XElement xml = new XElement(ModelName);
                Type objectType = r.GetType();
                PropertyInfo[] properties = objectType.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    string Name = property.Name;
                    object Val = property.GetValue(r, null);
                    if (Name == "Msgtext") Val = ((string)Val).Replace(crlf, lf);
                    xml.Add(new XElement(Name, Val));
                }
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2(clsRest.urlBaseType.sync, "Convert", strxml);
                if (retur != "Status: 404") clsSQLite.insertStoreXML(Program.AppEngName, false, "sync/Convert", strxml, retur);
            }
        }
    }
}

