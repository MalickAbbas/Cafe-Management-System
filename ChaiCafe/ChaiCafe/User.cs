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
    
    public partial class User : Form
    {
        string connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chaikhana;Integrated Security=True;";
        public User()
        {
            InitializeComponent();
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

            string query = "select * from Itemsdata where Category ='"+comboBox1.SelectedItem.ToString()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, connection);
            SqlCommandBuilder bb = new SqlCommandBuilder(sda);
            var data = new DataSet();
            sda.Fill(data);
            dataGridView1.DataSource = data.Tables[0];

            connection.Close();


        }
        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login log = new Login();
            log.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        DataTable table = new DataTable();
        private void User_Load(object sender, EventArgs e)
        {
            insert();
            table.Columns.Add("Num", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("Price", typeof(int));
            table.Columns.Add("Tprice", typeof(int));
            label8.Text = DateTime.Today.Day.ToString()+"/"+ DateTime.Today.Month.ToString() + "/"+ DateTime.Today.Year.ToString();
            label3.Text = Login.user;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Items item = new Items();
            item.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            insertfilter();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        public void afteradd()
        {
            qty = 0;
            price = 0;
            total = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (textBox1.Text == "")
            {
                MessageBox.Show("Enter Quantity");
            }
            if(flag == 0)
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
                orderamt = orderamt+total;
                label4.Text = orderamt.ToString();



            }
        }
        


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                flag = 1; 
                       item = row.Cells["Item_Name"].Value.ToString();
                       price =Convert.ToInt32( row.Cells["Item_price"].Value.ToString());
                        
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserData udata = new UserData();
            udata.Show();
        }
        int flag = 0;
        int num=0;
        int price=0, qty=0, total=0 , orderamt = 0;

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
                commands.CommandText = "INSERT into Orders VALUES ('" + textBox2.Text + "','" + label8.Text + "','" + label3.Text+ "','" +label4.Text+ "')";



                commands.ExecuteNonQuery();
                MessageBox.Show("Ordered SuccessFully");

                connection.Close();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            order or = new order();
            or.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        string item;
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
