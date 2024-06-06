using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Form_Application
{
    public partial class update : Form
    {
        public update()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //UPDATE NAME IN DATABASE
            string reg_no = txtRegno.Text;
            string name = txtFName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string userClass = txtClass.Text;
            string confirm = txtConfirm.Text;
            string gender = txtGender.Text;

            if (string.IsNullOrWhiteSpace(txtFName.Text) ||
                string.IsNullOrWhiteSpace(txtRegno.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtGender.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtConfirm.Text))
            {
                MessageBox.Show("Please fill in all fields");
                return;
            }
            else
            {
                if (password != confirm)
                {
                    MessageBox.Show("Passwords do not match");
                }
                else
                {
                    try
                    {
                       
                        string connString = "Server=localhost;Database=mydatabase;Uid=root;Pwd=mahmoudiman370;";
                        using (MySqlConnection con = new MySqlConnection(connString))
                        {
                            con.Open();
                            string query = "UPDATE details SET Email = @email, Gender = @gender, Name = @name, Password = @password, Class = @userClass WHERE RegNo = @reg_no";
                            MySqlCommand cmd = new MySqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@reg_no", reg_no);
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@password", password);
                            cmd.Parameters.AddWithValue("@gender", gender);
                            cmd.Parameters.AddWithValue("@userClass", userClass);
                            cmd.Parameters.AddWithValue("@email", email);
                            int i = cmd.ExecuteNonQuery();
                            if (i > 0)
                            {
                                MessageBox.Show("Details Updated successfully");
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error:" + ex.Message);
                    }
                }
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void update_Load(object sender, EventArgs e)
        {
           
           try
           {
                // Assuming formlogin instance is already created
                formlogin loginForm = new formlogin();
                // Show the login form
                DialogResult result = loginForm.ShowDialog();

                // Check if the login was successful
                if (result == DialogResult.OK)
                {
                    // Access the RegNO property
                    string RegNo = loginForm.RegNO;
                    // Now you can use the regNo variable as needed
                }



                string connString = "Server=localhost;Database=mydatabase;Uid=root;Pwd=mahmoudiman370;";
    using (MySqlConnection con = new MySqlConnection(connString))
    {
        con.Open();
        string query = "SELECT * FROM details WHERE RegNo= @RegNO";
        using (MySqlCommand cmd = new MySqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@RegNO", RegNO);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    // Check for null before accessing reader fields
                    txtFName.Text = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : "";
                    txtGender.Text = reader["Gender"] != DBNull.Value ? reader["Gender"].ToString() : "";
                    txtClass.Text = reader["Class"] != DBNull.Value ? reader["Class"].ToString() : "";
                    txtPassword.Text = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : "";
                    txtEmail.Text = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "";
                    txtRegno.Text = reader["RegNo"] != DBNull.Value ? reader["RegNo"].ToString() : "";
                }
                else
                {
                    MessageBox.Show("reg not found");
                }
            }
        }
    }
}
catch (MySqlException ex)
{
    MessageBox.Show("Error: " + ex.Message);
}




        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
