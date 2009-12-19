﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;


namespace nsPuls3060
{
    public static class clsUtil
    {
        const double J_correction_01_01_4712BC = 1721119.5;
        const double J_correction_01_01_1900 = -693899;

        public static int SummaDateTime2Serial(DateTime dt)
        {
            double sumStartDate = 1088647360;
            DateTime MSStartDate = new DateTime(2009, 1, 1);
            double days = (double)DateAndTime.DateDiff("d", MSStartDate, dt, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays);
            return (int)(sumStartDate + days * 32);
        }

        public static DateTime SummaSerial2DateTime(double J)
        {
            double sumStartDate = 1088647360;
            DateTime MSStartDate = new DateTime(2009, 1, 1);
            double days = (J - sumStartDate) / 32;
            if (days > -7000)
            {
                return MSStartDate.AddDays(days);
            }
            else
            {
                return new DateTime(1900, 1, 1);
            }
        }

        public static double MSDateTime2Serial(DateTime dt)
        {
            return GregorianDate2JulianDayNumber(dt, J_correction_01_01_1900);
        }

        public static DateTime MSSerial2DateTime(double J)
        {
            return JulianDayNumber2GregorianDate(J, J_correction_01_01_1900);
        }

        private static double GregorianDate2JulianDayNumber(DateTime dt, double pJ0)
        {
            // formel from website: http://www.astro.uu.nl/~strous/AA/en/reken/juliaansedag.html

            int j = dt.Year;
            int m = dt.Month;
            double d = (double)dt.Day + (double)dt.Hour / 24 + (double)dt.Minute / (24 * 60) + (double)dt.Second / (24 * 3600);
            //If m < 3, then replace m by m + 12 and j by j − 1
            if (m < 3)
            {
                m += 12;
                j--;
            }

            //(Eq. 1) c = ⌊j/100⌋
            double c = Math.Floor((double)j / 100);

            //(Eq. 2) x = j − 100*c = j mod 100
            double x = j - 100 * c;

            //(Eq. 3) J₀ = 1721119.5
            double J0 = pJ0;

            //(Eq. 4) J₁ = ⌊146097*c/4⌋
            double J1 = Math.Floor(146097 * c / 4);

            //(Eq. 5) J₂ = ⌊36525*x/100⌋
            double J2 = Math.Floor(36525 * x / 100);

            //(Eq. 6) J₃ = ⌊(153*m − 457)/5⌋
            double J3 = Math.Floor((153 * (double)m - 457) / 5);

            //(Eq. 7) J = J₁ + J₂ + J₃ + d − 1 + J₀ = ⌊146097*c/4⌋ + ⌊36525*x/100⌋ + ⌊(153*m − 457)/5⌋ + d − 1 + 1721119.5
            double J = J1 + J2 + J3 + d - 1 + J0;

            return J;
        }

        private static DateTime JulianDayNumber2GregorianDate(double J, double pJ0)
        {
            // formel from website: http://www.astro.uu.nl/~strous/AA/en/reken/juliaansedag.html

            double J0 = pJ0;

            //1. (Eq. 8) x₂ = J − 1721119.5
            double x2 = J - J0;

            //2. (Eq. 9) c₂ = ⌊(8*x₂ + 7)/292194⌋
            double c2 = Math.Floor((8 * x2 + 7) / 292194);

            //3. (Eq. 10) x₁ = x₂ − ⌊146097*c₂/4⌋
            double x1 = x2 - Math.Floor(146097 * c2 / 4);

            //4. (Eq. 11) c₁ = ⌊(200*x₁ + 199)/73050⌋
            double c1 = Math.Floor((200 * x1 + 199) / 73050);

            //5. (Eq. 12) x₀ = x₁ − ⌊36525*c₁/100⌋
            double x0 = x1 - Math.Floor(36525 * c1 / 100);

            //6. (Eq. 13) j = 100*c₂ + c₁
            double j = 100 * c2 + c1;

            //7. (Eq. 14) m = ⌊(10*x₀ + 923)/306⌋
            double m = Math.Floor((10 * x0 + 923) / 306);

            //8. (Eq. 15) d = x₀ − ⌊(153*m − 457)/5⌋ + 1
            double d = x0 - Math.Floor((153 * m - 457) / 5) + 1;

            //9. If m > 12, then replace m by m − 12 and j by j + 1
            if (m > 12)
            {
                j++;
                m -= 12;
            }

            double ts = d - Math.Floor(d);
            double h = Math.Floor(ts * 24);
            ts = ts - h / 24;
            double min = Math.Floor(ts * 24 * 60);
            ts = ts - min / (24 * 60);
            double s = Math.Round(ts * 24 * 60 * 60);
            ts = ts - s / (24 * 60 * 60);
            DateTime dt;
            if (d < 1)
            {
                dt = new DateTime((int)j, (int)m, 1, (int)h, (int)min, (int)s);
                return dt.AddDays(-1);
            }
            else
            {
                return new DateTime((int)j, (int)m, (int)d, (int)h, (int)min, (int)s);
            }
        }
    }
}
