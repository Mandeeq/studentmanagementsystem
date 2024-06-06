using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;



namespace Form_Application
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string reg_no = txtRegno.Text;
            string name = txtFName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string Class = "3";
            string confirm = txtConfirm.Text;
            string gender = comboGender.Text;

            if (string.IsNullOrWhiteSpace(txtFName.Text) ||
            string.IsNullOrWhiteSpace(txtRegno.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text) ||
            string.IsNullOrWhiteSpace(comboGender.Text) ||

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
                    MessageBox.Show("passwords do not match");
                }
                else
                {
                    try
                    {
                        string connString = "Server=localhost;Database=mydatabase;Uid=root;Pwd=mahmoudiman370;";
                        using (MySqlConnection con = new MySqlConnection(connString))
                        {
                            con.Open();
                            string query = "INSERT INTO details (RegNo,Email, Gender, Name, Password, Class) VALUES (@reg_no, @email, @gender, @name, @password, @Class)";
                            MySqlCommand cmd = new MySqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@password", password);
                            cmd.Parameters.AddWithValue("@gender", gender);
                            cmd.Parameters.AddWithValue("@Class", Class);
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@reg_no", reg_no);
                            int i = cmd.ExecuteNonQuery();
                            if (i > 0)
                            {
                                MessageBox.Show("Details Registered successfully");
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

        private void Form1_Load(object sender, EventArgs e)
        {
            comboGender.Items.Add("Male");
            comboGender.Items.Add("Female");
        }
        private void ClearForm()
        {
            txtFName.Text="";
            txtRegno.Text = "";
            txtEmail.Text = "";
            comboGender.SelectedIndex = -1;
            txtPassword.Text = "";
            txtConfirm.Text = "";
        }

        private void txtFName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFName_Leave(object sender, EventArgs e)
        {

        }

        private void checkBxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBxShowPas.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtConfirm.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
                txtConfirm.PasswordChar = '*';
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            {
                txtFName.Text = "";
                txtRegno.Text = "";
                txtEmail.Text = "";
                comboGender.SelectedIndex = -1;
                txtPassword.Text = "";
                txtConfirm.Text = "";
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            formlogin loginForm = new formlogin();
            loginForm.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            AddStudentForm addrecords = new AddStudentForm();
            addrecords.Show();
            this.Hide();
        }

        private void comboGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }


    
}
