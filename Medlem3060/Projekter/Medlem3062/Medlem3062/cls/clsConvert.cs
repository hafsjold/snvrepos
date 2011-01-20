using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Reflection;


namespace nsPuls3060
{
    class clsConvert
    {
        public void cvnMedlog()
        {
            string ModelName = "Medlog";
            var qry = from r in Program.dbData3060.TblMedlemLog select r;
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
                    //string NamePropertyType = property.GetValue(r, null).GetType().Name;
                    xml.Add(new XElement(Name, Val));
                    if (Name == "Id")
                    {
                        xml.Add(new XElement("Source_id", Val));
                        xml.Add(new XElement("Source", "Medlog"));        
                    }
                }
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Convert/" + ModelName, strxml);
            }
        }
        public void cvnPbsforsendelse()
        {
            string ModelName = "Pbsforsendelse";
            var qry = from r in Program.dbData3060.Tblpbsforsendelse select r;
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
                    //string NamePropertyType = property.GetValue(r, null).GetType().Name;
                    xml.Add(new XElement(Name, Val));
                }
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Convert/" + ModelName, strxml);
            }
        }

        public void cvnTilpbs()
        {
            string ModelName = "Tilpbs";
            var qry = from r in Program.dbData3060.Tbltilpbs select r;
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
                    //string NamePropertyType = property.GetValue(r, null).GetType().Name;
                    xml.Add(new XElement(Name, Val));
                }
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Convert/" + ModelName, strxml);
            }
        }

        public void cvnFak()
        {
            string ModelName = "Fak";
            var qry = from r in Program.dbData3060.Tblfak select r;
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
                    //string NamePropertyType = property.GetValue(r, null).GetType().Name;
                    xml.Add(new XElement(Name, Val));
                }
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Convert/" + ModelName, strxml);
            }
        }

        public void cvnRykker()
        {
            string ModelName = "Rykker";
            var qry = from r in Program.dbData3060.Tblrykker select r;
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
                    //string NamePropertyType = property.GetValue(r, null).GetType().Name;
                    xml.Add(new XElement(Name, Val));
                }
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Convert/" + ModelName, strxml);
            }
        }

        public void cvnOverforsel()
        {
            string ModelName = "Overforsel";
            var qry = from r in Program.dbData3060.Tbloverforsel select r;
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
                    //string NamePropertyType = property.GetValue(r, null).GetType().Name;
                    xml.Add(new XElement(Name, Val));
                }
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Convert/" + ModelName, strxml);
            }
        }

        public void cvnPbsfiles()
        {
            string ModelName = "Pbsfiles";
            var qry = from r in Program.dbData3060.Tblpbsfiles select r;
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
                    //string NamePropertyType = property.GetValue(r, null).GetType().Name;
                    xml.Add(new XElement(Name, Val));
                }
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Convert/" + ModelName, strxml);

                var qry2 = from r2 in Program.dbData3060.Tblpbsfile
                           where r2.Pbsfilesid == r.Id
                           orderby r2.Seqnr ascending
                           select r2;
                string TilPBSFile = "";
                int i = 0;
                foreach (var rec_pbsfile in qry2)
                {
                    if (i++ > 0) TilPBSFile += "\r\n";
                    TilPBSFile += rec_pbsfile.Data;
                }
                //char[] c_TilPBSFile = TilPBSFile.ToCharArray();
                //byte[] b_TilPBSFile = System.Text.Encoding.GetEncoding("windows-1252").GetBytes(c_TilPBSFile);
                XElement xml2 = new XElement("Pbsfile");
                xml2.Add(new XElement("Id", r.Id));
                xml2.Add(new XElement("Pbsfilesid", r.Id));
                xml2.Add(new XElement("Data", TilPBSFile));

                string strxml2 = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml2.ToString();
                string retur2 = objRest.HttpPost2("Convert/" + "Pbsfile", strxml2);
            }
        }

        public void cvnFrapbs()
        {
            string ModelName = "Frapbs";
            var qry = from r in Program.dbData3060.Tblfrapbs select r;
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
                    //string NamePropertyType = property.GetValue(r, null).GetType().Name;
                    xml.Add(new XElement(Name, Val));
                }
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Convert/" + ModelName, strxml);
            }
        }

        public void cvnBet()
        {
            string ModelName = "Bet";
            var qry = from r in Program.dbData3060.Tblbet select r;
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
                    //string NamePropertyType = property.GetValue(r, null).GetType().Name;
                    xml.Add(new XElement(Name, Val));
                }
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Convert/" + ModelName, strxml);
            }
        }

        public void cvnBetlin()
        {
            string ModelName = "Betlin";
            var qry = from r in Program.dbData3060.Tblbetlin select r;
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
                    //string NamePropertyType = property.GetValue(r, null).GetType().Name;
                    xml.Add(new XElement(Name, Val));
                }
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Convert/" + ModelName, strxml);
            }
        }

        public void cvnAftalelin()
        {
            string ModelName = "Aftalelin";
            var qry = from r in Program.dbData3060.Tblaftalelin select r;
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
                    //string NamePropertyType = property.GetValue(r, null).GetType().Name;
                    xml.Add(new XElement(Name, Val));
                }
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Convert/" + ModelName, strxml);
            }
        }

        public void cvnIndbetalingskort()
        {
            string ModelName = "Indbetalingskort";
            var qry = from r in Program.dbData3060.Tblindbetalingskort select r;
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
                    //string NamePropertyType = property.GetValue(r, null).GetType().Name;
                    xml.Add(new XElement(Name, Val));
                }
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Convert/" + ModelName, strxml);
            }
        }

        public void cvnSftp()
        {
            string ModelName = "Sftp";
            var qry = from r in Program.dbData3060.Tblsftp select r;
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
                    //string NamePropertyType = property.GetValue(r, null).GetType().Name;
                    xml.Add(new XElement(Name, Val));
                }
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Convert/" + ModelName, strxml);
            }
        }

        public void cvnInfotekst()
        {
            string ModelName = "Infotekst";
            var qry = from r in Program.dbData3060.Tblinfotekst select r;
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
                    //string NamePropertyType = property.GetValue(r, null).GetType().Name;
                    xml.Add(new XElement(Name, Val));
                }
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Convert/" + ModelName, strxml);
            }
        }

        public void cvnSysinfo()
        {
            string ModelName = "Sysinfo";
            var qry = from r in Program.dbData3060.TblSysinfo select r;
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
                    //string NamePropertyType = property.GetValue(r, null).GetType().Name;
                    xml.Add(new XElement(Name, Val));
                }
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Convert/" + ModelName, strxml);
            }
        }

        public void cvnKreditor()
        {
            string ModelName = "Kreditor";
            var qry = from r in Program.dbData3060.Tblkreditor select r;
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
                    //string NamePropertyType = property.GetValue(r, null).GetType().Name;
                    xml.Add(new XElement(Name, Val));
                }
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Convert/" + ModelName, strxml);
            }
        }

        public void cvnNrSerie()
        {
            string ModelName = "NrSerie";
            var qry = from r in Program.dbData3060.Tblnrserie select r;
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
                    //string NamePropertyType = property.GetValue(r, null).GetType().Name;
                    xml.Add(new XElement(Name, Val));
                }
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Convert/" + ModelName, strxml);
            }
        }

    }
}
