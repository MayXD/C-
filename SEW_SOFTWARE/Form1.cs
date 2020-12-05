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
        public string connString = "Data Source = localhost；Initial Catalog = SEW；User ID = wuyue；Pwd = Kylin123.";
        public SqlConnection conn;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connString);
            DataSet cdbDs = new DataSet();
            string cdbSql = "select * from CDB";
            try
            {
                conn.Open();
                SqlDataAdapter cdbAdp = new SqlDataAdapter(cdbSql, conn);
                cdbAdp.Fill(cdbDs);
                this.comboBox1.DataSource = cdbDs.Tables[0].DefaultView;
                this.comboBox1.DisplayMember = "cbd";
                this.comboBox1.ValueMember = "cbd";
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}
