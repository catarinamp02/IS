using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionLine
{
    internal class Peca
    {
        public string dataProd { get; set; }
        public string horaProd { get; set; }
        public string codigo { get; set; }
        public int tempoProd { get; set; }
        public int resultadoTeste { get; set; }
        public string descricaoTeste { get; set; }
        public string datateste { get; set; }

        public static readonly Random random = new Random();

        public Peca()
        {
            dataProd = RandomDate();
            horaProd = RandomTime(dataProd);
            codigo = Code();
            tempoProd = random.Next(10, 51);
            resultadoTeste = random.Next(1, 7);
            descricaoTeste = TestDescription(resultadoTeste);
            datateste = RandomTestDate(dataProd);

        }

        public string TestDescription(int resultadoTeste)
        { 

            switch (resultadoTeste)
            {
                case 1:
                    return "OK";
                case 2:
                    return "Falha na inspeção visual";
                case 3:
                    return "Falha na inspeção de resistência";
                case 4:
                    return "Falha na inspeção de dimensões";
                case 5:
                    return "Falha na inspeção de estanquiedade";
                default:
                    return "Desconhecido";
            }
        }
        public string RandomDate()
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

            return dateFormater(year, month, day);
        }

        public string dateFormater(int year, int month, int day)
        {
            string formatedMonth;
            string formatedDay;


            if (month < 10)
            {
                formatedMonth = "0" + month;
            }
            else
            {
                formatedMonth = month.ToString();
            }

            if (day < 10)
            {
                formatedDay = "0" + day;
            }
            else
            {
                formatedDay = day.ToString();
            }

            return year.ToString() + "-" + formatedMonth + "-" + formatedDay;
        }


        public string RandomTime(string productionDate)
        {
            int hour = random.Next(0, 24);

            int minute = random.Next(0, 60);

            int seconds = random.Next(0, 60);

            if (DateTime.Now == DateTime.Parse(productionDate))
            {
                hour = random.Next(1, DateTime.Now.Hour + 1);
                minute = random.Next(1, DateTime.Now.Minute + 1);
                seconds = random.Next(1, DateTime.Now.Second + 1);
            }


            string formatedHour;
            string formatedMinute;
            string formatedSecond;

            if (hour < 10)
            {
                formatedHour = "0" + hour;
            }
            else
            {
                formatedHour = hour.ToString();
            }
            if (minute < 10)
            {
                formatedMinute = "0" + minute;
            }
            else
            {
                formatedMinute = minute.ToString();
            }
            if (seconds < 10)
            {
                formatedSecond = "0" + seconds;
            }
            else
            {
                formatedSecond = seconds.ToString();
            }

            return formatedHour + ":" + formatedMinute + ":" + formatedSecond;
        }

        public string Code()
        {
            string[] ProdType = { "aa", "ab", "ba", "bb" };

            int id = random.Next(100000, 1000000);

            int index = random.Next(0, 4);

            return ProdType[index] + id.ToString();

        }

        public string RandomTestDate(string productionDate)
        {
            string[] date = productionDate.Split("-");

            int prodYear = int.Parse(date[0]);

            int prodMonth = int.Parse(date[1]);

            int prodDay = int.Parse(date[2]);

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

            return dateFormater(prodYear, testMonth, testDay);
        }
    }

}




