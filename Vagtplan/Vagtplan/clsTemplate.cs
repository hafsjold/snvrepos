using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;


namespace nsVagtplan
{
    public class recTemplate
    {
        public int Dag { get; set; }
        public bool Fri { get; set; }
        public TimeSpan? Start { get; set; }
        public TimeSpan? Slut { get; set; }
    }

    public class clsTemplate : List<recTemplate>
    {
        public string Tekst;
        private DateTime StartDato;
        private int RulDage;

        public clsTemplate() 
        {
            load();
        }
        
        private void load() 
        {
            XDocument xmlDoc = XDocument.Load("template.xml");
            XElement Vagtplan = xmlDoc.Descendants("Vagtplan").First();
            Tekst = Vagtplan.Attribute("Tekst").Value;
            StartDato = DateTime.Parse(Vagtplan.Attribute("StartDato").Value);
            RulDage = int.Parse(Vagtplan.Attribute("RulDage").Value);
            recTemplate rec;

            var qry = from vagt in Vagtplan.Descendants("Vagt") select vagt;
            foreach (var v in qry)
            {

                int Dag = int.Parse(v.Attribute("Dag").Value);
                DateTime Dato = StartDato.AddDays(Dag - 1);
                bool Fri = false;
                if (v.Attribute("Fri") != null)
                    if (v.Attribute("Fri").Value.ToUpper() == "JA")
                        Fri = true;
                rec = new recTemplate 
                {
                     Dag = Dag,
                     Fri = Fri,
                };

                if (!Fri)
                {
                    rec.Start = TimeSpan.Parse(v.Attribute("Start").Value);
                    rec.Slut = TimeSpan.Parse(v.Attribute("Slut").Value);
                }
                this.Add(rec);
            }
        }

        public recTemplate getDag(DateTime pDato) 
        {
            int dage = pDato.Date.Subtract(StartDato.Date).Days;
            int perioder = dage / RulDage;
            if (dage < 0) perioder--;
            DateTime NyStartDato = StartDato.AddDays(RulDage * perioder);
            int nydage = pDato.Date.Subtract(NyStartDato.Date).Days + 1;
            recTemplate rec = (from r in this where r.Dag == nydage select r).First();
            return rec;
        }
    }
}
