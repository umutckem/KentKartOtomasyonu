
using KentKartOtomasyonu.Dtos;
using KentKartOtomasyonu.Services;
using KentKartOtomasyonu.Views;

namespace KentKartOtomasyonu.Views;

public partial class AnaMenu : ContentPage
{
    private readonly IKullaniciServices _kullaniciServices;
    public AnaMenu()
    {
        InitializeComponent();
        _kullaniciServices = new KullaniciServices();
    }


    private async void giris_Clicked(object sender, EventArgs e)
    {
        await giris.FadeTo(0,100);
        await giris.FadeTo(1,100);

        if (telefonNumarasi.Text != null && sifre.Text != null) { 

        double telefonNo = double.Parse(telefonNumarasi.Text);

        var kullanicilar = await _kullaniciServices.GetTumKullanicilar();
        var kullanici = kullanicilar.FirstOrDefault( x => x.TelefonNo == telefonNo && x.Password == sifre.Text);

        if (kullanici != null)
        {
                if(kullanici.Yetki == "KULLANICI")
                {
                    KullaniciArayuzu kullaniciArayuzu = new KullaniciArayuzu();
                    kullaniciArayuzu.SetKullanici(kullanici);
                    Application.Current.MainPage = new NavigationPage(kullaniciArayuzu);
                }
                else if(kullanici.Yetki == "PERSONEL")
                {
                    AdminPaneli adminPaneli = new AdminPaneli();
                    adminPaneli.setKullanici(kullanici);
                    Application.Current.MainPage = new NavigationPage(adminPaneli);


                }
                else
                {
                    await DisplayAlert("", "Yetkisiz Erisim", "Tamam");
                }
        }
        else {
            await DisplayAlert("", "Telefon Numarasi veya Sifre Hatali ", "Tamam");
        }
        }
        else
        {
            await DisplayAlert("", "Eksik Bilgi Girisi", "Tamam");
        }

    }

    private async void kayit_Clicked(object sender, EventArgs e)
    {
        await kayit.FadeTo(0,100);
        await kayit.FadeTo(1,100);

        KayitOl kayitOl = new KayitOl();
        await Navigation.PushAsync(kayitOl);
    }

    private async void sifreUnuttum_Clicked(object sender, EventArgs e)
    {
        await sifreUnuttum.FadeTo(0,140);
        await sifreUnuttum.FadeTo(1, 140);
        SifreUnuttum SifreUnuttum = new SifreUnuttum();
        await Navigation.PushAsync(SifreUnuttum);
    }

    private async void hakkinda_Clicked(object sender, EventArgs e)
    {
        await hakkinda.FadeTo(0,140);
        await hakkinda.FadeTo(1, 140);

        Hakkında hakkında = new Hakkında();
        await Navigation.PushAsync(hakkında);
    }

}
