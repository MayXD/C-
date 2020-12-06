using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEW_SOFTWARE
{
    public partial class Form2 : Form
    {
        Form1 _form1;
        public Form2(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.label5.Text = Convert.ToString(_form1.jx);
            this.label6.Text = Convert.ToString(_form1.cdb);
            this.label7.Text = Convert.ToString(_form1.azxs);
            if (_form1.nzq == 0)
                this.label8.Text = "加装";
            else
                this.label8.Text = "不加装";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            _form1.Close();
            System.Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            _form1.Show();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
