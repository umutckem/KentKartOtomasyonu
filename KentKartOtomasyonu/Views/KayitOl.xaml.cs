using KentKartOtomasyonu.Dtos;
using KentKartOtomasyonu.Services;
using KentKartOtomasyonu.Views;

namespace KentKartOtomasyonu.Views;

public partial class KayitOl : ContentPage
{
	private readonly IKullaniciServices _kullaniciServices;
	public KayitOl()
	{
		InitializeComponent();
		_kullaniciServices = new KullaniciServices();
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
		await kayitolbutonu.FadeTo(0, 150);
        await kayitolbutonu.FadeTo(1, 150);


        if (telefonNumarasi.Text != null && ad.Text != null && soyad.Text != null && sifre.Text != null)
		{

			if (telefonNumarasi.Text.Length == 10)
			{
				if (sifre.Text.Length > 5)
				{
					var telefonNumarasiValue = double.Parse(telefonNumarasi.Text);
					var kullanicilar = await _kullaniciServices.GetTumKullanicilar();
					var kontrol = kullanicilar.FirstOrDefault(x => x.TelefonNo == telefonNumarasiValue);
					if (kontrol == null)
					{

						var kullanici = new KullaniciEkleDto
						{
							Ad = ad.Text,
							Soyad = soyad.Text,
							Yetki = "KULLANICI",
							Password = sifre.Text,
							TelefonNo = telefonNumarasiValue,
							Bakiye = 0,
							KayitTarihi = DateTime.Now,
							ProfilResmi = "https://www.umitakcakaya.com/storage/app/uploads/public/603/95e/fb4/60395efb43f73471679690.jpg",

                            KentKartId = GenerateRandomKentKartId()
						};
						await _kullaniciServices.KullaniciEkle(kullanici);
						AnaMenu anaMenu = new AnaMenu();
						bool secenek = await DisplayAlert("", "Kayit Islemi Basarili Sekilde Tamamlandi", "Tamam", "Bilgilerim");
						if (secenek == false)
						{
							var kullaniciTelefonNumarasi = telefonNumarasiValue;
							var yeniKullaniciListesi = await _kullaniciServices.GetTumKullanicilar();
							var ilkKisi = yeniKullaniciListesi.FirstOrDefault(x => x.TelefonNo == kullaniciTelefonNumarasi);
							await DisplayAlert("Bilgilerim", $" Kullanici Adi: {ad.Text} \n Kullanici Soyadi: {soyad.Text} \n Kullanici Telefon Numarasi: {telefonNumarasi.Text} \n Kullanici Kent-Kart Id: {ilkKisi.KentKartId} \n Kullanici Sifresi: {ilkKisi.Password} \n Kullanici Bakiyesi: {ilkKisi.Bakiye} \n Kullanici Kayit Tarihi: {ilkKisi.KayitTarihi}", "Tamam");
                            Application.Current.MainPage = new NavigationPage(anaMenu);
                        }
                        Application.Current.MainPage = new NavigationPage(anaMenu);
                    }
					else
					{
						await DisplayAlert("","Bu telefon numarasi zaten Kayitli","Tamam");
					}

				}
                else
                {
                   await DisplayAlert("", "Sifre uzunlugu 5'den uzun degil", "Tamam");
                }	
            }
            else
            {
               await DisplayAlert("", "Telefon Numarasi 10 haneden uzun veya kisa", "Tamam");
            }


		}
        else
        {
            DisplayAlert("Hata", "Lutfen tum alanlari doldurunuz.", "Tamam");
        }
    }
    private int GenerateRandomKentKartId()
    {
        Random random = new Random();
        return random.Next(100000, 999999); // 6 haneli rastgele bir sayi uretir
    }
}