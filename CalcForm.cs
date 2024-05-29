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
    public partial class CalcForm : FormInfo
    {

        public CalcForm(string funcLabel, List<string> lines)
        {
            InfoString = $"This is window with calculation results for function:\n\n{funcLabel}";
            InitializeComponent();

            functionLabel.Text = $"Function: {funcLabel}";
            amountLabel.Text = $"Amount of operations: {lines.Count()}";

            foreach (string line in lines)
            {
                Console.WriteLine($"{funcLabel} | {line}");
                resultListBox.Items.Add(line);
            }
        }

    }
}
