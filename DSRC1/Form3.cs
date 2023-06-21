using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace DSRC1
{
    public partial class Form3 : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select [Box_Number],[Code],[Name] ,[Batch],[QTY] from DSRC_Packing_Details ", conn);
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            conn.Close();
            dataGridView1.DataSource = dt;
        }
    }
}
