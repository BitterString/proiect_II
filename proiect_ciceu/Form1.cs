using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Json;

namespace proiect_ciceu
{
    public partial class Form1 : Form
    { protected long suma;
        int durata;
        string peluna;
        credit var = new credit();
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bine ati venit in aplicatia oferita de Cluj Bank care va ajuta sa simulati un credit" +
                " si sa convertiti valute simplu si rapid."+"\n"+
                "Instructiuni: Introduceti valorile cerute in campurile verzi."
                +" Dupa ce ati umplut toate campurile necesare apasati, in functie de calculul dorit, pe butoanele alaturate campurilor."
                +" ATENTIE!: Toate campurile trebuie sa contina DOAR valori numerice, iar optional la dobanda se poate folosi si virgula (ex: 7.65)."
                +" Campul <<alta moneda>> va fi completat cu prescurtarea valutei dorite (ex: JPY, CAD), cu litere mari. }n caz contrar, aplicatia va afisa eroare.");
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
             suma = (long)Convert.ToDouble(textBox1.Text);
             durata = Convert.ToUInt16(textBox2.Text);
            float procent = (float)Convert.ToDouble(textBox3.Text);
           
            var.Dobanda = procent;
            var.Durata = durata;
            var.Suma = suma;
           // MessageBox.Show("procent" + var.Dobanda +"durata"+ var.Durata+ "suma " + var.Suma);

            textBox4.Text = Convert.ToString( var.Luna() );
            peluna = var.Luna();
            textBox5.Text = Convert.ToString(var.Total());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox4.Text =
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Euro val = new Euro(suma);

            MessageBox.Show("suma dorita in EURO: " + val.Conve() + "\n"+ "rata lunara in EURO: " + (int)((Convert.ToDouble(peluna) / 4.75)*100)/100.0);
            
            
           
        }

        private void uSDToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void eURToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float a = float.Parse (textBox6.Text);
            Euro eur = new Euro(a);
            textBox7.Text = eur.Conve()+" EURO";
        }

        private void gBPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            long a = Convert.ToInt16(textBox6.Text);
            Euro gbp = new Euro(a);
            textBox7.Text = gbp.GbpConve() + " GBP";
            
        }

        private void uSDToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            long a = Convert.ToInt16(textBox6.Text);
            Euro usd = new Euro(a);
            textBox7.Text = usd.UsdConve() + " USD";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox6.Text = "";
            textBox7.Text = "";
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged_1(object sender, EventArgs e)
        {
            float x = float.Parse(textBox6.Text);
            string n = textBox8.Text;
            string info = new WebClient().DownloadString("https://free.currconv.com/api/v7/convert?q=" + n 
                + "_RON&compact=ultra&apiKey=02109d5b9f21cf341d8b");
                                                         
            JsonValue jsonValue = System.Json.JsonValue.Parse(info);
            float d = 0;
            if (jsonValue.ContainsKey(n + "_RON"))
            {
                d = jsonValue[n + "_RON"];
                textBox7.Text = Convert.ToString((int)(x / d * 100) / 100.0);
            }
            else textBox7.Text = "valuta inexistenta";  
        }
    }
}
