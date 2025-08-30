using KentKartOtomasyonu.Dtos;
using KentKartOtomasyonu.Services;
using KentKartOtomasyonu.Views;
using System.Threading.Tasks;

namespace KentKartOtomasyonu.Views;

public partial class KullaniciBilgileriniGoruntule : ContentPage
{
	private readonly IKullaniciServices _kullaniciServices;
	Kullanici _kullanici;

	public void setKullanici(Kullanici kullanici)
	{
		_kullanici = kullanici;
		BindingContext = null;
		BindingContext = _kullanici;
	}
	public KullaniciBilgileriniGoruntule()
	{
		InitializeComponent();
		_kullaniciServices = new KullaniciServices();

    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        BakiyeDuzenle bakiyeDuzenle = new BakiyeDuzenle();
        bakiyeDuzenle.setKullanici(_kullanici);
        await Navigation.PushAsync(bakiyeDuzenle);
    }



    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        bool secim = await DisplayAlert("", "Bu Kullan�c�y� Silmek �stiyormusunuz", "Evet", "Hay�r");

		if( secim == true)

		{
			if (_kullanici is not null)
			{
				await _kullaniciServices.KullaniciSil(_kullanici.Id);
				await DisplayAlert("", "Kullanici Ba�ar�l� Bir �ekilde Silindi", "Tamam");
				KullaniciBilgileri kullaniciBilgileri = new KullaniciBilgileri();
                Application.Current.MainPage = new NavigationPage(kullaniciBilgileri);
            }
		}
		else
		{
			await DisplayAlert("", "��lem �ptal Edildi", "Tamam");
		}

    }
}