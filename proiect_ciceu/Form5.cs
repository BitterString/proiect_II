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

namespace proiect_ciceu
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0LALPAV\SQLEXPRESS;Initial Catalog=BankApp;persist security info=True;Integrated Security=SSPI;");

        DataTable dt = new DataTable();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                // Define the SQL query to fetch data from the Client table
                string query = "SELECT * FROM Client";

                // Create a SqlDataAdapter to execute the query and fill a DataTable
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    conn.Open();

                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        // Assuming the primary key column in your Client table is named "ClientID"
                        int clientId = Convert.ToInt32(row.Cells["ClientID"].Value);

                        // Define the SQL query to delete the record from the Client table
                        string deleteQuery = "DELETE FROM Client WHERE ClientID = @ClientID";

                        using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@ClientID", clientId);
                            cmd.ExecuteNonQuery();
                        }

                        // Remove the row from the DataGridView
                        dataGridView1.Rows.Remove(row);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Please select at least one row to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Client", conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                // Apply the changes to the database
                int updatedRows = adapter.Update(dt);

                MessageBox.Show($"{updatedRows} rows updated successfully.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh the data grid view
                dt.Clear();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}