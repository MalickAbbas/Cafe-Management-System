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
    public partial class Items : Form
    {
        public Items()
        {
            InitializeComponent();
        }
        string connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chaikhana;Integrated Security=True;";

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login set = new Login();
            set.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            User user = new User();
            user.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserData data = new UserData();
            data.Show();
        }

        private void Items_Load(object sender, EventArgs e)
        {
            insert();
        }
        public void reset()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox1.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Fill all the Fields");
            }
            else
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = connectionstring;
                connection.Open();


                SqlCommand commands = new SqlCommand();
                commands.Connection = connection;


                commands.CommandType = CommandType.Text;
                commands.CommandText = "INSERT into Itemsdata (Item_Num , Item_Name,Item_price , Category) VALUES ('" + textBox1.Text + "','"+ textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')";



                commands.ExecuteNonQuery();
                MessageBox.Show("SuccessFully Added");
                reset();
                connection.Close();
                insert();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Fill all the Fields");
            }
            else
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = connectionstring;
                connection.Open();

                //step 2 Create sql command

                SqlCommand commands = new SqlCommand();
                commands.Connection = connection;

                // step 3 run quesries
                SqlCommand command = new SqlCommand("Update Itemsdata set Item_Num=@Item_Num , Item_Name=@Item_Name , Item_price=@Item_price , Category=@Category where Item_Num=@Item_Num");
                command.Connection = connection;
                command.Parameters.AddWithValue("@Item_Num", textBox1.Text);
                command.Parameters.AddWithValue("@Item_Name", textBox2.Text);
                command.Parameters.AddWithValue("@Item_price", textBox3.Text);
                command.Parameters.AddWithValue("@Category", textBox4.Text);

                command.ExecuteNonQuery();
                MessageBox.Show("Updated Successfully");
                insert();
                reset();

                connection.Close();
            }
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

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Select the Field to be Deleted");
            }
            else
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = connectionstring;
                connection.Open();



                SqlCommand commands = new SqlCommand("delete from Itemsdata where Item_Num=@Item_Num");
                commands.Connection = connection;
                commands.Parameters.AddWithValue("@Item_Num", textBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(commands);
                DataSet ds = new DataSet();
                da.Fill(ds);
                MessageBox.Show("Deleted Successfully");
                reset();
                insert();
                connection.Close();

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["Item_Num"].Value.ToString();
                textBox2.Text = row.Cells["Item_Name"].Value.ToString();
                textBox3.Text = row.Cells["Item_price"].Value.ToString();
                textBox4.Text = row.Cells["Category"].Value.ToString();
            }
        }
    }
}
