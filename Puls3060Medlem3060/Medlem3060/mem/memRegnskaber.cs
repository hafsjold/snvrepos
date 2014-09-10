using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsPuls3060
{
    public class recRegnskaber
    {
        public int rid   { get; set; }
	    public string Navn   { get; set; }
	    public DateTime? Oprettet  { get; set; }
	    public DateTime? Start  { get; set; }
	    public DateTime? Slut   { get; set; }
	    public DateTime? DatoLaas   { get; set; }
	    public string Firmanavn   { get; set; }
        public string Placering { get; set; }
        public string Eksportmappe { get; set; } 
	    public string TilPBS   { get; set; }
	    public string FraPBS    { get; set; }
        public bool? Afsluttet { get; set; }
    }

    public class MemRegnskaber : List<recRegnskaber>
    {

    }
}
