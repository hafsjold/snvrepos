using System;
using System.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using Microsoft.SharePoint;
using System.IO;
using System.Xml.Serialization;
using pvMetadata;

namespace GenerateTypes
{
    partial class genTypeMain
    {
        static Metadata model;
        
        static void Main(string[] args)
        {
            model = new Metadata();
            GenerateTypes();
        }

 
    }
}
