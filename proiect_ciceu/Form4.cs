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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-4E2J2L9;Initial Catalog=BankApp;Integrated Security=True");

        private void Form4_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Nume")
            {
                textBox1.Text = "";
                textBox1.ForeColor= Color.Black; 
            }

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {   if (textBox1.Text == "")
                {
                    textBox1.Text = "Nume";

                    textBox1.ForeColor = Color.Silver;
                }

         }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("All fields must be filled!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"INSERT INTO Client (Nume, Prenume, NumarTelefon, Email, username, password, AdminID) VALUES (@Nume, @Prenume, @NumarTelefon, @Email, @username, @password, @AdminID)", conn);

                cmd.Parameters.Add("@Nume", SqlDbType.NVarChar).Value = textBox1.Text;
                cmd.Parameters.Add("@Prenume", SqlDbType.NVarChar).Value = textBox3.Text;
                cmd.Parameters.Add("@NumarTelefon", SqlDbType.NVarChar).Value = textBox2.Text; // Change to NVarChar assuming phone number as text
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = textBox4.Text;
                cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = textBox5.Text;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = textBox6.Text;
                cmd.Parameters.Add("@AdminID", SqlDbType.Int).Value = 1;

                cmd.ExecuteNonQuery();

                MessageBox.Show("Client registered successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Form2 form = new Form2();
                form.Show();
                this.Hide(); // Optionally hide the current form after registration
            }
            catch(SqlException ex) when(ex.Number == 2627 || ex.Number == 2601)
            {
                MessageBox.Show("Could not register: this username is already in use.Please choose another one!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not register: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}