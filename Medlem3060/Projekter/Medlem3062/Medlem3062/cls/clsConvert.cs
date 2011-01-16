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
        public void cvnFak()
        {
            string Model = "Fak";
            var qry = from r in Program.dbData3060.Tblfak select r;
            foreach (var r in qry)
            {
                clsRest objRest = new clsRest();
                XElement xml = new XElement(Model);
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
                string retur = objRest.HttpPost2(Model, strxml);
            }
        }
    }
}
