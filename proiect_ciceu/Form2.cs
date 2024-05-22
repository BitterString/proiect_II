using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proiect_ciceu
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form=new Form3();
            form.Show();    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 frm=new Form4();
            frm.Show();
        }

      
    }
}
