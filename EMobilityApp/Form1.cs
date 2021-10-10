using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMobilityApp
{
    public partial class Form1 : Form
    {
        BdeProcessor processor = new ();
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            ClearButtons();
            btn.BackColor = Color.Chocolate;
            var res = await processor.SendState(button1.Text);
            if (res) {
                btn.BackColor = Color.SaddleBrown;
                tableLayoutPanel1.BackColor = Color.Black;
            }
            else { btn.BackColor = Color.DarkRed; tableLayoutPanel1.BackColor = Color.Yellow; }
            
//            panel1.BackColor = Color.Red;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         //   tableLayoutPanel1.BackColor = Color.Yellow;
            
        }

        private void ClearButtons()
        {
            foreach (var ctrl in tableLayoutPanel1.Controls)
            {
                var btn = ctrl as Button;
                if (btn is not null)
                    btn.BackColor = Color.Black;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            processor.ToggleErrorMode();
        }
    }
}
