using MauiApp1.Services;
using System.Runtime.Intrinsics.X86;

namespace MauiApp1.Views;

public partial class SifreUnuttum : ContentPage
{
	private readonly IKullaniciServices _kullaniciServices;
	public SifreUnuttum()
	{
		InitializeComponent();
        _kullaniciServices = new KullaniciServices();

    }

    private async void button1_Clicked_1(object sender, EventArgs e)
    {
        await button1.FadeTo(0, 140);
        await button1.FadeTo(1,140);

        var Ad = ad.Text;
        var Soyad = soyad.Text;
        var KenkartID = int.Parse(kenkartID.Text);
        double TelefonNumarasi = double.Parse(telefonNumarasi.Text);

        var kullnicilar = await _kullaniciServices.GetTumKullan�c�lar();
        var kullanici = kullnicilar.FirstOrDefault(x=>x.Soyad == Soyad &&  x.Ad == Ad && x.KentKartId == KenkartID && x.TelefonNo == TelefonNumarasi );
        if (kullanici != null)
        {
             bool sonuc = await DisplayAlert("", "Hesap Bulundu �ifre De�i�tirme Ekran�na Y�nlendiriliyorsunuz ...", "Tamam","��k��");
            if(sonuc == true) {
                SifreDegistir sifreDegistir = new SifreDegistir();
                sifreDegistir.setkulanici(kullanici);
                await Navigation.PushAsync(sifreDegistir);
            }
            else
            {
                await DisplayAlert("","��k�� i�lemi yap�l�yor ...","Tamam");
            }
        }
        else
        {
           await DisplayAlert("","Hesap Bilgileri Bulunamad�","Tamam");
        }
        


    }
}