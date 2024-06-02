namespace proiect_ciceu
{
    partial class Form9
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbConturi = new System.Windows.Forms.ComboBox();
            this.btnSelecteaza = new System.Windows.Forms.Button();
            this.textSuma = new System.Windows.Forms.TextBox();
            this.comtransferuri = new System.Windows.Forms.ComboBox();
            this.btnIncarca = new System.Windows.Forms.Button();
            this.btnseldes = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTrimite = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbConturi
            // 
            this.cmbConturi.FormattingEnabled = true;
            this.cmbConturi.Location = new System.Drawing.Point(297, 12);
            this.cmbConturi.Name = "cmbConturi";
            this.cmbConturi.Size = new System.Drawing.Size(121, 21);
            this.cmbConturi.TabIndex = 0;
            this.cmbConturi.Text = "Conturi Expeditor";
            // 
            // btnSelecteaza
            // 
            this.btnSelecteaza.Location = new System.Drawing.Point(281, 39);
            this.btnSelecteaza.Name = "btnSelecteaza";
            this.btnSelecteaza.Size = new System.Drawing.Size(166, 23);
            this.btnSelecteaza.TabIndex = 1;
            this.btnSelecteaza.Text = "Selectare Cont Expeditor";
            this.btnSelecteaza.UseVisualStyleBackColor = true;
            this.btnSelecteaza.TextChanged += new System.EventHandler(this.btnSelecteaza_Click);
            this.btnSelecteaza.Click += new System.EventHandler(this.btnSelecteaza_Click);
            // 
            // textSuma
            // 
            this.textSuma.Location = new System.Drawing.Point(281, 208);
            this.textSuma.Name = "textSuma";
            this.textSuma.Size = new System.Drawing.Size(100, 20);
            this.textSuma.TabIndex = 2;
            this.textSuma.TextChanged += new System.EventHandler(this.textSuma_TextChanged);
            // 
            // comtransferuri
            // 
            this.comtransferuri.FormattingEnabled = true;
            this.comtransferuri.Location = new System.Drawing.Point(281, 129);
            this.comtransferuri.Name = "comtransferuri";
            this.comtransferuri.Size = new System.Drawing.Size(121, 21);
            this.comtransferuri.TabIndex = 3;
            this.comtransferuri.Text = "Conturi Destinatar";
            this.comtransferuri.SelectedIndexChanged += new System.EventHandler(this.comtransferuri_SelectedIndexChanged);
            // 
            // btnIncarca
            // 
            this.btnIncarca.Location = new System.Drawing.Point(264, 91);
            this.btnIncarca.Name = "btnIncarca";
            this.btnIncarca.Size = new System.Drawing.Size(173, 23);
            this.btnIncarca.TabIndex = 4;
            this.btnIncarca.Text = "Incărcare  Conturi Destinatar";
            this.btnIncarca.UseVisualStyleBackColor = true;
            this.btnIncarca.Click += new System.EventHandler(this.btnIncarca_Click);
            // 
            // btnseldes
            // 
            this.btnseldes.Location = new System.Drawing.Point(262, 169);
            this.btnseldes.Name = "btnseldes";
            this.btnseldes.Size = new System.Drawing.Size(140, 23);
            this.btnseldes.TabIndex = 5;
            this.btnseldes.Text = "Selectare Cont Destinatar";
            this.btnseldes.UseVisualStyleBackColor = true;
            this.btnseldes.TextChanged += new System.EventHandler(this.btnseldes_Click);
            this.btnseldes.Click += new System.EventHandler(this.btnseldes_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Introduceți suma dorită:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnTrimite
            // 
            this.btnTrimite.Location = new System.Drawing.Point(297, 246);
            this.btnTrimite.Name = "btnTrimite";
            this.btnTrimite.Size = new System.Drawing.Size(75, 23);
            this.btnTrimite.TabIndex = 7;
            this.btnTrimite.Text = "Trimite banii";
            this.btnTrimite.UseVisualStyleBackColor = true;
            this.btnTrimite.Click += new System.EventHandler(this.btnTrimite_Click);
            // 
            // Form9
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnTrimite);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnseldes);
            this.Controls.Add(this.btnIncarca);
            this.Controls.Add(this.comtransferuri);
            this.Controls.Add(this.textSuma);
            this.Controls.Add(this.btnSelecteaza);
            this.Controls.Add(this.cmbConturi);
            this.Name = "Form9";
            this.Text = "Form9";
            this.Load += new System.EventHandler(this.Form9_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbConturi;
        private System.Windows.Forms.Button btnSelecteaza;
        private System.Windows.Forms.TextBox textSuma;
        private System.Windows.Forms.ComboBox comtransferuri;
        private System.Windows.Forms.Button btnIncarca;
        private System.Windows.Forms.Button btnseldes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTrimite;
    }
}