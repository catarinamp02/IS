namespace Sistema_Legado
{
    public partial class Form1 : Form
    {

        private static readonly Random random = new Random();

        public Form1()
        {
            InitializeComponent();

            randomDataGenerator();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            randomDataGenerator();
        }

        private void randomDataGenerator()
        {
            dateProd.Text = randomDate();

            hourProd.Text = randomHour();

            code.Text = Code();

            int randomTempoProd = random.Next(10, 51);

            timeProd.Text = randomTempoProd.ToString();

            int testResult = random.Next(1, 11);

            if (testResult == 10)
            {
                resultadoTeste.Text = testResult.ToString();
            }
            else
            {
                resultadoTeste.Text = "0" + testResult.ToString();
            }
            
            testDate.Text = randomTestDate(dateProd.Text);
        }

        private string randomDate()
        {
            int year = random.Next(2000, 2026);

            int month = random.Next(1, 13);

            int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);

            return dateFormater(year, month, day);
        }

        private string dateFormater(int year, int month, int day)
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

        private string randomHour()
        {
            int hour = random.Next(0, 24);

            int minute = random.Next(0, 60);

            int seconds = random.Next(0, 60);

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

        private string Code()
        {
            string[] ProdType = { "aa", "ab", "ba", "bb" };

            int id = random.Next(100000, 1000000);

            int index = random.Next(0, 4);

            return ProdType[index] + id.ToString();

        }

        private string randomTestDate(string productionDate)
        {
            string[] date = productionDate.Split("-");

            int prodYear = int.Parse(date[0]);

            int prodMonth = int.Parse(date[1]);

            int testMonth = random.Next(prodMonth, 13);

            int prodDay = int.Parse(date[2]);

            int testDay;

            if (testMonth == prodMonth)
            {
                testDay = random.Next(prodDay, DateTime.DaysInMonth(prodYear, prodMonth) + 1); ;
            }
            else
            {
                testDay = random.Next(1, DateTime.DaysInMonth(prodYear, prodMonth) + 1);
            }

            return dateFormater(prodYear, testMonth, testDay);
        }
    }
}
