using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCrontab;

namespace nsMedlem3060Service
{
    public enum enumTask
    {
        ReceiveFilesFromPBS = 1,
        ProcessType602Files,
        ProcessType603Files,
        SendFilesToPBS
    }

    public class clsSchedule
    {
        public static T StringToEnum<T>(string name)
        {
            return (T)Enum.Parse(typeof(T), name);
        }

        public static void LoadSchedule() { LoadSchedule(2); }
        public static void LoadSchedule(int days)
        {
            dbJobQDataContext dbJobQ = Program.dbJobQDataContextFactory();
            var Schedules = from s in dbJobQ.tblSchedules orderby s.start select s;
            foreach (var s in Schedules)
            {
                if (Enum.IsDefined(typeof(enumTask), s.jobname))
                {
                    var occurrence = CrontabSchedule.Parse(s.schedule).GetNextOccurrences(DateTime.Now, DateTime.Now.AddDays(days)).GetEnumerator();
                    while (occurrence.MoveNext())
                        dbJobQ.jobqueueadd(occurrence.Current, s.jobname, s.id, false);
                }
            }
        }
    }

}
