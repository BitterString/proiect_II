using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace proiect
{
    public partial class Form1 : Form
    {
        private int clientId;
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-4E2J2L9;Initial Catalog=BankApp;Integrated Security=True");

        public Form1(int clientId)
        {
            InitializeComponent();
            this.clientId = clientId;
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            using (SqlConnection con = new SqlConnection(conn.ConnectionString))
            {
                con.Open();
                string query = "SELECT * FROM Cont WHERE ClientID = @ClientID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ClientID", this.clientId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void buttonTransfer_Click(object sender, EventArgs e)
        {
            decimal amount;
            if (!decimal.TryParse(textBoxAmount.Text, out amount))
            {
                MessageBox.Show("Vă rugăm să introduceți o sumă validă.");
                return;
            }

            int toAccountId;
            if (!int.TryParse(textBoxToAccount.Text, out toAccountId))
            {
                MessageBox.Show("Vă rugăm să introduceți un ID de cont valid.");
                return;
            }

            using (SqlConnection con = new SqlConnection(conn.ConnectionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    // Obține contul sursă
                    string getSourceAccountQuery = "SELECT ContID FROM Cont WHERE ClientID = @ClientID";
                    SqlCommand getSourceAccountCmd = new SqlCommand(getSourceAccountQuery, con, transaction);
                    getSourceAccountCmd.Parameters.AddWithValue("@ClientID", this.clientId);
                    int sourceAccountId = (int)getSourceAccountCmd.ExecuteScalar();

                    // Actualizați contul sursă
                    string updateFromAccount = "UPDATE Cont SET Suma = Suma - @Amount WHERE ContID = @FromAccountID";
                    SqlCommand cmdFrom = new SqlCommand(updateFromAccount, con, transaction);
                    cmdFrom.Parameters.AddWithValue("@Amount", amount);
                    cmdFrom.Parameters.AddWithValue("@FromAccountID", sourceAccountId);
                    cmdFrom.ExecuteNonQuery();

                    // Actualizați contul destinație
                    string updateToAccount = "UPDATE Cont SET Suma = Suma + @Amount WHERE ContID = @ToAccountID";
                    SqlCommand cmdTo = new SqlCommand(updateToAccount, con, transaction);
                    cmdTo.Parameters.AddWithValue("@Amount", amount);
                    cmdTo.Parameters.AddWithValue("@ToAccountID", toAccountId);
                    cmdTo.ExecuteNonQuery();

                    // Inserarea tranzacției
                    string insertTransaction = "INSERT INTO Tranzactie (FromContID, ToContID, Tip, Suma, Data) VALUES (@FromAccountID, @ToAccountID, 'Transfer', @Amount, @Data)";
                    SqlCommand cmdTransaction = new SqlCommand(insertTransaction, con, transaction);
                    cmdTransaction.Parameters.AddWithValue("@FromAccountID", sourceAccountId);
                    cmdTransaction.Parameters.AddWithValue("@ToAccountID", toAccountId);
                    cmdTransaction.Parameters.AddWithValue("@Amount", amount);
                    cmdTransaction.Parameters.AddWithValue("@Data", DateTime.Now);
                    cmdTransaction.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Transferul a fost efectuat cu succes.");
                    LoadAccounts();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("A apărut o eroare la efectuarea transferului: " + ex.Message);
                }
            }
        }

        private void buttonViewAccounts_Click(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
