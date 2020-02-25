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
    public partial class Baslangic : Form
    {
        public Baslangic()
        {
            InitializeComponent();
        }

        private void HowToPlay_Click(object sender, EventArgs e)
        {
            panel1.Show();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void ApplicationExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Oyun oyn = new Oyun();
            this.Visible = false;
            oyn.ShowDialog();
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kim Milyoner Olmak İster Yarışmasına Hoşgeldiniz...BERKAN YILMAZ TARAFINDAN KODLANMIŞTIR.","Kim Milyoner Olmak ister Yarışması",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

    }
}
