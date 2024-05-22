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

namespace Login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadSumsIntoListBox();
        }
        private void LoadSumsIntoListBox()
        {
            string connectionString = "Data Source=DESKTOP-7U2ECFL;Initial Catalog=BankApp;Integrated Security=True"; // Update with your actual connection string
            string query = "SELECT Suma FROM Cont";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        listBox1.Items.Clear();
                        while (reader.Read())
                        {
                            listBox1.Items.Add(reader.GetDecimal(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // For example, displaying the selected item in a MessageBox or another control
            if (listBox1.SelectedItem != null)
            {
                MessageBox.Show("Selected sum: " + listBox1.SelectedItem.ToString());
            }
        }
    }
}
          