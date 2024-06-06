using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form_Application
{
    public partial class dashboardForm : Form
    {
        public dashboardForm()
        {
            InitializeComponent();
        }
        public void dashboard_load(object sender, EventArgs e)
        {
            try
            {
                string connString = "Server=localhost;Database=mydatabase;Uid=root;Pwd=mahmoudiman370;";
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    con.Open();
                    string query = "SELECT * FROM details";
                    MySqlCommand cmd = new MySqlCommand(query, con);

                    DataTable dataTable = new DataTable();
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                    dataAdapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtSearch.Text;
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Please enter registration number to search");
                return;
            }
            else
            {
                try
                {
                    string connString = "Server=localhost;Database=mydatabase;Uid=root;Pwd=mahmoudiman370;";
                    using (MySqlConnection con = new MySqlConnection(connString))
                    {
                        con.Open();
                        string query = "SELECT * FROM details WHERE Name = @name";
                        MySqlCommand cmd = new MySqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@name", txtSearch.Text);

                        DataTable dataTable = new DataTable();
                        MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                        dataAdapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            dataGridView1.DataSource = dataTable;
                        }
                        else
                        {
                            MessageBox.Show("No records found for the provided name");
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error:" + ex.Message);
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Assuming dataGridView1 is the DataGridView control
            // Check if any row is selected
            if (dataGridView1.SelectedRows.Count > 0)
            {
                MessageBox.Show("you have selected a row");
                // Get the value of the identifier column (e.g., primary key value) from the selected row
                //int selectedRecordId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["RegNo"].Value);
                object selectedRecordid = dataGridView1.SelectedRows[0].Cells["RegNo"].Value;
                Console.WriteLine("RegNo Cell Value: " + selectedRecordid); // Output the cell value to console


                // Prompt the user to confirm deletion
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Construct the DELETE query
                        string connString = "Server=localhost;Database=mydatabase;Uid=root;Pwd=mahmoudiman370;";
                        using (MySqlConnection con = new MySqlConnection(connString))
                        {
                            con.Open();
                            string query = "DELETE FROM details WHERE RegNo = @RegNo";
                            MySqlCommand cmd = new MySqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@RegNo", selectedRecordid);

                            // Execute the DELETE query
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Record deleted successfully");

                                // Refresh the DataGridView to reflect the changes
                                // Implement this method to reload data into the DataGridView
                                dashboard_load(sender, e);

                            }
                            else
                            {
                                MessageBox.Show("Failed to delete record");
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a record to delete", "No Record Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dashboardForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Connection string
                string connString = "Server=localhost;Database=mydatabase;Uid=root;Pwd=mahmoudiman370;";

                // Create a new MySqlConnection object
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    // Open the connection
                    con.Open();

                    // SQL query to select all records from the table
                    string query = "SELECT * FROM details";

                    // Create a MySqlCommand object with the query and connection
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        // Create a DataTable to hold the results
                        DataTable dataTable = new DataTable();

                        // Create a MySqlDataAdapter to fetch the data
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            // Fill the DataTable with the data from the database
                            dataAdapter.Fill(dataTable);
                        }

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Handle any errors
                MessageBox.Show("Error: " + ex.Message);
            }
        }
       
        private void button4_Click(object sender, EventArgs e)
        {
            dashboardForm_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //update record in db
            update update = new update();
            update.Show();
            this.Close();
        }
    }
}
