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
    public partial class InputForm : FormInfo
    {
        public double XMin, XMax, Dx, A;

        public InputForm(double xmin, double xmax, double dx, double a)
        {
            InfoString = "This window is used to enter the initial data.\n\nRules:\n1) XMin must be less than XMax\n2) Dx must not equal 0, otherwise an error will occur in the main window\n3) Step must be less than XMax - XMin";
            InitializeComponent();

            // встановлення початкових значень змінних
            XMin = xmin;
            XMax = xmax;
            Dx = dx;
            A = a;

            // заповнення текстових полів початковими значеннями
            xMinTextBox.Text = XMin.ToString();
            xMaxTextBox.Text = XMax.ToString();
            dxTextBox.Text = Dx.ToString();
            aTextBox.Text = A.ToString();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            try
            {
                // парсинг значень з текстових полів та присвоєння їх змінним
                XMin = double.Parse(xMinTextBox.Text);
                XMax = double.Parse(xMaxTextBox.Text);
                Dx = double.Parse(dxTextBox.Text);
                A = double.Parse(aTextBox.Text);

                // перевірка коректності введених значень
                if (XMin > XMax)
                {
                    throw new Exception("XMax must be greater than XMin");
                }
                else if (Dx > XMax - XMin)
                {
                    throw new Exception("XMax - XMin must be greater than step Dx");
                }

                DialogResult = DialogResult.OK; // встановлення результату діалогу
                Close(); // закриття форми
            }
            catch (Exception ex)
            {
                // відображення повідомлення про помилку
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
