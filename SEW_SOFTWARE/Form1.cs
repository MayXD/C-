using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEW_SOFTWARE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static bool closeflag = true;
        public float zj;
        public float cdb;
        public float secNum;
        public String azxs;
        public int nzq;

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection conn = DBcon.MyCon();
            string cdbSql = "select * from CDB";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(cdbSql, conn);
                SqlDataReader cdbSdr = cmd.ExecuteReader();
                while (cdbSdr.Read())
                {
                    comboBox1.Items.Add(cdbSdr["cdb"].ToString().Trim());
                }
                cdbSdr.Close();
            }
            catch
            {
                MessageBox.Show("sql error");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            this.comboBox2.Items.Add(Convert.ToString(1.1));
            this.comboBox2.Items.Add(Convert.ToString(1.5));
            this.comboBox2.Items.Add(Convert.ToString(1.8));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(this.comboBox1.SelectedItem.ToString());
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                zj = Convert.ToSingle(this.textBox1.Text.Trim());
                cdb = Convert.ToSingle(this.comboBox1.Text.Trim());
                secNum = Convert.ToSingle(this.comboBox2.Text.Trim());
                if (this.comboBox1.Text == "")
                    MessageBox.Show("请选择一个传动比");
                if (this.textBox1.Text == "")
                    MessageBox.Show("请输入正确的转矩");
                if (this.radioButton1.Checked)
                    azxs = "F";
                else if (this.radioButton2.Checked)
                    azxs = "K/T";
                else
                    MessageBox.Show("请选择是否加装逆止器");
                if (this.radioButton3.Checked)
                    nzq = 1;
                else if(this.radioButton4.Checked)
                    nzq = 0;
                else
                    MessageBox.Show("请选择一种安装型式");
            }
            catch
            {
                MessageBox.Show("未知错误");
            }
            finally
            {
                Form2 Form2 = new Form2(this);
                this.Hide();
                Form2.Show();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //if (radioButton4.Checked)
            //    radioButton3.Checked = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
