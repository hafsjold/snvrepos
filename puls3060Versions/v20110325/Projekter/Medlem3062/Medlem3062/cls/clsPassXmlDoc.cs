using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace nsPuls3060
{
    public class clsPassXmlDoc
    {

        static public int? attr_val_int(XElement doc, string attr_name) { return (int?)attr_val(doc, attr_name, "IntegerProperty"); }
        static public double? attr_val_double(XElement doc, string attr_name) { return (double?)attr_val(doc, attr_name, "FloatProperty"); }
        static public bool attr_val_bool(XElement doc, string attr_name) { return (bool)attr_val(doc, attr_name, "BooleanProperty"); }
        static public string attr_val_string(XElement doc, string attr_name) { return (string)attr_val(doc, attr_name, "TextProperty"); }
        static public DateTime? attr_val_date(XElement doc, string attr_name) { return (DateTime?)attr_val(doc, attr_name, "DateProperty"); }
        static public DateTime? attr_val_datetime(XElement doc, string attr_name) { return (DateTime?)attr_val(doc, attr_name, "DateTimeProperty"); }
 
        static public object attr_val(XElement doc, string attr_name, string attr_type)
        {
            string strval;
            try
            {
                strval = doc.Descendants(attr_name).First().Value.Trim();
            }
            catch (Exception)
            {
                strval = null;
            }

            if (attr_type == "IntegerProperty")
            {
                if (strval == "None") return (int?)null;
                try
                {
                    return int.Parse(strval);
                }
                catch (Exception)
                {
                    return (int?)null;
                }
            }
            else if (attr_type == "FloatProperty")
            {
                if (strval == "None") return (double?)null;
                try
                {
                    return double.Parse(strval);
                }
                catch (Exception)
                {
                    return (double?)null;
                }
            }
            else if (attr_type == "BooleanProperty")
            {
                if (strval == "None") return (bool?)null; 
                try
                {
                    string[] strTrue = { "yes", "true", "t", "1" };
                    return strTrue.Contains(strval.ToLower());
                }
                catch (Exception)
                {
                    return (bool?)null;
                }
            }
            else if (attr_type == "TextProperty")
            {
                if (strval == "None") return (string)null;
                try
                {
                    return strval;
                }
                catch (Exception)
                {
                    return (string)null;
                }
            }
            else if (attr_type == "DateProperty")
            {
                if (strval == "None") return (DateTime?)null;
                try
                {
                    return DateTime.Parse(strval);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else if (attr_type == "DateTimeProperty")
            {
                if (strval == "None") return (DateTime?)null;
                try
                {
                    return DateTime.Parse(strval);
                }
                catch (Exception)
                {
                    return (DateTime?)null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
