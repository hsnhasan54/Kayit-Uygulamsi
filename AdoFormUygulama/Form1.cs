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


namespace AdoFormUygulama
{

    public partial class Form1 : Form
    {
        SqlConnection baglantı = new SqlConnection(@"Data Source=DESKTOP-7FIO8MJ\SQLEXPRESS;Initial Catalog=Adonet;User=sa;password=1");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = listele();
        }

        public List<musteri> listele()
        {
            baglantı.Open();
            List<musteri> liste = new List<musteri>();
            SqlCommand cmd = new SqlCommand("Select * from musteri", baglantı);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                musteri musteri = new musteri();

                musteri.id = int.Parse(reader["id"].ToString());
                musteri.ad = reader["ad"].ToString();
                musteri.soyad = reader["soyad"].ToString();
                musteri.mail = reader["mail"].ToString();
                musteri.telefon = reader["telefon"].ToString();
                liste.Add(musteri);
            }
            baglantı.Close();
            return liste;
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {

            int a = ekle();
            if (a == 1)
            {
                MessageBox.Show("Kayıt Eklendi..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = listele();
            }
            else { MessageBox.Show("Kayıt Eklenemedi", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); }


        }
        public int ekle()
        {

            baglantı.Open();


            SqlCommand cm = new SqlCommand("insert into musteri (ad,soyad,mail,telefon) values(@ad,@soyad,@mail,@telefon)",baglantı);

            cm.Parameters.AddWithValue("@ad", adtext.Text);
            cm.Parameters.AddWithValue("@soyad", SoyadTxt.Text);
            cm.Parameters.AddWithValue("@mail", TxtMail.Text);
            cm.Parameters.AddWithValue("@telefon", maskedTextBox1.Text);
            cm.ExecuteNonQuery();

            baglantı.Close();

            int sayac = 1;
            if (sayac == 1)
            {
                return 1;
            }
            else { return 0; }


        }

        private void BtnGuncelleme_Click(object sender, EventArgs e)
        {
            int deger = gunceglleme();

            if (deger == 1)
            {
                MessageBox.Show("Kayıt Güncellendi");

                dataGridView1.DataSource = listele();
            }
            else
            {
                MessageBox.Show("Kayıt Gün");
            }
        }
        public int gunceglleme()

        {
            baglantı.Open();
            SqlCommand cmd = new SqlCommand("update musteri set mail=@mail where id=@id", baglantı);

            cmd.Parameters.AddWithValue("@mail", TxtMail.Text);
            cmd.Parameters.AddWithValue("@id", int.Parse(IdText.Text));

            int deger = cmd.ExecuteNonQuery();
            baglantı.Close();
            if (deger == 1)
            {
                return deger;
            }
            else
            {
                return 0;
            }

        }

        private void Btn_Sil_Click(object sender, EventArgs e)
        {
            int sonuc = sil();
            if (sonuc == 1)
            {
                MessageBox.Show("Kayıt Silindi");

                dataGridView1.DataSource = listele();
            }
            else
            {
                MessageBox.Show("Kayıt Silinemedi");
            }
        }

        public int sil()
        {
            baglantı.Open();
            SqlCommand cmdi = new SqlCommand("delete from musteri where id=@id",baglantı);

            cmdi.Parameters.AddWithValue("@id", int.Parse(IdText.Text));
            int deger = cmdi.ExecuteNonQuery();
            baglantı.Close();
            if (1 == deger)
            {
                return deger;
            }
            else { return 0; }
            


        }
    }
}
