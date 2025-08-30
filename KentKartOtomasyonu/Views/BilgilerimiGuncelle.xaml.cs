using KentKartOtomasyonu.Dtos;
using KentKartOtomasyonu.Services;
using KentKartOtomasyonu.Views;

namespace KentKartOtomasyonu.Views;

public partial class BilgilerimiGuncelle : ContentPage
{
	private readonly IKullaniciServices _kullaniciServices;
	private Kullanici _Kullanici;
	public void SetKullanici(Kullanici kullanici) 
	{
		_Kullanici = kullanici;
		BindingContext = null;
		BindingContext = _Kullanici;
	}
	public BilgilerimiGuncelle()
	{
		InitializeComponent();
		_kullaniciServices = new KullaniciServices();

    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {

    }

    private async void guncelle_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100);
        await button.ScaleTo(1, 100);
        if(!string.IsNullOrWhiteSpace(profilResmi.Text) &&
            !string.IsNullOrWhiteSpace(tekrarProfilResmi.Text))
        {
        
            if (profilResmi.Text == tekrarProfilResmi.Text)
            {
                var kullaniciDto = new KullaniciGuncelleDto
                {
                    Ad = _Kullanici.Ad,
                    Soyad = _Kullanici.Soyad,
                    Bakiye = _Kullanici.Bakiye,
                    Id = _Kullanici.Id,
                    KentKartId = _Kullanici.KentKartId,
                    Password = _Kullanici.Password,
                    TelefonNo = _Kullanici.TelefonNo,
                    ProfilResmi = profilResmi.Text
                    
                    
                };
                await _kullaniciServices.KullaniciGuncelle(kullaniciDto);
                var _kullanicilar = await _kullaniciServices.GetTumKullanicilar();
                var _kullanici = _kullanicilar.FirstOrDefault(x => x.Id == _Kullanici.Id);
                if(_kullanici is not null)
                {
                    await DisplayAlert("","Resim Basarili Bir Sekilde Degismistir!","Tamam");
                    KullaniciArayuzu kullaniciArayuzu = new KullaniciArayuzu();
                    kullaniciArayuzu.SetKullanici(_kullanici);
                    await Navigation.PushAsync(kullaniciArayuzu);
                }
            }
            else
            {
                await DisplayAlert("", "Resimler Uyusmuyor!", "Tamam");
            }
        }
        else
        {
            await DisplayAlert("","Alanlar Bos Birakilamaz!","Tamam");
        }
    }
}