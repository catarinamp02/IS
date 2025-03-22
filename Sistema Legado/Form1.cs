namespace Sistema_Legado
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            randomDataGenerator();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            randomDataGenerator();
        }


        private void randomDataGenerator ()
        {
            Random random = new Random();

            dateProd.Text = randomDate();

            hourProd.Text = randomHour();

            code.Text = Code();

            int randomTempoProd = random.Next(10, 51);

            timeProd.Text = randomTempoProd.ToString();

            int testResult = random.Next(1, 7);

            resultadoTeste.Text = "0" + testResult.ToString();
        }

        private string randomDate()
        {
            Random random = new Random();

            int year = random.Next(2000, 2026);

            int month = random.Next(1, 13);

            int day;

            if (month % 2 != 0 || month == 7 || month == 8)
            {
                day = random.Next(1, 32);
            }
            else if (year % 4 == 0 && month == 2)
            {
                day = random.Next(1, 30);
            }
            else if (year % 4 != 0 && month == 2)
            {
                day = random.Next(1, 29);
            }
            else
            {
                day = random.Next(1,31);
            }

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
            Random random = new Random();

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
            Random random = new Random();

            string[] ProdType = {"aa", "ab", "ba", "bb" };

            int id = random.Next(100000, 1000000);

            int index = random.Next(0,4);

            return ProdType[index] + id.ToString();

        }
    }
}
