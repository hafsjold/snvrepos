using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NCrontab;
using System.Threading;
using System.Threading.Tasks;
using nsPbs3060v2;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace nsMedlem3060v2BatchJob
{
    public enum enumTask
    {
        ReceiveFilesFromPBS = 1,
        ProcessType602Files,
        ProcessType603Files,
        SendFilesToPBS,
        LoadSchedule,
        KontingentNyeMedlemmer,
        SendEmailRykker,
        UpdateMedlemStatus
    }

    public partial class clsMedlem3060v2BatchJob
    {
        static EventWaitHandle _waitStopHandle = new ManualResetEvent(false);

        private T StringToEnum<T>(string name)
        {
            return (T)Enum.Parse(typeof(T), name);
        }

        public void Scheduler()
        {
            LoadSchedule();

            while (true)
            {
                try
                {
                    ObjectParameter idParameter = new ObjectParameter("id", typeof(int));
                    ObjectParameter jobnameParameter = new ObjectParameter("jobname", typeof(string));
                    ObjectParameter returParameter = new ObjectParameter("retur", typeof(int));
                    var result = Program.dbJobQ.jobqueuenext_v2(idParameter, jobnameParameter, returParameter);
                    if ((int)returParameter.Value == 0)
                    {
                       int? id = (int)idParameter.Value;
                       string jobname = (string)jobnameParameter.Value;
                       if (Enum.IsDefined(typeof(enumTask), jobname))
                        {
                            Task<int> task = new Task<int>(() => JobWorker(jobname));
                            task.Start();
                            int resultat = task.Result;
                        }
                       Program.dbJobQ.jobqueuecomplete((int)id);
                    }
                    else
                    {
                        if (_waitStopHandle.WaitOne(2 * 6000))
                            break;
                    }
                }
                catch
                {
                    if (_waitStopHandle.WaitOne(5 * 6000))
                        break;
                }
            }
        }

        private int JobWorker(string jobname)
        {
            try
            {
                if (Enum.IsDefined(typeof(enumTask), jobname))
                {
                    string constr = nsPbs3060v2.clsConnectionstringManager.EFConnection;
                    dbData3060 m_dbData3060 = new dbData3060(constr);
                    enumTask job = StringToEnum<enumTask>(jobname);
                    switch (job)
                    {
                        case enumTask.ReceiveFilesFromPBS:
                            clsSFTP objSFTP = new clsSFTP(m_dbData3060);
                            int AntalImportFiler = objSFTP.ReadFraSFtp(m_dbData3060);  //Læs direkte SFTP
                            objSFTP.DisconnectSFtp();
                            objSFTP = null;

                            clsPbs602 objPbs602 = new clsPbs602();
                            int Antal602Filer = objPbs602.betalinger_fra_pbs(m_dbData3060);
                            objPbs602 = null;

                            clsPbs603 objPbs603 = new clsPbs603();
                            int Antal603Filer = objPbs603.aftaleoplysninger_fra_pbs(m_dbData3060);
                            objPbs603 = null;

                            clsPbs686 objPbs686 = new clsPbs686();
                            int Antal686Filer = objPbs686.aftaleoplysninger_fra_pbs(m_dbData3060);
                            objPbs686 = null;

                            if (Antal686Filer > 0)
                            {
                                clsPbs601 objPbs601a = new clsPbs601();
                                Tuple<int, int> tresult = objPbs601a.advis_auto(m_dbData3060);
                                int AntalAdvis = tresult.Item1;
                                int lobnra = tresult.Item2;
                                if ((AntalAdvis > 0))
                                    objPbs601a.advis_email(m_dbData3060, lobnra);
                                objPbs601a = null;
                            }

                            break;

                        case enumTask.ProcessType602Files:
                            break;

                        case enumTask.ProcessType603Files:
                            break;

                        case enumTask.SendFilesToPBS:
                            break;

                        case enumTask.LoadSchedule:
                            LoadSchedule();
                            break;

                        case enumTask.KontingentNyeMedlemmer:
                            clsPbs601 objPbs601c = new clsPbs601();
                            Tuple<int, int> tresultc = objPbs601c.kontingent_fakturer_auto(m_dbData3060);
                            int AntalKontingent = tresultc.Item1;
                            int lobnrc = tresultc.Item2;
                            if ((AntalKontingent > 0)){
                                objPbs601c.faktura_og_rykker_601_action(m_dbData3060, lobnrc, fakType.fdfaktura);
                                clsSFTP objSFTPc = new clsSFTP(m_dbData3060);
                                objSFTPc.WriteTilSFtp(m_dbData3060, lobnrc);
                                objSFTPc.DisconnectSFtp();
                                objSFTPc = null;
                            }
                            objPbs601c = null;
                            break;

                        case enumTask.SendEmailRykker:
                            clsPbs601 objPbs601b = new clsPbs601();
                            Tuple<int, int> tresultb = objPbs601b.rykker_auto(m_dbData3060);
                            int AntalRykker = tresultb.Item1;
                            int lobnrb = tresultb.Item2;
                            if ((AntalRykker > 0))
                                objPbs601b.rykker_email(m_dbData3060, lobnrb);
                            objPbs601b = null;
                            break;

                        case enumTask.UpdateMedlemStatus:
                            m_dbData3060.UpdateMedlemStatus();
                            break;

                        default:
                            break;
                    }
                }
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        private void LoadSchedule(int days = 2)
        {
            try
            {
                Program.dbJobQ.tblSchedule.Load();
                var Schedules = from s in Program.dbJobQ.tblSchedule.Local orderby s.start select s;
                
                foreach (var s in Schedules)
                {
                    if (Enum.IsDefined(typeof(enumTask), s.jobname))
                    {
                        var occurrence = CrontabSchedule.Parse(s.schedule).GetNextOccurrences(DateTime.Now, DateTime.Now.AddDays(days)).GetEnumerator();
                        while (occurrence.MoveNext())
                            Program.dbJobQ.jobqueueadd(occurrence.Current, s.jobname, s.id, false);
                    }
                }
            }
            catch 
            {
            }
        }

    }
}
