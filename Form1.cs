using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CW
{
    public partial class Form1 : FormInfo
    {
        public double XMin, XMax, Dx, A;
        readonly Random random = new Random();

        public Form1()
        {
            InfoString = "This is main window.\n\n1) Inputed Data - ReadOnly fields with inputed data\n2) Input Data - opens Input Form for inputting initial data\n3) Calculate - starts calculation";
            InitializeComponent();
            XMin = XMax = Dx = A = 0;
        }

        private void inputDataButton_Click(object sender, EventArgs e)
        {
            using (var inputForm = new InputForm(XMin, XMax, Dx, A))
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    // оновлення текстових полів новими значеннями
                    xMinTextBox.Text = inputForm.XMin.ToString();
                    xMaxTextBox.Text = inputForm.XMax.ToString();
                    dxTextBox.Text = inputForm.Dx.ToString();
                    aTextBox.Text = inputForm.A.ToString();

                    // оновлення змінних значеннями з форми введення
                    XMin = inputForm.XMin;
                    XMax = inputForm.XMax;
                    Dx = inputForm.Dx;
                    A = inputForm.A;
                }
            }
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            // перевірка, чи введено допустиме значення кроку Dx
            if (Dx <= 0)
            {
                MessageBox.Show("Please, enter valid step Dx.\n\nYou can do this clicking by 'Input Data'", "Unable to start process", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> firstFuncLines = new List<string>();
            List<string> secondFuncLines = new List<string>();

            double q, f;
            Console.WriteLine($"XMin = {XMin}, XMax = {XMax}, Dx = {Dx}, A = {A}");
            for (double x = XMin; x <= XMax; x += Dx)
            {
                q = random.NextDouble(); // генерація випадкового значення q
                Console.WriteLine($"Q = {q}");

                if (q <= 0.45)
                {
                    try
                    {
                        f = FirstFunc(x, A, q); // обчислення першої функції
                        firstFuncLines.Add($"Result: {Math.Round(f, 3)} | x = {Math.Round(x, 3)}, a = {Math.Round(A, 3)}, q = {Math.Round(q, 3)}");
                    }
                    catch (Exception ex)
                    {
                        firstFuncLines.Add($"Error: {ex.Message} | x = {Math.Round(x, 3)}, a = {Math.Round(A, 3)}, q = {Math.Round(q, 3)}");
                    }
                }
                else
                {
                    try
                    {
                        f = SecondFunc(x); // обчислення другої функції
                        secondFuncLines.Add($"Result: {Math.Round(f, 3)} | x = {Math.Round(x, 3)}, q = {Math.Round(q, 3)}");
                    }
                    catch (Exception ex)
                    {
                        firstFuncLines.Add($"Error: {ex.Message} | x = {Math.Round(x, 3)}, q = {Math.Round(q, 3)}");
                    }
                }
            }

            // створення форм для відображення результатів обчислень
            var CalcForm1 = new CalcForm("ln(a - qx) / x", firstFuncLines);
            var CalcForm2 = new CalcForm("x ^ 1/3", secondFuncLines);

            CalcForm1.Show();
            CalcForm2.Show();
        }

        // функція обчислення першої формули
        private double FirstFunc(double x, double a, double q)
        {
            double numeratorPart = a - q * x;

            if (numeratorPart <= 0)
            {
                throw new Exception("The value under the natural logarithm is not positive");
            }
            else if (x == 0)
            {
                throw new DivideByZeroException("Dividing by zero is impossible");
            }

            return Math.Log(numeratorPart) / x;
        }

        // функція обчислення другої формули
        private double SecondFunc(double x)
        {
            return x < 0 ? -Math.Pow(Math.Abs(x), 1.0 / 3.0) : Math.Pow(x, 1.0 / 3.0);
        }

    }
}
