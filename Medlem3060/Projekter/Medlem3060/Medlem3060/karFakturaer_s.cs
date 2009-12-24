using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace nsPuls3060
{
    public struct ordtype
    {
        public int faknr;
        public int fakid;
        public int int8;
        public int int12;
        public int dato;
        public int int20;
        public int forfdato;
        public int int28;
        public int int32;
        public int fakbelob;
        public int int40;
        public int saldo;
        public int int48;
        public int int52;
        public int const_1079574528;
        public int valuta;
        public int debitornr;
        public int int68;
        public int statenum;

        public ordtype(int p_fakid, DateTime p_dato, DateTime p_forfdato, int p_fakbelob, int p_debitornr)
        {
            fakid = p_fakid;
            dato = clsUtil.SummaDateTime2Serial(p_dato);
            forfdato = clsUtil.SummaDateTime2Serial(p_forfdato);
            fakbelob = p_fakbelob;
            saldo = p_fakbelob;
            debitornr = p_debitornr;

            faknr = 0;
            const_1079574528 = 1079574528;
            valuta = 1263223811;
            statenum = 0;
            int8 = 0;
            int12 = 0;
            int20 = 0;
            int28 = 0;
            int32 = 0;
            int40 = 0;
            int48 = 0;
            int52 = 0;
            int68 = 0;
        }
    }


    public class recFakturaer_s
    {
        private int m_rec_no;
        private ordtype m_rec_data;
        
        public int rec_no {
            get
            {
                return m_rec_no;
            }
            set
            {
                m_rec_no = value;
            }
        }
        public ordtype rec_data { 
            get
            {
                return m_rec_data;
            } 
            set
            {
                m_rec_data = value;
            } 
        }
        public int faknr
        {
            get
            {
                return m_rec_data.faknr;
            }
            set
            {
                m_rec_data.faknr = value;
            }
        }
        public int fakid
        {
            get
            {
                return m_rec_data.fakid;
            }
            set
            {
                m_rec_data.fakid = value;
            }
        }
        public DateTime dato
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
        public DateTime forfdato
        {
            get
            {
                return clsUtil.SummaSerial2DateTime(m_rec_data.forfdato);
            }
            set
            {
                m_rec_data.forfdato = clsUtil.SummaDateTime2Serial(forfdato);
            }
        }
        public int fakbelob
        {
            get
            {
                return m_rec_data.fakbelob;
            }
            set
            {
                m_rec_data.fakbelob = value;
            }
        }
        public int debitornr
        {
            get
            {
                return m_rec_data.debitornr;
            }
            set
            {
                m_rec_data.debitornr = value;
            }
        }        
        public recFakturaer_s() { }
    }

    public class KarFakturaer_s : List<recFakturaer_s>
    {
        private string m_path { get; set; }

        public KarFakturaer_s()
        {
            var rec_regnskab = (from r in Program.dbData3060.TblRegnskab
                                join a in Program.dbData3060.TblAktivtRegnskab on r.Rid equals a.Rid
                                select r).First();

            m_path = rec_regnskab.Placering + "fakturaer_s.dat";
            open();
        }

        private void open()
        {
            FileStream bs = new FileStream(m_path + "_test", FileMode.Open, FileAccess.Read, FileShare.None);
            ordtype ord;
            recFakturaer_s rec;
            int n = 0;
            using (BinaryReader br = new BinaryReader(bs))
            {
                while ((bs.Length - bs.Position) >= Marshal.SizeOf(typeof(ordtype)))
                {
                    ord = FromBinaryReaderBlock(br);
                    rec = new recFakturaer_s { rec_no = n++, rec_data = ord };
                    this.Add(rec);
                }
            }
        }

        public void save()
        {
            FileStream bs = new FileStream(m_path + "_test", FileMode.Truncate, FileAccess.Write, FileShare.None);
            using (BinaryWriter bw = new BinaryWriter(bs))
            {
                var qry_this = from d in this
                               orderby d.rec_no
                               select d;
                foreach (var rec in qry_this)
                {
                    ToBinaryWriterBlock(bw, rec.rec_data);
                }
            }
        }

        public static ordtype FromBinaryReaderBlock(BinaryReader br)
        {
            byte[] buff = br.ReadBytes(Marshal.SizeOf(typeof(ordtype)));                                //Read byte array
            GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);                                //Make sure that the Garbage Collector doesn't move our buffer
            ordtype s = (ordtype)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(ordtype));  //Marshal the bytes
            handle.Free();                                                                              //Give control of the buffer back to the GC 
            return s;
        }

        public static void ToBinaryWriterBlock(BinaryWriter bw, ordtype s)
        {
            byte[] buff = new byte[Marshal.SizeOf(typeof(ordtype))];         //Create Buffer
            GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);     //Hands off GC
            Marshal.StructureToPtr(s, handle.AddrOfPinnedObject(), false);   //Marshal the structure
            handle.Free();                                                   //Give control of the buffer back to the GC
            bw.Write(buff);                                                  //Write byte array
        }
    }
}
