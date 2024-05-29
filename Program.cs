using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CW
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    // додатковий клас-наслідник Form, який за замовчуванням створює ToolStrip з кнопками:
    // 1) Info - відкриває MessageBox з контентом InfoString
    // 2) Author - відкриває MessageBox з інформацією про автора
    // використовується як батьківський клас для всіх форм програми
    public class FormInfo : Form
    {
        public string InfoString { get; set; }

        public FormInfo()
        {
            InitializeToolStrip();
        }

        private void InitializeToolStrip()
        {
            ToolStrip toolStrip = new ToolStrip();
            ToolStripButton aboutButton = new ToolStripButton("Info");
            ToolStripButton authorButton = new ToolStripButton("Author");

            aboutButton.Click += AboutButton_Click;
            authorButton.Click += AuthorButton_Click;

            toolStrip.Items.Add(aboutButton);
            toolStrip.Items.Add(authorButton);
            Controls.Add(toolStrip);
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(InfoString, "Window Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AuthorButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Konoplianchenko Dmytro. IN-22/1", "Author", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
