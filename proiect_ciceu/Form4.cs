using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace proiect_ciceu
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0LALPAV\SQLEXPRESS;Initial Catalog=BankApp;persist security info=True;Integrated Security=SSPI;");

        private void Form4_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Nume")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Nume";
                textBox1.ForeColor = Color.Silver;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nume = textBox1.Text;
            string prenume = textBox3.Text;
            string telefon = textBox2.Text;
            string email = textBox4.Text;
            string username = textBox5.Text;
            string password = textBox6.Text;

            var invalidFields = new List<string>();

            // Validate name
            if (string.IsNullOrEmpty(nume) || !IsValidName(nume))
            {
                invalidFields.Add("Nume");
            }

            // Validate surname
            if (string.IsNullOrEmpty(prenume) || !IsValidName(prenume))
            {
                invalidFields.Add("Prenume");
            }

            // Validate phone number
            if (string.IsNullOrEmpty(telefon) || !IsValidPhoneNumber(telefon))
            {
                invalidFields.Add("Număr de telefon");
            }

            // Validate email
            if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
            {
                invalidFields.Add("Email");
            }

            // Validate username
            if (string.IsNullOrEmpty(username))
            {
                invalidFields.Add("Username");
            }

            // Validate password
            if (string.IsNullOrEmpty(password) || !IsValidPassword(password))
            {
                invalidFields.Add("Parola");
            }

            if (invalidFields.Count > 0)
            {
                if (invalidFields.Count == 1)
                {
                    MessageBox.Show($"Campul \"{invalidFields[0]}\" este invalid.", "Eroare de validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Există câmpuri invalide.", "Eroare de validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            try
            {
                conn.Open();

                // Check for existing email, phone number, or username
                var checkCmdEmail = new SqlCommand(@"SELECT COUNT(*) FROM Client WHERE Email = @Email", conn);
                var checkCmdPhone = new SqlCommand(@"SELECT COUNT(*) FROM Client WHERE NumarTelefon = @NumarTelefon", conn);
                var checkCmdUsername = new SqlCommand(@"SELECT COUNT(*) FROM Client WHERE username = @username", conn);

                checkCmdEmail.Parameters.AddWithValue("@Email", email);
                checkCmdPhone.Parameters.AddWithValue("@NumarTelefon", telefon);
                checkCmdUsername.Parameters.AddWithValue("@username", username);

                bool emailExists = (int)checkCmdEmail.ExecuteScalar() > 0;
                bool phoneExists = (int)checkCmdPhone.ExecuteScalar() > 0;
                bool usernameExists = (int)checkCmdUsername.ExecuteScalar() > 0;

                if (emailExists)
                {
                    MessageBox.Show("Email deja folosit.", "Eroare de validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (phoneExists)
                {
                    MessageBox.Show("Număr de telefon deja folosit.", "Eroare de validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (usernameExists)
                {
                    MessageBox.Show("Username deja folosit.", "Eroare de validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SqlCommand cmd = new SqlCommand(@"INSERT INTO Client (Nume, Prenume, NumarTelefon, Email, username, password, AdminID) VALUES (@Nume, @Prenume, @NumarTelefon, @Email, @username, @password, @AdminID)", conn);

                cmd.Parameters.Add("@Nume", SqlDbType.NVarChar).Value = nume;
                cmd.Parameters.Add("@Prenume", SqlDbType.NVarChar).Value = prenume;
                cmd.Parameters.Add("@NumarTelefon", SqlDbType.NVarChar).Value = telefon; // Change to NVarChar assuming phone number as text
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
                cmd.Parameters.Add("@AdminID", SqlDbType.Int).Value = 1;

                cmd.ExecuteNonQuery();

                MessageBox.Show("Client înregistrat cu succes", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Form2 form = new Form2();
                form.Show();
                this.Hide(); // Optionally hide the current form after registration
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                MessageBox.Show("Nu s-a putut înregistra: acest nume de utilizator este deja folosit. Vă rugăm să alegeți altul!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Nu s-a putut înregistra: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private bool IsValidName(string input)
        {
            return !string.IsNullOrEmpty(input) && char.IsUpper(input[0]) && input.Skip(1).All(char.IsLower) && !input.Any(char.IsDigit);
        }

        private bool IsValidPhoneNumber(string input)
        {
            return input.Length >= 8 && input.All(char.IsDigit);
        }

        private bool IsValidEmail(string input)
        {
            return Regex.IsMatch(input, @"^[a-zA-Z]+@(gmail\.com|yahoo\.com)$");
        }

        private bool IsValidPassword(string input)
        {
            return input.Length >= 8 &&
                   input.Any(char.IsUpper) &&
                   input.Any(char.IsDigit) &&
                   input.Any(ch => !char.IsLetterOrDigit(ch));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            // Afișează un mesaj informativ
            MessageBox.Show("Înainte să vă înregistrați, asigurați-vă că respectați următoarele indicații pentru evitarea anumitor erori:\n" +
                "1: Numele și prenumele sunt scrise cu majuscule, urmate de litere mici.\n" +
                "2: Numărul de telefon este format din MINIM 8 CIFRE.\n" +
                "3: Emailul este de forma popescuion@yahoo.com sau popescuion@gmail.com.\n" +
                "4: Parola conține o majusculă, o minusculă, o cifră, un caracter special și este formată din minim 8 caractere.\n" +
                "5: Numărul de telefon, emailul și username-ul sunt unice. În caz contrar, vă va apărea un mesaj de informare.\n" +
                "6: NU LĂSAȚI CÂMPURI NECOMPLETATE!!", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
