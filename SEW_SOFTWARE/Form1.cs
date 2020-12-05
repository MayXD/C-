﻿using System;
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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection conn = BaseClass.database.myCon();
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
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(this.comboBox1.SelectedItem.ToString());
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
