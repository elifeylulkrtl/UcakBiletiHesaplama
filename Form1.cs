using System;
using System.Windows.Forms;

namespace UcakBiletiHesaplama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnHesapla_Click_1(object sender, EventArgs e)
        {
            // --- KRİTER 6: GİRİŞ KONTROLLERİ ---
            if (string.IsNullOrWhiteSpace(txtAdSoyad.Text) ||
                string.IsNullOrWhiteSpace(txtYas.Text) ||
                string.IsNullOrWhiteSpace(txtMesafe.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtYas.Text, out int yas) || !double.TryParse(txtMesafe.Text, out double mesafe))
            {
                MessageBox.Show("Yaş ve Mesafe alanlarına sadece sayısal değerler girilmelidir!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (yas < 0 || mesafe <= 0)
            {
                MessageBox.Show("Yaş veya Mesafe sıfırdan küçük olamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbBiletSinifi.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen bir bilet sınıfı seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- HESAPLAMA MANTIĞI ---
            double birimFiyat = 2.0;
            double temelTutar = mesafe * birimFiyat;
            string sinifBilgisi = cmbBiletSinifi.SelectedItem.ToString();

            // Sınıf Farkı (Business %50 artış)
            if (sinifBilgisi == "Business")
            {
                temelTutar *= 1.5;
            }

            // Yaş İndirimi
            double indirimOrani = 0;
            if (yas < 12) indirimOrani = 0.50;
            else if (yas >= 65) indirimOrani = 0.30;

            double indirimTutari = temelTutar * indirimOrani;
            double toplamTutar = temelTutar - indirimTutari;

            // --- KRİTER 4 & FİŞ GÖRÜNÜMÜ ---
            string fisDetayi = $"--- UÇUŞ BİLETİ ÖZETİ ---\n" +
                              $"Yolcu      : {txtAdSoyad.Text}\n" +
                              $"Mesafe     : {mesafe} KM\n" +
                              $"Sınıf      : {sinifBilgisi}\n" +
                              $"------------------------\n" +
                              $"Ara Toplam : {temelTutar:C2}\n" +
                              $"İndirim    : -{indirimTutari:C2}\n" +
                              $"------------------------\n" +
                              $"ÖDENECEK   : {toplamTutar:C2}";

            lblSonuc.Text = fisDetayi;
        }

        private void btnTemizle_Click_1(object sender, EventArgs e)
        {
            txtAdSoyad.Clear();
            txtYas.Clear();
            txtMesafe.Clear();
            cmbBiletSinifi.SelectedIndex = -1;
            lblSonuc.Text = "Sonuç burada görüntülenecektir.";
        }

        private void btnYardim_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Uygulama, mesafe ve yaşa göre bilet fiyatı hesaplar.\n" +
                            "- KM Birim: 2 TL\n" +
                            "- Business: %50 Ek Fiyat\n" +
                            "- <12 Yaş: %50 İndirim\n" +
                            "- >=65 Yaş: %30 İndirim", "Yardım", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}