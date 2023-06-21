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

namespace DSRC1
{

    public partial class Form2 : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
        
        //packing_remove_data rmv = new packing_remove_data();
        public Form2()
        {
            InitializeComponent();
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string sqlstring = "select * from DSRC_Packing_Details where Box_Number='" + packing_remove_data.Number + "'";
            SqlDataAdapter da = new SqlDataAdapter(sqlstring, conn);
            DataSet datareport = new DataSet();
            da.Fill(datareport, "DSRC_Packing_Details");

            CrystalReport1 myreport = new CrystalReport1();
            myreport.SetDataSource(datareport);
            crystalReportViewer1.ReportSource = myreport;
        }
    }
}
