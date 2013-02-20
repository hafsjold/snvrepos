using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicNP.FileViewControl;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

namespace bjArkiv
{
    [Serializable()]
    public class Column : IXmlSerializable
    {
        public string Name { get; set; }
        public Guid ColumnGuid { get; set; }
        public int ColumnPid { get; set; }
        public int Width { get; set; }
        public int ColumnDisplayIndex { get; set; }

        public XmlSchema GetSchema() { return null; }
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("ColumnGuid", ColumnGuid.ToString());
            writer.WriteAttributeString("ColumnPid", ColumnPid.ToString());
            writer.WriteAttributeString("Width", Width.ToString());
            writer.WriteAttributeString("ColumnDisplayIndex", ColumnDisplayIndex.ToString());
        }
        public void ReadXml(System.Xml.XmlReader reader)
        {
            reader.MoveToContent();
            Name = reader.GetAttribute("Name");
            ColumnGuid = Guid.Parse(reader.GetAttribute("ColumnGuid"));
            ColumnPid = int.Parse(reader.GetAttribute("ColumnPid"));
            Width = int.Parse(reader.GetAttribute("Width"));
            ColumnDisplayIndex = int.Parse(reader.GetAttribute("ColumnDisplayIndex"));
        }
    }

    [Serializable()]
    public class Columns : Dictionary<String, Column>, IXmlSerializable
    {
        public void SetColoumnWidthAndDisplayindex(FileView flView)
        {
            int i = 0;
            while (true)
            {
                string colunmName = flView.GetColumnName(string.Empty, i);
                int columnDisplayIndex = flView.GetColumnDisplayIndex(string.Empty, i);
                Guid columnGuid = Guid.Empty;
                int columnPid = 0;
                flView.GetColumndIDFromColumn(string.Empty, i, ref columnGuid, ref columnPid);

                if (colunmName != string.Empty)
                {
                    if (!this.ContainsKey(colunmName))
                    {
                        this[colunmName] = new Column { Name = colunmName, ColumnGuid = columnGuid, ColumnPid = columnPid, Width = 150, ColumnDisplayIndex = columnDisplayIndex };
                    }
                    flView.SetColumnWidth(string.Empty, i, this[colunmName].Width);
                    flView.SetColumnDisplayIndex(string.Empty, i, this[colunmName].ColumnDisplayIndex);
                }
                else
                    return;
                i++;
            }
        }

        public void ReadColoumnAttributes(FileView flView)
        {
            int i = 0;
            while (true)
            {
                string colunmName = flView.GetColumnName(string.Empty, i);
                int columnDisplayIndex = flView.GetColumnDisplayIndex(string.Empty, i);
                Guid columnGuid = Guid.Empty;
                int columnPid = 0;
                flView.GetColumndIDFromColumn(string.Empty, i, ref columnGuid, ref columnPid);

                if (colunmName != string.Empty)
                {
                    this[colunmName] = new Column { Name = colunmName, ColumnGuid = columnGuid, ColumnPid = columnPid, Width = flView.GetColumnWidth(string.Empty, i), ColumnDisplayIndex = columnDisplayIndex };
                }
                else
                    return;
                i++;
            }
        }

        public void AddCustomColumn(FileView flView)
        {
            foreach (string key in Program.customColumns.Keys)
                if (!this.ContainsKey(key))
                    this[key] = Program.customColumns[key];
            ReadColoumnAttributes(flView);
            foreach (string key in Program.customColumns.Keys)
                if (flView.GetColumnDisplayIndex(this[key].Name, -1) < 0)
                    flView.AddCustomColumn(this[key].Name, ColumnTextJustificationStyles.Left, this[key].Width);

        }

        public void DeleteCustomColumn(FileView flView)
        {
            foreach (string key in Program.customColumns.Keys)
                if (!this.ContainsKey(key))
                    this[key] = Program.customColumns[key];
            ReadColoumnAttributes(flView);
            foreach (string key in Program.customColumns.Keys)
            {
                flView.DeleteCustomColumn(this[key].Name);
            }
        }

        public bool Exists(Guid guid, int pid) 
        {
            foreach (Column col in this.Values)
            {
                if (col.ColumnGuid == guid && col.ColumnPid == pid)
                    return true;
            }
            return false;
        }

        public XmlSchema GetSchema() { return null; }
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            foreach (var key in this.Keys)
            {
                writer.WriteStartElement("Column");
                this[key].WriteXml(writer);
                writer.WriteEndElement();
            }
        }
        public void ReadXml(System.Xml.XmlReader reader)
        {
            int eventDepth = reader.Depth;
            reader.Read();
            while (reader.Depth > eventDepth)
            {
                if (reader.MoveToContent() == XmlNodeType.Element && reader.Name == "Column")
                {
                    Column col = new Column();
                    col.ReadXml(reader);
                    this[col.Name] = col;
                }
                reader.Read();
            }
        }
    }

}
