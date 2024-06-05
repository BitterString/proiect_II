using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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

        private SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0LALPAV\SQLEXPRESS;Initial Catalog=BankApp;persist security info=True;Integrated Security=SSPI;");

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Introduceți un username și o parolă", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Există un câmp invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                conn.Open();

                // Check if username exists
                bool usernameExists = UsernameExists(username);

                // Check if password matches for the given username
                bool isAdminPasswordValid = IsValidUser("Admin", username, password);
                bool isClientPasswordValid = IsValidUser("Client", username, password);

                if (!usernameExists)
                {
                    // If username doesn't exist, check if the password is also incorrect
                    bool passwordExists = PasswordExists(password);
                    if (!passwordExists)
                    {
                        MessageBox.Show("Username și parolă incorecte", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Username incorect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return; // Stop further execution
                }
                else if (!isAdminPasswordValid && !isClientPasswordValid)
                {
                    MessageBox.Show("Parola incorectă", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Stop further execution
                }
                else
                {
                    // Check Admin Credentials
                    if (isAdminPasswordValid)
                    {
                        Form5 adminForm = new Form5();
                        adminForm.Show();
                        this.Hide();
                        return;
                    }

                    // Check Client Credentials
                    if (isClientPasswordValid)
                    {
                        Salveaza.ClientId = GetClientId(username, password);

                        Form6 clientForm = new Form6();
                        clientForm.Show();
                        this.Hide();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private bool UsernameExists(string username)
        {
            string query = "SELECT COUNT(*) FROM (SELECT Username FROM Admin UNION SELECT Username FROM Client) AS Users WHERE Username = @username";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        private bool PasswordExists(string password)
        {
            string query = "SELECT COUNT(*) FROM (SELECT Password FROM Admin UNION SELECT Password FROM Client) AS Users WHERE Password = @password";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@password", password);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        private bool IsValidUser(string tableName, string username, string password)
        {
            string query = $"SELECT COUNT(*) FROM {tableName} WHERE username = @username AND password = @password";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        private int GetClientId(string username, string password)
        {
            string query = "SELECT ClientID FROM Client WHERE Username = @username AND Password = @password";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Convert.ToInt32(reader["ClientID"]);
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
