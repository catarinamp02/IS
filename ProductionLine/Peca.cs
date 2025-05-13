using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionLine
{
    internal class Peca
    {
        public DateOnly dataProd { get; set; }
        public TimeOnly horaProd { get; set; }
        public string codigo { get; set; }
        public int tempoProd { get; set; }
        public int resultadoTeste { get; set; }
        public DateOnly datateste { get; set; }

        public static readonly Random random = new Random();

        public Peca()
        {
            dataProd = randomDate();
            horaProd = randomTime(dataProd);
            codigo = Code();
            tempoProd = random.Next(10, 51);
            resultadoTeste = random.Next(1, 7);
            datateste = randomTestDate(dataProd);

        }

        public DateOnly randomDate()
        {
            int month;
            int day;

            int year = random.Next(2021, 2026);

            if (year == 2025)
            {
                month = random.Next(1, DateTime.Now.Month + 1);
            }
            else
            {
                month = random.Next(1, 13);
            }

            if (year == 2025 && month == DateTime.Now.Month)
            {
                day = random.Next(1, DateTime.Now.Day + 1);
            }
            else
            {
                day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);
            }

            DateOnly date = new DateOnly(year, month, day);

            return date;
        }

        public TimeOnly randomTime(DateOnly productionDate)
        {
            int hour = random.Next(0, 24);

            int minute = random.Next(0, 60);

            int seconds = random.Next(0, 60);

            if (DateOnly.FromDateTime(DateTime.Now) == productionDate)
            {
                hour = random.Next(1, DateTime.Now.Hour + 1);
                minute = random.Next(1, DateTime.Now.Minute + 1);
                seconds = random.Next(1, DateTime.Now.Second + 1);
            }

            TimeOnly time = new TimeOnly(hour, minute, seconds);

            return time;
        }

        public string Code()
        {
            string[] ProdType = { "aa", "ab", "ba", "bb" };

            int id = random.Next(100000, 1000000);

            int index = random.Next(0, 4);

            return ProdType[index] + id.ToString();

        }

        public DateOnly randomTestDate(DateOnly productionDate)
        {

            int prodYear = productionDate.Year;

            int prodMonth = productionDate.Month;

            int prodDay = productionDate.Day;

            int testYear = prodYear;

            int testMonth;

            int testDay;

            if (testYear == DateTime.Now.Year)
            {
                testMonth = random.Next(prodMonth, DateTime.Now.Month + 1);

                if (testMonth == prodMonth)
                {
                    testDay = random.Next(prodDay, DateTime.Now.Day + 1);
                }
                else
                {
                    testDay = random.Next(1, DateTime.DaysInMonth(prodYear, prodMonth) + 1);
                }


            }
            else
            {
                testMonth = random.Next(prodMonth, 13);
                testDay = random.Next(1, DateTime.DaysInMonth(prodYear, prodMonth) + 1);

            }

            DateOnly testDate = new DateOnly(testYear, testMonth, testDay);

            return testDate;
        }
    }

}




