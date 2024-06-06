using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Form_Application
{
    public partial class AddStudentForm : Form
    {
        string connectionString = "Server=localhost;Database=mydatabase;Uid=root;Pwd=mahmoudiman370;";
        public AddStudentForm()
        {
            InitializeComponent();
            dobpicker.ValueChanged += dobvalue_ValueChanged;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Name = txtName.Text;
            string Class = txtClass.Text;
            DateTime dobValue = dobpicker.Value;
            string Gender = comboGender.Text;
            

            
            string updateQuery = $"UPDATE mydatabase.details SET Class = '{Class}', Date_of_birth = '{dobValue.ToString("yyyy-MM-dd")}', Gender = '{Gender}' WHERE Name = '{Name}'";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
            
            try
            {
                connection.Open();
                int rowsAffected = updateCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Details updated successfully");
                    

                }
                else
                {
                    MessageBox.Show("not updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


    


        

        private void dobvalue_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboGender_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void AddStudentForm_Load(object sender, EventArgs e)
        {

            comboGender.Items.Add("Male");
            comboGender.Items.Add("Female");
        }
    }
}
