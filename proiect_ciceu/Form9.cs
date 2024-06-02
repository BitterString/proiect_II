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
    public partial class Form9 : Form

    {
        private SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0LALPAV\SQLEXPRESS;Initial Catalog=BankApp;persist security info=True;Integrated Security=SSPI;");
        private int clientID;
        private string numarContSelectat;
        private string valutaContExpeditor;
        private decimal sumaContExpeditor;
        private string numarContDestinatar;



        public Form9()
        {
            InitializeComponent();

        }

        private void Form9_Load(object sender, EventArgs e)
        {

            clientID = Form3.Salveaza.ClientId;
            LoadConturi();
        }


        private void LoadConturi()
        {
            try
            {
                conn.Open();

                // Interogare pentru a obține conturile clientului logat
                string query = "SELECT Numar FROM Cont WHERE ClientID = @clientID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@clientID", clientID);

                SqlDataReader reader = cmd.ExecuteReader();

                // Populare ComboBox cu numerele conturilor
                while (reader.Read())
                {
                    cmbConturi.Items.Add(reader["Numar"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading accounts: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }


        private void btnSelecteaza_Click(object sender, EventArgs e)
        {
            if (cmbConturi.SelectedIndex != -1)
            {
                numarContSelectat = cmbConturi.SelectedItem.ToString();
                MessageBox.Show($"Ai selectat contul cu numărul: {numarContSelectat}", "Cont selectat", MessageBoxButtons.OK, MessageBoxIcon.Information);

                try
                {
                    conn.Open();
                    // Interogare pentru a obține valuta și suma contului expeditor
                    string query = "SELECT Moneda, Suma FROM Cont WHERE Numar = @numarContSelectat";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@numarContSelectat", numarContSelectat);

                    SqlDataReader reader = cmd.ExecuteReader();

                    // Salvăm valuta și suma contului expeditor
                    if (reader.Read())
                    {
                        valutaContExpeditor = reader["Moneda"].ToString();
                        sumaContExpeditor = (decimal)reader["Suma"];
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error retrieving account currency and balance: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Vă rugăm să selectați un cont", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadConturiRamase()
        {
            try
            {
                conn.Open();

                // Interogare pentru a obține conturile care nu aparțin clientului logat
                string query = "SELECT Numar FROM Cont WHERE ClientID != @clientID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@clientID", Form3.Salveaza.ClientId);

                SqlDataReader reader = cmd.ExecuteReader();

                // Clear cmbTransferuri pentru a evita dublarea elementelor
                comtransferuri.Items.Clear();

                // Populare ComboBox cu numerele celorlalte conturi
                while (reader.Read())
                {
                    comtransferuri.Items.Add(reader["Numar"].ToString());
                }

                // Facem cmbTransferuri vizibil
                comtransferuri.Visible = true;

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading remaining accounts: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }
        private void textSuma_TextChanged(object sender, EventArgs e)
        {

        }

        private void comtransferuri_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnIncarca_Click(object sender, EventArgs e)
        {
            LoadConturiRamase();
        }

        private void btnseldes_Click(object sender, EventArgs e)
        {
            if (comtransferuri.SelectedIndex != -1)
            {
                numarContDestinatar = comtransferuri.SelectedItem.ToString();
                string valutaContDestinatar = ""; // Obținem valuta contului destinatar

                try
                {
                    conn.Open();

                    // Interogare pentru a obține valuta contului destinatar
                    string query = "SELECT Moneda FROM Cont WHERE Numar = @numarContDestinatar";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@numarContDestinatar", numarContDestinatar);

                    SqlDataReader reader = cmd.ExecuteReader();

                    // Salvăm valuta contului destinatar
                    if (reader.Read())
                    {
                        valutaContDestinatar = reader["Moneda"].ToString();
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error retrieving destination account currency: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }

                // Verificăm compatibilitatea valutelor
                if (valutaContExpeditor != valutaContDestinatar)
                {
                    MessageBox.Show("Selectează un cont cu aceeași valută ca și contul expeditor!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    textSuma.Enabled = true;
                    textSuma.Focus();
                }
            }
            else
            {
                MessageBox.Show("Te rugăm să selectezi un cont destinatar!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnTrimite_Click(object sender, EventArgs e)
        {
            // Verificăm dacă suma introdusă este validă și dacă contul expeditor are suficienți bani
            if (!string.IsNullOrEmpty(textSuma.Text) && decimal.TryParse(textSuma.Text, out decimal sumaTrimisa))
            {
                if (string.IsNullOrEmpty(numarContDestinatar))
                {
                    MessageBox.Show("Te rugăm să selectezi un cont destinatar!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificăm dacă suma introdusă este mai mică de 5 unități din valuta contului expeditor
                if (sumaTrimisa < 5)
                {
                    MessageBox.Show($"Introduceți o sumă de minim 5 {valutaContExpeditor}!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificăm dacă suma introdusă este mai mică sau egală cu suma contului expeditor
                if (sumaTrimisa <= sumaContExpeditor)
                {
                    try
                    {
                        conn.Open();
                        SqlTransaction transaction = conn.BeginTransaction();

                        // Comanda pentru a verifica și actualiza suma într-un singur pas
                        string queryUpdateSume = @"
                    UPDATE Cont 
                    SET Suma = 
                        CASE 
                            WHEN Numar = @numarContExpeditor THEN Suma - @sumaTrimisa 
                            WHEN Numar = @numarContDestinatar THEN Suma + @sumaTrimisa 
                        END
                    WHERE Numar IN (@numarContExpeditor, @numarContDestinatar)";

                        SqlCommand cmdUpdateSume = new SqlCommand(queryUpdateSume, conn, transaction);
                        cmdUpdateSume.Parameters.AddWithValue("@sumaTrimisa", sumaTrimisa);
                        cmdUpdateSume.Parameters.AddWithValue("@numarContExpeditor", numarContSelectat);
                        cmdUpdateSume.Parameters.AddWithValue("@numarContDestinatar", numarContDestinatar);

                        int rowsAffected = cmdUpdateSume.ExecuteNonQuery();

                        if (rowsAffected == 2) // ar trebui să fie 2 rânduri actualizate (expeditor și destinatar)
                        {
                            transaction.Commit();
                            MessageBox.Show("Tranzacție inițiată cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            transaction.Rollback();
                            MessageBox.Show("Fonduri insuficiente!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Eroare la efectuarea tranzacției: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    // Afișăm un mesaj de eroare dacă fondurile sunt insuficiente
                    MessageBox.Show("Fonduri insuficiente!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Afișăm un mesaj de eroare dacă suma introdusă nu este validă
                MessageBox.Show("Vă rugăm să introduceți o sumă validă!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
