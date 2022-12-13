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
    public partial class Menu : Form
    {
        
        public Menu()
        {
            InitializeComponent();
        }
        int flag = 0;
        int num = 0;
        int price = 0, qty = 0, total = 0, orderamt = 0;
        string item;
        DataTable table = new DataTable();
        string connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chaikhana;Integrated Security=True;";
        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void insert()
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionstring;
            connection.Open();

            string query = "select * from Itemsdata";
            SqlDataAdapter sda = new SqlDataAdapter(query, connection);
            SqlCommandBuilder bb = new SqlCommandBuilder(sda);
            var data = new DataSet();
            sda.Fill(data);
            dataGridView1.DataSource = data.Tables[0];

            connection.Close();


        }
        public void insertfilter()
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionstring;
            connection.Open();

            string query = "select * from Itemsdata where Category ='" + comboBox1.SelectedItem.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, connection);
            SqlCommandBuilder bb = new SqlCommandBuilder(sda);
            var data = new DataSet();
            sda.Fill(data);
            dataGridView1.DataSource = data.Tables[0];

            connection.Close();


        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                flag = 1;
                item = row.Cells["Item_Name"].Value.ToString();
                price = Convert.ToInt32(row.Cells["Item_price"].Value.ToString());

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Enter Quantity");
            }
            if (flag == 0)
            {

                MessageBox.Show("Select an Item");
            }
            else
            {
                num = num + 1;
                qty = Convert.ToInt32(textBox1.Text);
                total = qty * price;
                table.Rows.Add(num, item, qty, price, total);
                dataGridView2.DataSource = table;
                orderamt = orderamt + total;
                label4.Text = orderamt.ToString();



            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            insertfilter();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "Order Num")
            {
                MessageBox.Show("Plzz Enter Order Number");
            }
            else
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = connectionstring;
                connection.Open();


                SqlCommand commands = new SqlCommand();
                commands.Connection = connection;


                commands.CommandType = CommandType.Text;
                commands.CommandText = "INSERT into Orders VALUES ('" + textBox2.Text + "','" + label8.Text + "','" + label3.Text + "','" + label4.Text + "')";



                commands.ExecuteNonQuery();
                MessageBox.Show("Ordered SuccessFully");

                connection.Close();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            order od = new order();
            od.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Menu_Load(object sender, EventArgs e)
        {
            insert();
            table.Columns.Add("Num", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("Price", typeof(int));
            table.Columns.Add("Tprice", typeof(int));
            label8.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }
    }
}
