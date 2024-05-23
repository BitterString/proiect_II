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
    public partial class Form3 : Form
    {
        public static class Salveaza
        {
            public static int ClientId { get; set; }
        }
        public Form3()
        {
            InitializeComponent();
            textBox2.PasswordChar = '•';

        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ciceu\Documents\VS_projects\ProiectII_repo\BankApp.mdf;Integrated Security=True;");
        private void button1_Click(object sender, EventArgs e)
        {
            String username, password;
            username = textBox1.Text;
            password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                conn.Open();

                // Check Admin Credentials
                if (IsValidUser("Admin", username, password))
                {
                    Form5 adminForm = new Form5();
                    adminForm.Show();
                    this.Hide(); // Optionally hide the current form
                    return;
                }

                // Check Client Credentials
                if (IsValidUser("Client", username, password))
                {     Salveaza.ClientId =GetClientId( username, password);

                    // aici va veni implementat formul pentru Cont
                    Form6 clientForm = new Form6();

                    clientForm.Show();
                    this.Hide(); // Optionally hide the current form
                    return;
                }

                // If no match found
                MessageBox.Show("Invalid username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                textBox1.Clear();
                textBox2.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private bool IsValidUser(string tableName, string username, string password)
        {
            string query = $"SELECT * FROM {tableName} WHERE username = @username AND password = @password";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt.Rows.Count > 0;
                }


            }
        }
        private int GetClientId(string username, string password)
        {
            string query = "SELECT ClientID FROM Client WHERE Username = @username AND Password = @password";
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ciceu\Documents\VS_projects\ProiectII_repo\BankApp.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Read the ClientID directly from the first (and only) row
                            return Convert.ToInt32(reader["ClientID"]);
                        }
                        else
                        {
                            // No rows found
                            return -1;
                        }
                    }
                }
            }
        }





    }
}
