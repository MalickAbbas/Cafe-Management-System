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

namespace ChaiCafe
{
    public partial class order : Form
    {
        public order()
        {
            InitializeComponent();
        }
        string connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chaikhana;Integrated Security=True;";
        public void insert()
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionstring;
            connection.Open();

            string query = "select * from Orders";
            SqlDataAdapter sda = new SqlDataAdapter(query, connection);
            SqlCommandBuilder bb = new SqlCommandBuilder(sda);
            var data = new DataSet();
            sda.Fill(data);
            dataGridView1.DataSource = data.Tables[0];

            connection.Close();


        }

        private void order_Load(object sender, EventArgs e)
        {
            insert();
        }

        private void button3_Click(object sender, EventArgs e)
        {
             this.Hide();
        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            e.Graphics.DrawString("****** CHAI . COM ******", new Font("Lucida Handwritting", 32, FontStyle.Bold), Brushes.Red, new Point(140, 40));
            e.Graphics.DrawString("**** ORDER SUMMARY ****", new Font("Lucida Handwritting", 32, FontStyle.Bold), Brushes.Red, new Point(130,80));
            e.Graphics.DrawString("ORDER NUMBER : "+dataGridView1.Rows[0].Cells["order_num"].Value.ToString()+"", new Font("Lucida Handwritting", 25, FontStyle.Bold), Brushes.Black, new Point(110, 155));
            e.Graphics.DrawString("ORDER DATE : " + dataGridView1.Rows[0].Cells["order_date"].Value.ToString() + "", new Font("Lucida Handwritting", 25, FontStyle.Bold), Brushes.Black, new Point(110, 195));
            e.Graphics.DrawString("Seller     : " + dataGridView1.Rows[0].Cells["user"].Value.ToString() + "", new Font("Lucida Handwritting", 25, FontStyle.Bold), Brushes.Black, new Point(110, 230));
            e.Graphics.DrawString("ORDER AMOUNT : " + dataGridView1.Rows[0].Cells["order_amount"].Value.ToString() + "", new Font("Lucida Handwritting", 25, FontStyle.Bold), Brushes.Black, new Point(110, 270));
            e.Graphics.DrawString("******* POWERED BY ********", new Font("Lucida Handwritting", 32, FontStyle.Bold), Brushes.Red, new Point(140, 340));
            e.Graphics.DrawString("******* MALICK ABBAS ********", new Font("Lucida Handwritting", 32, FontStyle.Bold), Brushes.Red, new Point(140, 380));
        }
    }
}
