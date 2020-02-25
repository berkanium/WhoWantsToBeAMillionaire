using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KimMilyonerOlmakIster
{
    public partial class OyunGiris : Form
    {
        public OyunGiris()
        {
            InitializeComponent();
        }
        void OyunGirisi() {
            Baslangic giris = new Baslangic();
            this.Hide();
            giris.ShowDialog();
        }
        private void tmrGiris_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value==progressBar1.Maximum)
            {
                tmrGiris.Enabled = false;
                OyunGirisi();
            }
            else
            {
                progressBar1.Value += 1;
            }
        }

        private void OyunGiris_Load(object sender, EventArgs e)
        {
            tmrGiris.Enabled = true;
            tmrGiris.Interval = 10;
            progressBar1.BackColor = Color.MediumBlue;
            progressBar1.ForeColor = Color.DarkBlue;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 250;
        }
    }
}
