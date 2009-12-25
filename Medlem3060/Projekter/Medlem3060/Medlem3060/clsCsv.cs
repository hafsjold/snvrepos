using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsPuls3060
{
    
    public class clsCsv
    {
        public delegate void MedlemFieldUpdateHandler();
        public static event MedlemFieldUpdateHandler MedlemFieldUpdate;
        public static event MedlemFieldUpdateHandler MedlemCsvStringUpdate;

        private String m_value;
        private int m_n;
        private String m_ln;
        private Int32 m_lnLen;
        private Int32 m_niveau;
        private Int32 m_nr;
        private Int32 m_fld_start;
        private Int32 m_fld_end;
        private clsCsv[] m_stack;
        private String m_ln_updated;
        private String m_value_new;
        private CsvToDoType m_todo;
        private Int32 m_comma_start;
        private Int32 m_comma_end;

        public String value
        {
            get
            {
                return m_value;
            }
            set
            {
                m_value = value;
            }
        }

        public int n
        {
            get
            {
                return m_n;
            }
            set
            {
                m_n = value;
            }
        }

        public String ln_raw
        {
            get
            {
                return m_ln;
            }
        }
        
        public String ln
        {
            get
            {
                return m_ln + ",";
            }
            set
            {
                m_ln = value;
                m_lnLen = m_ln.Length;

            }
        }

        public Int32 lnLen
        {
            get
            {
                return m_lnLen;
            }
            set
            {
                m_lnLen = value;
            }
        }

        public Int32 niveau
        {
            get
            {
                return m_niveau;
            }
            set
            {
                m_niveau = value;
            }
        }

        public Int32 nr
        {
            get
            {
                return m_nr;
            }
            set
            {
                m_nr = value;
            }
        }

        public Int32 fld_start
        {
            get
            {
                return m_fld_start;
            }
            set
            {
                m_fld_start = value;
            }
        }

        public Int32 fld_end
        {
            get
            {
                return m_fld_end;
            }
            set
            {
                m_fld_end = value;
            }
        }

        public clsCsv[] stack
        {
            get
            {
                return m_stack;
            }
            set
            {
                m_stack = value;
            }
        }

        public Boolean multifld
        {
            get
            {
                if (m_niveau == 0)
                {
                    return Program.memMedlemDictionary.multifld(m_nr);
                }
                else
                {
                    return false;
                }

            }
        }

        public String ln_updated
        {
            get
            {
                return m_ln_updated;
            }
            set
            {
                m_ln_updated = value;
            }
        }

        public String value_new
        {
            get
            {
                return m_value_new;
            }
            set
            {
                m_value_new = value;
            }
        }

        public CsvToDoType todo
        {
            get
            {
                return m_todo;
            }
            set
            {
                m_todo = value;
            }
        }

        public Int32 comma_start
        {
            get
            {
                return m_comma_start;
            }
            set
            {
                m_comma_start = value;
            }
        }

        public Int32 comma_end
        {
            get
            {
                return m_comma_end;
            }
            set
            {
                m_comma_end = value;
            }
        }

        public clsCsv()
        {
            m_stack = new clsCsv[1];
            todo = CsvToDoType.fdUpdateClsMedlem;
        }

        public void todoAction()
        {
            if ((m_todo & CsvToDoType.fdUpdateCsvString) == CsvToDoType.fdUpdateCsvString) MedlemCsvStringUpdate();
            if ((m_todo & CsvToDoType.fdUpdateClsMedlem) == CsvToDoType.fdUpdateClsMedlem) MedlemFieldUpdate();
        }

        public void Push()
        {
            object objcopy = this.MemberwiseClone();
            clsCsv cp = (clsCsv)objcopy;

            m_ln = m_value;
            m_lnLen = m_ln.Length;
            m_value = "";

            m_stack[m_niveau] = cp;
            m_niveau = m_niveau + 1;
        }

        public void Pop()
        {
            m_niveau = m_niveau - 1;
            m_value = m_stack[m_niveau].value;
            m_n = m_stack[m_niveau].n;
            m_ln = m_stack[m_niveau].ln_raw;
            m_lnLen = m_stack[m_niveau].lnLen;
            m_fld_start = m_stack[m_niveau].fld_start;
            m_fld_end = m_stack[m_niveau].fld_end;
            m_nr = m_stack[m_niveau].nr;
            m_ln_updated = m_stack[m_niveau].ln_updated;
            m_value_new = m_stack[m_niveau].value_new;
            m_comma_start = m_stack[m_niveau].comma_start;
            m_comma_end = m_stack[m_niveau].comma_end;

            m_stack[m_niveau] = null;
        }
    }


    public enum ParseStatus : int
    {
        fdParserInField = 1,
        fdParserInFieldQuoted = 2,
        fdParserInFieldDoubleQuoted = 4,
        fdParserOutsideField = 8
    }

    [Flags]
    public enum FoundChars : int
    {
        fdNoneFound = 0,
        fdAlfaFound = 1,
        fdNumberFound = 2,
        fdBlankFound = 4,
        fdSignFound = 8,
        fdCommaFound = 16,
        fdDotFound = 32,
        fdMoreCommasFound = 64,
        fdQuoteFound = 128,
        fdDoubleQuoteFound = 256
    }

    [Flags]
    public enum CsvToDoType : int
    {
        fdDoNothing = 0,
        fdUpdateCsvString = 1,
        fdUpdateClsMedlem = 2
    }
}
