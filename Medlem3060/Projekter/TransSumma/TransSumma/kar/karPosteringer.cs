using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace nsPuls3060
{

    public struct posttype
    {
        public int int0;
        public int dato;
        public int bilag;
        public int int12;
        public int int16;
        public int int20;
        public int int24;
        public int int28;
        public int int32;
        public int int36;
        public int int40;
        public int int44;
        public int int48;
        public int int52;
        public int konto;
        public int int60;
        public int belob;
        public int int68;
        public int int72;
        public int int76;
        public int Nr;
        public int Id;
        public int int88;
        public int int92;
    }


    public class recPosteringer
    {
        private int m_rec_no;
        private posttype m_rec_data;
        private string m_rec_txt;
        private int? m_regnskabid;

        public int rec_no
        {
            get
            {
                return m_rec_no;
            }
            set
            {
                m_rec_no = value;
            }
        }

        public posttype rec_data
        {
            get
            {
                return m_rec_data;
            }
            set
            {
                m_rec_data = value;
            }
        }

        public string Tekst
        {
            get
            {
                return m_rec_txt;
            }
            set
            {
                m_rec_txt = value;
            }
        }

        public DateTime Dato
        {
            get
            {
                return clsUtil.SummaSerial2DateTime(m_rec_data.dato);
            }
            set
            {
                m_rec_data.dato = clsUtil.SummaDateTime2Serial(value);
            }
        }

        public int Bilag
        {
            get
            {
                return m_rec_data.bilag;
            }
            set
            {
                m_rec_data.bilag = value;
            }
        }

        public int Konto
        {
            get
            {
                return m_rec_data.konto;
            }
            set
            {
                m_rec_data.konto = value;
            }
        }

        public decimal Bruttobeløb
        {
            get
            {
                return ((decimal)(m_rec_data.belob)) / 100;
            }
            set
            {
                m_rec_data.belob = (int)(value * 100);
            }
        }

        public int? Nr
        {
            get
            {
                return m_rec_data.Nr;
            }
            set
            {
                m_rec_data.Nr = (int)value;
            }
        }

        public int? Id
        {
            get
            {
                return m_rec_data.Id;
            }
            set
            {
                m_rec_data.Id = (int)value;
            }
        }
        
        public int? Regnskabid
        {
            get
            {
                return m_regnskabid;
            }
            set
            {
                m_regnskabid = (int)value;
            }
        }
        
        public recPosteringer() { }
    }

    public class KarPosteringer : List<recPosteringer>
    {
        private string m_path { get; set; }
        private int m_regnskabid;

        public KarPosteringer()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering;
            m_regnskabid = rec_regnskab.Rid;
            open();
        }

        private void open()
        {
            posttype ord;
            recPosteringer rec;
            DirectoryInfo fld;

            fld = new DirectoryInfo(m_path);
            Regex regexPoster = new Regex(@"^[0123456789]+\.dat", RegexOptions.IgnoreCase);

            int n = 0;
            foreach (FileInfo f in fld.GetFiles())
            {
                Match m = regexPoster.Match(f.Name);
                if (m.Success)
                {
                    string filename = f.FullName;
                    FileStream bs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
                    using (BinaryReader br = new BinaryReader(bs))
                    {
                        while ((bs.Length - bs.Position) >= Marshal.SizeOf(typeof(posttype)))
                        {
                            byte[] buff = br.ReadBytes(Marshal.SizeOf(typeof(posttype)));                           //Read byte array
                            GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);                            //Make sure that the Garbage Collector doesn't move our buffer
                            ord = (posttype)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(posttype));  //Marshal the bytes
                            handle.Free();                                                                          //Give control of the buffer back to the GC 

                            string Tekst = "";
                            int len = buff[12];
                            for (int i = 0; i < len; i++)
                            {
                                Tekst += (char)buff[13 + i];
                            }
                            rec = new recPosteringer
                            {
                                rec_no = n++,
                                rec_data = ord,
                                Tekst = Tekst,
                                Regnskabid = m_regnskabid
                            };
                            this.Add(rec);
                        }
                    }
                } //File end
            }
        }

    }
}
