using KentKartOtomasyonu.Dtos;
using KentKartOtomasyonu.Services;
using KentKartOtomasyonu.Views;

namespace KentKartOtomasyonu.Views;

public partial class AdminPaneli : ContentPage
{
	private Kullanici _kullanici;
	public void setKullanici(Kullanici kullanici)
	{
		_kullanici = kullanici;
		BindingContext = null;
		BindingContext = _kullanici;
	}
	public AdminPaneli()
	{
		InitializeComponent();
	}

    private async void button1_Clicked_1(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100);
        await button.ScaleTo(1, 100);
        KullaniciBilgileri kullaniciBilgileri = new KullaniciBilgileri();
        kullaniciBilgileri.setKullanici(_kullanici);
        await Navigation.PushAsync(kullaniciBilgileri);
    }

    private async void button2_Clicked_1(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); // Kucultme efekti
        await button.ScaleTo(1, 100); // Eski haline getirme
        bool secim = await DisplayAlert("","Cikis Yapmak Istediginize Emin Misiniz ?","Evet","Hayir");
        if(secim == true) {

            AnaMenu anaMenu = new AnaMenu();
            Application.Current.MainPage = new NavigationPage(anaMenu);

        }

    }
}