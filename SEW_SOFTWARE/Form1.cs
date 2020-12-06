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
        public static bool jumpFlag = false;
        public int jx;
        public float zj;
        public float cdb;
        public float secNum;
        public String azxs;
        public int nzq;
        public String searchJx;

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
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
            this.Close();
            System.Environment.Exit(0);
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
                {
                    MessageBox.Show("请输入正确的转矩");
                    this.textBox1.Focus();
                }
                else if (Convert.ToDouble(this.textBox1.Text) > 432)
                    MessageBox.Show("你输入的转矩过大");

                if (this.comboBox2.Text == "")
                    MessageBox.Show("请输入或选择一个安全系数");
                    this.comboBox2.Focus();

                if (this.radioButton1.Checked)
                    azxs = "F";
                else if (this.radioButton2.Checked)
                    azxs = "KT";
                else
                    MessageBox.Show("请选择是否加装逆止器");

                if (this.radioButton3.Checked)
                    nzq = 1;
                else if(this.radioButton4.Checked)
                    nzq = 0;
                else
                    MessageBox.Show("请选择一种安装型式");
                /// 计算客户需要的实际转矩
                float relZj = zj * secNum;
                SqlConnection conn2 = DBcon.MyCon();
                try
                {
                    conn2.Open();
                    /// 查找 额定转矩-机型 表，得到最接近实际转矩的额定转矩，从大到小开始循环
                    string jxSql = "select jx from EDZJ where edzj <= '" + relZj + "'order by jx desc";
                    SqlDataAdapter zjDa = new SqlDataAdapter(jxSql, conn2);
                    DataSet zjDs = new DataSet();
                    zjDa.Fill(zjDs);
                    DataTable zjDt = zjDs.Tables[0];
                    int[] jxList = new int[zjDt.Rows.Count];

                    for (int i=0;i<zjDt.Rows.Count;i++)
                    {
                        DataRow zjDr = zjDt.Rows[i];
                        jxList[i] = Convert.ToInt16(zjDr[0]);
                    }

                    foreach (int tmpJx in jxList)
                    {
                        ///第一个额定转矩，以及对应的机型，以此类推
                        String searchJx = "F" + (tmpJx - 100) / 10;
                        string cdbSql2 = "select "+searchJx+" from "+azxs+"";
                        SqlDataAdapter cdbDa = new SqlDataAdapter(cdbSql2, conn2);
                        DataSet cdbDs = new DataSet();
                        cdbDa.Fill(cdbDs);
                        DataTable cdbDt = cdbDs.Tables[0];
                        float[] cdbList = new float[cdbDt.Rows.Count];
                        ///查找本次机型以及安装形式对应的 机型-传动比 表，匹配用户选择的传动比
                        foreach (float tmpCdb in cdbList)
                        {
                            if (tmpCdb == cdb)
                                cdb = tmpCdb;
                                jx = tmpJx;
                                jumpFlag = true;
                                return;
                        }
                    } 
                }
                catch(Exception ex)
                {
                    MessageBox.Show("SQL ERROR" + ex.Message);
                }
                finally
                {
                    if (conn2.State == ConnectionState.Open)
                    {
                        conn2.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("参数填写错误" + ex.Message);
            }
            finally
            {
                if (jumpFlag)
                {
                    Form2 Form2 = new Form2(this);
                    this.Hide();
                    Form2.Show();
                }
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            /// 允许输入数字、小数点、删除键和负号  
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != (char)('.'))
            {
                MessageBox.Show("请输入正确的数字");
                this.textBox1.Text = "";
                e.Handled = true;
            }
            /*小数点只能输入一次*/
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text.IndexOf('.') != -1)
            {
                MessageBox.Show("请输入正确的数字");
                this.textBox1.Text = "";
                e.Handled = true;
            }
            /*第一位不能为小数点*/
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text == "")
            {
                MessageBox.Show("请输入正确的数字");
                this.textBox1.Text = "";
                e.Handled = true;
            }
            /*第一位是0，第二位必须为小数点*/
            if (e.KeyChar != (char)('.') && ((TextBox)sender).Text == "0")
            {
                MessageBox.Show("请输入正确的数字");
                this.textBox1.Text = "";
                e.Handled = true;
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
    }
}
