using KentKartOtomasyonu.Dtos;
using KentKartOtomasyonu.Services;
using KentKartOtomasyonu.Views;

namespace KentKartOtomasyonu.Views;

public partial class BakiyeDuzenle : ContentPage
{
	private readonly IKullaniciServices _kullaniciServices;
	Kullanici _Kullanici;

	public void setKullanici(Kullanici kullanici)
	{
		_Kullanici = kullanici;

    }
	public BakiyeDuzenle()
	{
		InitializeComponent();
		_kullaniciServices = new KullaniciServices();


    }

    private async void Button_Clicked(object sender, EventArgs e)
    {	
		if(string.IsNullOrEmpty(adi.Text) || string.IsNullOrEmpty(soyadi.Text) || string.IsNullOrEmpty(yenibakiye.Text) )
		{
            await DisplayAlert("", "Bu Alanlar Boş Bırakılamaz", "Tamam");
        }	
		else
		{
			bool secim = await DisplayAlert("","Güncellemek İstediğinize Emin Misiniz ?","Evet","Hayır");
			if(secim == true) { 
			var guncellenecekKullanici = new KullaniciGuncelleDto{
				Ad = adi.Text,
				Soyad = soyadi.Text,
				Bakiye = float.Parse(yenibakiye.Text),
				Id = _Kullanici.Id,
				KentKartId = _Kullanici.KentKartId,
				Password = _Kullanici.Password,
				ProfilResmi = _Kullanici.ProfilResmi,
				TelefonNo = _Kullanici.TelefonNo,
			};
			await _kullaniciServices.KullaniciGuncelle(guncellenecekKullanici);
			await DisplayAlert("","Güncelleme İşlemi Tamam", "Tamam");
			KullaniciBilgileri kullaniciBilgileri = new KullaniciBilgileri();
            Application.Current.MainPage = new NavigationPage(kullaniciBilgileri);
            }
        }
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
		adi.Text = _Kullanici.Ad;
		soyadi.Text = _Kullanici.Soyad;
        var bakiye = Convert.ToString(_Kullanici.Bakiye);
		yenibakiye.Text = bakiye;
    }
}