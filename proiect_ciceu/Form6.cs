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
                conn.Open();
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
            try
            {
                conn.Open();
                int clientId = Form3.Salveaza.ClientId;

                // Define the SQL query
                string query = "SELECT T.[TranzactieID], T.[FromContID], T.[ToContID], T.[Tip], T.[Suma], T.[Data] " +
                "FROM Tranzactie T " +
                "JOIN Cont SenderCont ON T.[FromContID] = SenderCont.[ContID] " +
                "WHERE SenderCont.[ClientID] = @clientId;";

                // Create a SqlCommand with the query and connection
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add the clientId parameter to the SqlCommand
                    cmd.Parameters.AddWithValue("@clientID", clientId);

                    // Create a SqlDataAdapter to execute the query and fill a DataTable
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Bind the DataTable to the DataGridView
                    dataGridView2.DataSource = dt;
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
            try
            {
                conn.Open();
                int clientId = Form3.Salveaza.ClientId;

                // Define the SQL query to fetch all values for the specific client
                string queryClientValues = "SELECT * FROM Cont WHERE ClientID = @clientId";

                using (SqlCommand cmd = new SqlCommand(queryClientValues, conn))
                {
                    cmd.Parameters.AddWithValue("@clientId", clientId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Bind the DataTable to the DataGridView
                    dataGridView3.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching balance: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }



        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-4E2J2L9;Initial Catalog=BankApp;Integrated Security=True");

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 simulatorCredit = new Form1();
            simulatorCredit.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 convertor = new Form1();
            convertor.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form8 form = new Form8();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pentru un credit nou faceți o programare.");
        }
    }
}
