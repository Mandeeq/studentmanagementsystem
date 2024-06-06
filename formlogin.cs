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
using MySql.Data.MySqlClient;

namespace Form_Application
{
    public partial class formlogin : Form
    {
        public string RegNO{ get; private set;}
        public formlogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRegno.Text) ||
              string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            Public:
            string RegNO = txtRegno.Text;
            string Password = txtPassword.Text;

            string connectionString = "Server=localhost;Database=mydatabase;Uid=root;Pwd=mahmoudiman370;";
            string query = $"SELECT password FROM details WHERE RegNo= '{RegNO}'";
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = connectionString;
            MySqlCommand command = new MySqlCommand(query, con);
            try
            {
                con.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    string storedPassword = result.ToString();

                    if (Password == storedPassword)
                    {
                        MessageBox.Show("You have logged in succesfully");
                        RegNO = RegNO;
                        this.DialogResult = DialogResult.OK;
                        dashboardForm DashboardForm = new dashboardForm();
                        DashboardForm.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect password");
                    }
                }
                else
                {
                    MessageBox.Show("Student ID not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
        }

        private void checkBxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBxShowPas.Checked)
            {
                txtPassword.PasswordChar = '\0';
                
            }
            else
            {
                txtPassword.PasswordChar = '*';
               
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            new Registration().Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
