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
    public partial class DSRC : Form
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
        packing_remove_data rmv = new packing_remove_data();
        public DSRC()
        {
            InitializeComponent();
        }


        private void DSRC_Load(object sender, EventArgs e)
        {

            this.loadgrid2();
            this.total();
            loadgrid11();
            textBox1.Select();
            lastboxnumber();
            totalqty();
            totalsapqty();
        }
        void loadgrid(DataGridView dg, String query)
        {

            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            conn.Close();
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            packing_remove_data.QtyID = dataGridView1.SelectedCells[0].Value.ToString();
            txtcode.Text = dataGridView1.SelectedCells[1].Value.ToString();
            txtname.Text = dataGridView1.SelectedCells[2].Value.ToString();
            txtbatch.Text = dataGridView1.SelectedCells[3].Value.ToString();
            label8.Text = dataGridView1.SelectedCells[3].Value.ToString();
            label11.Text = dataGridView1.SelectedCells[4].Value.ToString();
           txtqty.Text = dataGridView1.SelectedCells[4].Value.ToString();
            txtqty.Select();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (txtqty.Text != "" & txtboxno.Text != "" & txtcode.Text != "" & txtname.Text != "" & txtbatch.Text != "")
            {
                if (Convert.ToInt32(txtqty.Text) > 0)
                {

                    if ((Convert.ToInt32(txtqty.Text)) <= (Convert.ToInt32(label11.Text)))
                    {
                        this.Grid2_Save();
                        this.qtyminus();
                        totalqty();
                        totalsapqty();
                    }
                    else
                    {
                        MessageBox.Show("Please select less Quantity than stock");
                    }
                    this.loadgrid2();

                    txtcode.Text = "";
                    txtname.Text = "";
                    txtqty.Text = "";
                    txtbatch.Text = "";
                    textBox1.Text = "";


                    textBox1.Select();
                    this.total();
                }
            }
            else
            {
                MessageBox.Show("Please Fill All Column");

            }
        }

        void qtyminus()
        {
            int insid = Convert.ToInt32(txtqty.Text);
            int lblid = Convert.ToInt32(label11.Text);
            int value = lblid - insid;

            string query = "Update DSRC_SAP_Details set QTY=@QTY where id=@id";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@QTY", value);
                command.Parameters.AddWithValue("@id", packing_remove_data.QtyID);
                conn.Open();
                int result = command.ExecuteNonQuery();
                conn.Close();
            }
        }
        void Search(DataGridView dg, String se)
        {
            string query = "select id,Code,Name,Batch,Qty from DSRC_SAP_Details where Code like('%" + se + "%') and QTY!=0";
            loadgrid(dg, query);
        }

        private void txtcode_TextChanged(object sender, EventArgs e)
        {

        }
        void loadgrid2()
        {
            SqlCommand cmd = new SqlCommand("select [id],[Code],[Name] ,[Batch],[QTY] from DSRC_PACKING_DETAILS where BOX_Number=@zip  ", conn);
            cmd.Parameters.AddWithValue("@zip", txtboxno.Text);
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            conn.Close();
            dataGridView2.DataSource = dt;


        }
        void clickadd_BOX_Number()
        {
            int a, b;
            a = Convert.ToInt32(txtboxno.Text);

            b = a + 1;
            txtboxno.Text = b.ToString();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //this.Grid2_Save();
            this.print();
            totalqty();
            totalsapqty();


            loadgrid11();
            this.clickadd_BOX_Number();
            textBox1.Select();
        }
        void Grid2_Save()
        {
            string query = "INSERT INTO[dbo].[DSRC_Packing_Details]([Box_Number],[Code],[Name],[Batch],[QTY]) VALUES (@Box_Number,@Code,@Name,@Batch,@QTY)";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@Box_Number", txtboxno.Text);
                command.Parameters.AddWithValue("@Code", txtcode.Text);
                command.Parameters.AddWithValue("@Name", txtname.Text);
                command.Parameters.AddWithValue("@Batch", txtbatch.Text);
                command.Parameters.AddWithValue("@QTY", txtqty.Text);
                // txtboxno.Text = rmv.Number;

                conn.Open();
                int result = command.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        void total()
        {
            int a = 0;
            int total = 0;
            foreach (DataGridViewRow pb in dataGridView2.Rows)
            {
                total += Convert.ToInt32(dataGridView2.Rows[a].Cells["QTY1"].Value.ToString());
                a++;
            }
            button2.Text = total.ToString();
            int btotal = Convert.ToInt32(button2.Text);
            int discount = 0;

            label10.Text = discount.ToString();

        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (txtqty.Text != "" & txtboxno.Text != "" & txtcode.Text != "" & txtname.Text != "" & txtbatch.Text != "")
            {
                if (Convert.ToInt32(txtqty.Text) > 0)
                {

                    if ((Convert.ToInt32(txtqty.Text)) <= (Convert.ToInt32(label11.Text)))
                    {
                        this.Grid2_Save();
                        this.qtyminus();
                        totalqty();
                        totalsapqty();
                    }
                    else
                    {
                        MessageBox.Show("Please select less Quantity than stock");
                    }
                    this.loadgrid2();

                    txtcode.Text = "";
                    txtname.Text = "";
                    txtqty.Text = "";
                    txtbatch.Text = "";
                    textBox1.Text = "";
                    textBox1.Select();
                    this.total();
                }
            }
            else
            {
                MessageBox.Show("Please Fill All Column");

            }
            this.print();
            totalqty();
            totalsapqty();


            loadgrid11();
            this.clickadd_BOX_Number();
            textBox1.Select();

        }

        private void txtboxno_TextChanged(object sender, EventArgs e)
        {
            this.loadgrid2();
            this.total();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            rmv.id = dataGridView2.SelectedCells[0].Value.ToString();
            rmv.code = dataGridView2.SelectedCells[1].Value.ToString();
            rmv.Name = dataGridView2.SelectedCells[2].Value.ToString();
            rmv.Batch = dataGridView2.SelectedCells[3].Value.ToString();
            rmv.QTY = dataGridView2.SelectedCells[4].Value.ToString();
            GridremoveDataSave();
            datadelete();
            loadgrid2();
            loadgrid11();
            totalqty();
            totalsapqty();
            this.total();




        }


        void GridremoveDataSave()
        {
            string query = "INSERT INTO[dbo].[DSRC_SAP_Details]([Code],[Name],[Batch],[QTY]) VALUES (@Code,@Name,@Batch,@QTY)";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@Code", rmv.code);
                command.Parameters.AddWithValue("@Name", rmv.Name);
                command.Parameters.AddWithValue("@Batch", rmv.Batch);
                command.Parameters.AddWithValue("@QTY", rmv.QTY);

                conn.Open();
                int result = command.ExecuteNonQuery();
                conn.Close();
            }
        }

        void datadelete()
        {
            conn.Open();
            using (SqlCommand command = new SqlCommand("DELETE FROM DSRC_Packing_Details  WHERE id = '" + rmv.id + "'", conn))
            {
                command.ExecuteNonQuery();
            }
            conn.Close();
        }
        void print()

        {
            packing_remove_data.Number = txtboxno.Text;
            //crystalReportViewer1.Show();
            Form2 a = new Form2();
            a.Show();
        }

        void loadgrid11()
        {
            SqlCommand cmd = new SqlCommand("select [Code],[Name] ,[Batch],[QTY] from DSRC_SAP_Details where QTY!=0  ", conn);
            //cmd.Parameters.AddWithValue("@zip", txtboxno.Text);
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            conn.Close();
            dataGridView1.DataSource = dt;


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.Search(dataGridView1, textBox1.Text);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form3 a = new Form3();
            a.Show();
        }

        void lastboxnumber()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select MAX (Box_Number) as Box_Number from DSRC_Packing_Details  ", conn);
            SqlDataReader dr;
            cmd.Connection = conn;

            dr = cmd.ExecuteReader();
            int b = 0;
            while (dr.Read())
            {
                int X = Convert.ToInt32(dr["Box_Number"]);
                b = X + 1;


            }
            dr.Close();
            dr.Dispose();
            conn.Close();
            txtboxno.Text = b.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        void totalqty()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select SUM (qty) as qty from DSRC_Packing_Details  ", conn);
            SqlDataReader dr;
            cmd.Connection = conn;

            dr = cmd.ExecuteReader();
            int b = 0;
            while (dr.Read())
            {
                label3.Text = dr["qty"].ToString();



            }
            dr.Close();
            dr.Dispose();
            conn.Close();

        }
        void totalsapqty()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select SUM (qty) as qty from DSRC_SAP_Details  ", conn);
            SqlDataReader dr;
            cmd.Connection = conn;

            dr = cmd.ExecuteReader();
            int b = 0;
            while (dr.Read())
            {
                label4.Text = dr["qty"].ToString();



            }
            dr.Close();
            dr.Dispose();
            conn.Close();
        }

        private void DSRC_MouseClick(object sender, MouseEventArgs e)
        {
            this.loadgrid2();
            this.total();
            loadgrid11();
            textBox1.Select();
          //  lastboxnumber();
            totalqty();
            totalsapqty();
        }

        private void txtqty_TextChanged(object sender, EventArgs e)
        {
            btnadd.Select();
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
        }
    }
}
