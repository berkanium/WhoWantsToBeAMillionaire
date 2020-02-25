using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Media;

namespace KimMilyonerOlmakIster
{
    public partial class Oyun : Form
    {
        public Oyun()
        {
            InitializeComponent();
        }
        SoundPlayer ses = new SoundPlayer();
        void getMusic() {
           
            ses.SoundLocation = Application.StartupPath + "\\kim.wav";
            ses.Play();
        }
     
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb");
        Label[] lbl = new Label[11];
        PictureBox[] pc = new PictureBox[21];
        int saniye = 20, point_Shower = 1,curX, curY;
        bool sur,yanlisBilme = false;
        DataTable tablo = new DataTable();

        private void CreateLabel()
        {
           for (int i = 10; i > 0; i--)
            {
                lbl[i] = new Label();
                lbl[i].Left = resim_soruPaneli.Left + 15;
                lbl[i].Top = resim_soruPaneli.Top - 10 + i * 20 + i * 2;
                lbl[i].BackColor = Color.DarkOrchid;
                lbl[i].ForeColor = Color.White;
                lbl[i].Width = 50;
                lbl[i].Height = 15;
                lbl[i].Font = new Font(lbl[i].Font.Name, (lbl[i].Font.Size + 1.5f),
                lbl[i].Font.Style, lbl[i].Font.Unit); //transtlate

                if (i < 10 && i > 1)
                    lbl[i].Text = "  " + (i * 10000).ToString();
                else if (i == 1)
                    lbl[i].Text = "   1000";
                else if (i == 10)
                    lbl[i].Text = "1000000";
                panel2.Controls.Add(lbl[i]);
            }
            resim_soruPaneli.SendToBack();
        }
        private void CreatePictureBox()
        {
            int sayac = 438;
            for (int i = 1; i < 21; i++)
            {
                pc[i] = new PictureBox();
                pc[i].Left =sayac;
                pc[i].Top = 3;
                pc[i].Width = 22;
                pc[i].Height = 22;
                pc[i].Image = KimMilyonerOlmakIster.Properties.Resources.time_on;
                panel_zaman.Controls.Add(pc[i]);
                sayac -= 23;
            }
            
        }
        private void AutoAlign()
        {
            lbl_A.Left = (210 - lbl_A.Width) / 2;
            lbl_B.Left = (210 - lbl_B.Width) / 2;
            lbl_C.Left = (210 - lbl_C.Width) / 2;
            lbl_D.Left = (210 - lbl_D.Width) / 2;
            lbl_Question.Left = (443 - lbl_Question.Width) / 2;
        }
        private void SetRandom(int x,string y)
        {
            switch (x)//doğru
            {
                case 1: lbl_A.Text = tablo.Rows[0][y].ToString();
                break;
                case 2: lbl_B.Text = tablo.Rows[0][y].ToString();
                break;
                case 3: lbl_C.Text = tablo.Rows[0][y].ToString(); 
                break;
                case 4: lbl_D.Text = tablo.Rows[0][y].ToString();
                break;
            }
        }
        private void GetRandomQuestion()
        {
            Random rnd = new Random();

            OleDbCommand komut = new OleDbCommand("SELECT * FROM Table1 WHERE SoruNo=@SoruNo",baglanti);
            komut.Parameters.AddWithValue("@SoruNo", rnd.Next(1, 301));
            OleDbDataAdapter adap = new OleDbDataAdapter(komut);  
            tablo.Clear();
            adap.Fill(tablo);

            lbl_Question.Text = tablo.Rows[0]["Soru"].ToString();

            int a=0, b=0, c=0, d=0;
            
            while (a == b || a == c || a == d || b == c || b == d || c == d)
            {
                a = rnd.Next(1,5);
                b = rnd.Next(1, 5);
                c = rnd.Next(1, 5);
                d = rnd.Next(1, 5);
            }
            SetRandom(a,"DogruCevap");
            SetRandom(b, "YanlisCevap1");
            SetRandom(c, "YanlisCevap2");
            SetRandom(d, "YanlisCevap3");
        }
        private void Temizle(int x)
        {
            switch (x)
            {
                case 1: lbl_A.Text = "";
                break;
                case 2: lbl_B.Text = ""; 
                break;
                case 3: lbl_C.Text = ""; 
                break;
                case 4: lbl_D.Text = "";
                break;
            }
        }
        private void Oyun_Load(object sender, EventArgs e)
        {
            getMusic();
            CreateLabel();
            CreatePictureBox();
            lbl[1].Font = new Font("Showcard Gothic", lbl[1].Font.Size);
            GetRandomQuestion();
            AutoAlign();
        }
        private void firstLoad_Tick(object sender, EventArgs e)
        {
            pc[saniye].Image = KimMilyonerOlmakIster.Properties.Resources.time_off;
            saniye--;
            if (saniye == 0)
            {
                firstLoad.Stop();
                MessageBox.Show("Oyunu Kaybettiniz!");
                ses.Stop();
            }
        }
        private void DogruYanlis_Click(object sender, EventArgs e)
        {
            string deger;
            try
            {
                 deger = ((Label)sender).Text;
            }
            catch (Exception)
            {
                 deger = ((Panel)sender).Controls[0].Text.ToString();
            }
            if (deger==tablo.Rows[0]["DogruCevap"].ToString())
            {
                if (point_Shower != 10)
                {
                    lbl[point_Shower].Font = new Font("Microsoft Sans Serif", lbl[point_Shower].Font.Size);
                    point_Shower++;
                    lbl[point_Shower].Font = new Font("Showcard Gothic", lbl[1].Font.Size - 1);

                    GetRandomQuestion();
                    AutoAlign();
                    saniye = 20;

                    for (int i = 1; i < 21; i++)
                    {
                        pc[i].Image = KimMilyonerOlmakIster.Properties.Resources.time_on;
                    }
                }
                else
                {
                    
                    MessageBox.Show("Tebrikler 1 Milyon Lirayı Kazandınız!");
                    ses.Stop();
                }
	        }
            else
            {
                for (int i = 1; i < 11; i++)
                {
                    if (lbl[i].Font.Name=="Showcard Gothic")
                    {
                        firstLoad.Stop();
                        MessageBox.Show("Maalesef Oyunu Kaybettiniz. Kazandığınız Para: " + lbl[i].Text);
                        ses.Stop();
                        yanlisBilme = true;
                        Baslangic bsl = new Baslangic();
                        this.Hide();
                        bsl.ShowDialog();
                        this.Close();
                    }
                }
            }
        }
        private void btn_AltaAl_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; // (-) Sağ Üstteki düğmeye tıklandığı zaman Ekranı en alta alır
            ses.Stop();
        }
        private void lbl_Kapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Oyun_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (yanlisBilme == false)
            {
                DialogResult cikti = MessageBox.Show("Oyunu kapatmak istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (cikti == DialogResult.No)
                {
                    e.Cancel = true;
                    ses.Stop();
                }
            }
        }
        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            sur = false;
        }
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            sur = true;
            curX = Cursor.Position.X - this.Left;
            curY = Cursor.Position.Y - this.Top;
        }
        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (sur)
            {
                this.Left = Cursor.Position.X - curX;
                this.Top = Cursor.Position.Y - curY;
            }
        }
        private void close_1_2_Click(object sender, EventArgs e)
        {
            close_1_2.Visible = false;
            
            Random rnd = new Random();
            int rakam1 = rnd.Next(1, 5);
            int rakam2= rnd.Next(1,5);
            
            int aktif_rakam=0;

            if (lbl_A.Text==tablo.Rows[0]["DogruCevap"].ToString())
                aktif_rakam = 1;
            if (lbl_B.Text == tablo.Rows[0]["DogruCevap"].ToString())
                aktif_rakam = 2;
            if (lbl_C.Text == tablo.Rows[0]["DogruCevap"].ToString())
                aktif_rakam = 3;
            if (lbl_D.Text == tablo.Rows[0]["DogruCevap"].ToString())
                aktif_rakam = 4;
            
            while (rakam1 == aktif_rakam || rakam2 == aktif_rakam || rakam1==rakam2)
            {
                rakam1 = rnd.Next(1, 5);
                rakam2 = rnd.Next(1, 5);
            }

            Temizle(rakam1);
            Temizle(rakam2);
        }
        private void close_telephone_Click(object sender, EventArgs e)
        {
            close_telephone.Visible = false;
            MessageBox.Show(tablo.Rows[0]["TelefonHakkiYorumu"].ToString(),"          Telefon Görüşmesi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        int aktif_rakam = 0;
        private void close_graphic_Click(object sender, EventArgs e)
        {
            close_graphic.Visible = false;
            panel_graphic.Visible = true;

           
            int aktif_yuzde=0 ,pasif_yuzde1=0, pasif_yuzde2=0,pasif_yuzde3=0;

            if (lbl_A.Text == tablo.Rows[0]["DogruCevap"].ToString())
                aktif_rakam = 1;
            if (lbl_B.Text == tablo.Rows[0]["DogruCevap"].ToString())
                aktif_rakam = 2;
            if (lbl_C.Text == tablo.Rows[0]["DogruCevap"].ToString())
                aktif_rakam = 3;
            if (lbl_D.Text == tablo.Rows[0]["DogruCevap"].ToString())
                aktif_rakam = 4;

            Random i_o = new Random();
            if (i_o.Next(0, 100) == 99)
            {
                aktif_yuzde=i_o.Next(1, 26);//1,26  22
                pasif_yuzde1 = i_o.Next(1, aktif_yuzde);//1,22  20
                pasif_yuzde2 = i_o.Next(1, aktif_yuzde + pasif_yuzde1);//1,44   28
                pasif_yuzde3 = 100 - (aktif_yuzde + pasif_yuzde1 + pasif_yuzde2);//3
            }
            else
            {
                aktif_yuzde = i_o.Next(50, 100);//65
                pasif_yuzde1 = i_o.Next(1, 100-aktif_yuzde);//20
                pasif_yuzde2 = i_o.Next(1, 100-(aktif_yuzde + pasif_yuzde1));//10
                pasif_yuzde3 = 100 - (aktif_yuzde + pasif_yuzde1 + pasif_yuzde2);//5
            }
            this.chart1.Titles.Clear();
            this.chart1.Series.Clear();

            this.chart1.Series.Add(tablo.Rows[0]["DogruCevap"].ToString());
            this.chart1.Series[0].Points.AddXY(tablo.Rows[0]["DogruCevap"], aktif_yuzde.ToString());

            this.chart1.Series.Add(tablo.Rows[0]["YanlisCevap1"].ToString());
            this.chart1.Series[1].Points.AddXY(tablo.Rows[0]["YanlisCevap1"], pasif_yuzde1.ToString());//Burda o kişiye birde upload değeri eklememize yarıyor. Ben kb olarak aldığım değeri gb ye çevirmek için 2 kez 1024 e bölüyorum.
            
            this.chart1.Series.Add(tablo.Rows[0]["YanlisCevap2"].ToString());
            this.chart1.Series[2].Points.AddXY(tablo.Rows[0]["YanlisCevap2"], pasif_yuzde2.ToString());//Burda o kişiye birde upload değeri eklememize yarıyor. Ben kb olarak aldığım değeri gb ye çevirmek için 2 kez 1024 e bölüyorum.
               
            
            this.chart1.Series.Add(tablo.Rows[0]["YanlisCevap3"].ToString());
            this.chart1.Series[3].Points.AddXY(tablo.Rows[0]["YanlisCevap3"], pasif_yuzde3.ToString());//Burda o kişiye birde upload değeri eklememize yarıyor. Ben kb olarak aldığım değeri gb ye çevirmek için 2 kez 1024 e bölüyorum.

            chart1.Titles.Add("En Yüksek Oy Alan Şık Aşağıda Yazmaktadır."); 
        }
        private void label2_Click(object sender, EventArgs e)
        {
            panel_graphic.Visible = false;
        }

      

        
    }
}
