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
    public partial class Login : Form
    {
        
        
        public Login()
        {
            InitializeComponent();
        }
        string connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chaikhana;Integrated Security=True;";
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            //this.Dispose();
            this.Hide();
            Menu menu = new Menu();
            menu.Show();

            
            
        }
        public void reset()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public static string user;
        private void button1_Click(object sender, EventArgs e)
        {



            user = textBox1.Text;
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Enter Valid Username Or Password");
            }
            else
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = connectionstring;
                connection.Open();
                var query = "select count(*) from Userdata where Uname='"+textBox1.Text+"' and Upassword='"+textBox2.Text+"'";
                SqlDataAdapter sda = new SqlDataAdapter(query,connection);
                DataTable t = new DataTable();
                sda.Fill(t);
                if (t.Rows[0][0].ToString() == "1")
                {
                    MessageBox.Show("WELCOME TO CHAI . COM");
                    this.Hide();
                    User user = new User();
                    
                    user.Show();
                    reset();

                }
                else
                {
                    MessageBox.Show("Wrong username or Password");
                    reset();
                }
                connection.Close();
            }
        }
    }
}
