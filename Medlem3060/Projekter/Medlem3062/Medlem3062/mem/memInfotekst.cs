using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

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
        public MemInfotekst()
        {
            open();
        }
        private void open() 
        {
        }
        
        public void save() { }   }
}

