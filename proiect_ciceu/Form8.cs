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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy hh:mm tt"; // Ensure AM/PM is displayed
            // Add event handler for ValueChanged
            dateTimePicker1.ValueChanged += DateTimePicker1_ValueChanged;




            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ciceu\Documents\VS_projects\ProiectII_repo\BankApp.mdf;Integrated Security=True;Connect Timeout=30");

            try
            {
                // Open the connection
                conn.Open();

                // Get the ClientID from the static property in Form3
                int clientId = Form3.Salveaza.ClientId;

                // Define the SQL query to fetch data from the Credit table for the specific client
                string query = "SELECT Nume, Prenume FROM Client WHERE ClientID = @clientId";

                // Create a SqlCommand with the query and connection
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add the clientId parameter to the SqlCommand
                    cmd.Parameters.AddWithValue("@clientId", clientId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox1.Text = $"{reader["Nume"]} {reader["Prenume"]}";
                        }
                    }
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
        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDateTime = dateTimePicker1.Value;

            // Get the hour and minute components
            int hour = selectedDateTime.Hour;
            int minute = selectedDateTime.Minute;

            // Adjust the minute to the nearest 30-minute increment
            if (minute < 30)
            {
                minute = 0;
            }
            else if (minute > 30)
            {
                minute = 0;
                hour += 1;

            }
            else minute = 30;


            if (hour < 8)
            {
                hour = 8;
                minute = 0;
            }
            else if (hour >= 16)
            {
                hour = 15;
                minute = 30;
            }

            // Update the DateTimePicker value
            dateTimePicker1.Value = new DateTime(selectedDateTime.Year, selectedDateTime.Month, selectedDateTime.Day, hour, minute, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ciceu\Documents\VS_projects\ProiectII_repo\BankApp.mdf;Integrated Security=True;Connect Timeout=30");

            try
            {
                // Open the connection
                conn.Open();

                // Get the ClientID from the static property in Form3
                int clientId = Form3.Salveaza.ClientId;

                // Get the current date and time for DataInregistrare
                DateTime dataInregistrare = DateTime.Now;

                // Use the selected date and time from dateTimePicker1 for Data
                DateTime data = dateTimePicker1.Value;

                Random random = new Random();
                int numar = random.Next();


                string check = "SELECT COUNT(*) FROM Programare WHERE Data = @data";

                using (SqlCommand checkCmd = new SqlCommand(check, conn))
                {

                    checkCmd.Parameters.AddWithValue("@data", data);

                    int programariCount = (int)checkCmd.ExecuteScalar();

                    if (programariCount > 0)
                    {
                        MessageBox.Show("Ocupat! Alege alta data.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                string insertQuery = @"
            INSERT INTO Programare (ClientID,Numar, DataInregistrare, Data) 
            OUTPUT inserted.Numar
            VALUES (@clientId, @Numar, @dataInregistrare, @data)
        ";


                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    // Add the parameters to the SqlCommand
                    cmd.Parameters.AddWithValue("@clientId", clientId);
                    cmd.Parameters.AddWithValue("@Numar", numar);
                    cmd.Parameters.AddWithValue("@dataInregistrare", dataInregistrare);
                    cmd.Parameters.AddWithValue("@data", data);

                    // Execute the command and retrieve the generated Numar
                    string generatedNumar = cmd.ExecuteScalar().ToString();

                    // Display the generated Numar in a message box
                    MessageBox.Show($"Reservation saved successfully. Numar: {generatedNumar}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during data insertion
                MessageBox.Show($"Error saving reservation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Close the connection
                conn.Close();
            }
        }
    }
}
