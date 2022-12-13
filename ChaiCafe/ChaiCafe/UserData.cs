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
    public partial class UserData : Form
    {
        public UserData()
        {
            InitializeComponent();
        }
        string connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chaikhana;Integrated Security=True;";

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login fr = new Login();
            fr.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            User md = new User();
            md.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Items back = new Items();
            back.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void reset()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
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
                commands.CommandText = "INSERT into Userdata (Uname , Uphone , Upassword) VALUES ('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')";



                commands.ExecuteNonQuery();
                MessageBox.Show("SuccessFully Added");
                reset();
                connection.Close();
                insert();
            }



        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

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



                SqlCommand commands = new SqlCommand("delete from Userdata where Uname=@Uname");
                commands.Connection = connection;
                commands.Parameters.AddWithValue("@Uname", textBox2.Text);
                SqlDataAdapter da = new SqlDataAdapter(commands);
                DataSet ds = new DataSet();
                da.Fill(ds);
                MessageBox.Show("Deleted Successfully");
                reset();
                insert();
                connection.Close();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
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
                SqlCommand command = new SqlCommand("Update Userdata set Uname=@Uname , Uphone=@Uphone , Upassword=@Upassword  where Uname=@Uname");
                command.Connection = connection;
                command.Parameters.AddWithValue("@Uname", textBox2.Text);
                command.Parameters.AddWithValue("@Uphone", textBox3.Text);
                command.Parameters.AddWithValue("@Upassword", textBox4.Text);

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

                string query = "select * from Userdata";
                SqlDataAdapter sda = new SqlDataAdapter(query, connection);
                SqlCommandBuilder bb = new SqlCommandBuilder(sda);
                var data = new DataSet();
                sda.Fill(data);
                dataGridView1.DataSource = data.Tables[0];

                connection.Close();
            

        }

        private void UserData_Load(object sender, EventArgs e)
        {
            insert();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
                if(e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox2.Text = row.Cells["Uname"].Value.ToString();
                textBox3.Text = row.Cells["Uphone"].Value.ToString();
                textBox4.Text = row.Cells["Upassword"].Value.ToString();
            }
              
        }
    }
}
