
using KentKartOtomasyonu.Dtos;
using KentKartOtomasyonu.Services;
using KentKartOtomasyonu.Views;

namespace KentKartOtomasyonu.Views;

public partial class SifreDegistir : ContentPage
{
    private readonly IKullaniciServices _kullaniciServices;
    private Kullanici _kullanici;

    public void setkulanici(Kullanici kullanici)
    {
        _kullanici = kullanici;
    }
    public SifreDegistir()
    {
        InitializeComponent();
        _kullaniciServices = new KullaniciServices();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await buton.FadeTo(0, 140);
        await buton.FadeTo(1, 140);

        if (!string.IsNullOrEmpty(yenisifre.Text) && !string.IsNullOrEmpty(yenisifretekrar.Text))
        {
            if(yenisifre.Text == yenisifretekrar.Text)
            {
                if (yenisifre.Text.Length > 5) 
                {
                    var _kullanicilar = await _kullaniciServices.GetTumKullanicilar();
                    var kullanici = _kullanicilar.FirstOrDefault(x=>x.Id == _kullanici.Id);

                    if(kullanici is not null)
                    {
                        var guncelleDto = new KullaniciGuncelleDto
                        {
                            Ad = kullanici.Ad,
                            Soyad = kullanici.Soyad,
                            Bakiye = kullanici.Bakiye,
                            Id = kullanici.Id,
                            KentKartId = kullanici.KentKartId,
                            Password = yenisifre.Text,
                            ProfilResmi = kullanici.ProfilResmi,
                            TelefonNo = kullanici.TelefonNo,
                        };
                        await _kullaniciServices.KullaniciGuncelle(guncelleDto);

                        var persons = await _kullaniciServices.GetTumKullanicilar();
                        var person = persons.FirstOrDefault(x => x.Id == _kullanici.Id);
                        if(person is not null)
                        {
                            KullaniciArayuzu kullaniciArayuzu = new KullaniciArayuzu();
                            kullaniciArayuzu.SetKullanici(person);
                            Application.Current.MainPage = new NavigationPage(kullaniciArayuzu);
                        }
                    }
                }
                else
                {
                    await DisplayAlert("", "Girilen Sifre Uzunlugu 6 Karakterden Uzun Degil", "Tamam");
                }
            }
            else
            {
                await DisplayAlert("", "Girilen Sifreler Ayni Degil!", "Tamam");
            }
        }
        else
        {
            await DisplayAlert("","Bu Alanlar Bos Birakilamaz","Tamam");
        }

        
    }

}