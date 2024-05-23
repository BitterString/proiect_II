using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proiect_ciceu
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            try
            {
                // Open the connection
                conn.Open();

                // Get the ClientID from the static property in Form3
                int clientId = Form3.Salveaza.ClientId;

                // Define the SQL query to fetch data from the Credit table for the specific client
                string query = "SELECT * FROM Credit WHERE ClientID = @clientId";

                // Create a SqlCommand with the query and connection
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add the clientId parameter to the SqlCommand
                    cmd.Parameters.AddWithValue("@clientId", clientId);

                    // Create a SqlDataAdapter to execute the query and fill a DataTable
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Bind the DataTable to the DataGridView
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during data retrieval
                MessageBox.Show($"Error fetching data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Close the connection
                conn.Close();
            }
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ciceu\Documents\VS_projects\ProiectII_repo\BankApp.mdf;Integrated Security=True;Connect Timeout=30");

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(Form3.Salveaza.ClientId);
        }
    }
}
