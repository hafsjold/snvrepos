using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace nsPuls3060
{
    public class recMemKreditor
    {
        public int Id { get; set; }
        public string Datalevnr { get; set; }
        public string Datalevnavn { get; set; }
        public string Pbsnr { get; set; }
        public string Delsystem { get; set; }
        public string Regnr { get; set; }
        public string Kontonr { get; set; }
        public string Debgrpnr { get; set; }
        public string Sektionnr { get; set; }
        public string Transkodebetaling { get; set; }
    }

    public class MemKreditor : List<recMemKreditor>
    {
        public MemKreditor()
        {
            open();
        }
        private void open()
        {
            clsRest objRest = new clsRest();
            string strxmldata = objRest.HttpGet2(clsRest.urlBaseType.data, "kreditor");
            XDocument xdoc = XDocument.Parse(strxmldata);
            var list = from kreditor in xdoc.Descendants("Kreditor") select kreditor;
            foreach (var kreditor in list)
            {
                recMemKreditor rec = new recMemKreditor();
                rec.Id = (int)clsPassXmlDoc.attr_val_int(kreditor, "Id");
                rec.Datalevnr = clsPassXmlDoc.attr_val_string(kreditor, "Datalevnr");
                rec.Datalevnavn = clsPassXmlDoc.attr_val_string(kreditor, "Datalevnavn");
                rec.Pbsnr = clsPassXmlDoc.attr_val_string(kreditor, "Pbsnr");
                rec.Delsystem = clsPassXmlDoc.attr_val_string(kreditor, "Delsystem");
                rec.Regnr = clsPassXmlDoc.attr_val_string(kreditor, "Regnr");
                rec.Kontonr = clsPassXmlDoc.attr_val_string(kreditor, "Kontonr");
                rec.Debgrpnr = clsPassXmlDoc.attr_val_string(kreditor, "Debgrpnr");
                rec.Sektionnr = clsPassXmlDoc.attr_val_string(kreditor, "Sektionnr");
                rec.Transkodebetaling = clsPassXmlDoc.attr_val_string(kreditor, "Transkodebetaling");
                this.Add(rec);
            }
        }
        public void save()
        {
            string ModelName = "Kreditor";
            var qry = from r in Program.memKreditor select r;
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
                    xml.Add(new XElement(Name, Val));
                }
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2(clsRest.urlBaseType.sync, "Convert", strxml);
                if (retur != "Status: 404") clsSQLite.insertStoreXML(Program.AppEngName, false, "sync/Convert", strxml, retur);
            }
        }
    }
}

