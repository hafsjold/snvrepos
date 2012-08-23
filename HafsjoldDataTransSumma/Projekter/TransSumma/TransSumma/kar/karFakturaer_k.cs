using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;


namespace nsPuls3060
{
    public struct ordtype_k
    {
        public int faknr;
        public int fakid;
        public int kreditorbilagsnr;
        public int int12;
        public int dato;
        public int int20;
        public int forfdato;
        public int momsbelob;
        public int int32;
        public int fakbelob;
        public int int40;
        public int saldo;
        public int int48;
        public int int52;
        public int const_1079574528;
        public int valuta;
        public int kreditornr;
        public int faktype;
        public int statenum;

        public ordtype_k(int p_fakid, DateTime p_dato, DateTime p_forfdato, int p_fakbelob, int p_kreditornr, int p_momsbelob, int p_kreditorbilagsnr)
        {
            fakid = p_fakid;
            dato = clsUtil.SummaDateTime2Serial(p_dato);
            forfdato = clsUtil.SummaDateTime2Serial(p_forfdato);
            fakbelob = p_fakbelob;
            saldo = p_fakbelob;
            kreditornr = p_kreditornr;
            momsbelob = p_momsbelob;
            faktype = 2; //2=Kreditor Faktura, 3=Kreditor Kreditnota
            kreditorbilagsnr = p_kreditorbilagsnr;

            faknr = 0;
            const_1079574528 = 1079574528;
            valuta = 1263223811;
            statenum = 0;
            int12 = 0;
            int20 = 0;
            int32 = 0;
            int40 = 0;
            int48 = 0;
            int52 = 0;
        }
    }
    public class recFakturaer_k
    {
        private int m_rec_no;
        private ordtype_k m_rec_data;

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
        public ordtype_k rec_data
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
        public int momsbelob
        {
            get
            {
                return m_rec_data.momsbelob;
            }
            set
            {
                m_rec_data.momsbelob = value;
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
        public int saldo
        {
            get
            {
                return m_rec_data.saldo;
            }
            set
            {
                m_rec_data.saldo = value;
            }
        }
        public int kreditornr
        {
            get
            {
                return m_rec_data.kreditornr;
            }
            set
            {
                m_rec_data.kreditornr = value;
            }
        }
        public int faktype
        {
            get
            {
                return m_rec_data.faktype;
            }
            set
            {
                m_rec_data.faktype = value;
            }
        }       public recFakturaer_k() { }
    }

    public class KarFakturaer_k : List<recFakturaer_k>
    {
        private string m_path { get; set; }

        public KarFakturaer_k()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "fakturaer_k.dat";
            open();
        }

        private void open()
        {
            FileStream bs = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            ordtype_k ord;
            recFakturaer_k rec;
            int n = 0;
            using (BinaryReader br = new BinaryReader(bs))
            {
                while ((bs.Length - bs.Position) >= Marshal.SizeOf(typeof(ordtype_k)))
                {
                    ord = FromBinaryReaderBlock(br);
                    rec = new recFakturaer_k { rec_no = n++, rec_data = ord };
                    this.Add(rec);
                }
            }
        }

        public void save()
        {
            FileStream bs = new FileStream(m_path, FileMode.Truncate, FileAccess.Write, FileShare.None);
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

        public static ordtype_k FromBinaryReaderBlock(BinaryReader br)
        {
            byte[] buff = br.ReadBytes(Marshal.SizeOf(typeof(ordtype_k)));                              //Read byte array
            GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);                                //Make sure that the Garbage Collector doesn't move our buffer
            ordtype_k s = (ordtype_k)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(ordtype_k));  //Marshal the bytes
            handle.Free();                                                                              //Give control of the buffer back to the GC 
            return s;
        }

        public static void ToBinaryWriterBlock(BinaryWriter bw, ordtype_k s)
        {
            byte[] buff = new byte[Marshal.SizeOf(typeof(ordtype_k))];       //Create Buffer
            GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);     //Hands off GC
            Marshal.StructureToPtr(s, handle.AddrOfPinnedObject(), false);   //Marshal the structure
            handle.Free();                                                   //Give control of the buffer back to the GC
            bw.Write(buff);                                                  //Write byte array
        }
    }
}
