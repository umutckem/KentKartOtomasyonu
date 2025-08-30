using KentKartOtomasyonu.Dtos;
using KentKartOtomasyonu.Services;
using KentKartOtomasyonu.Views;

namespace KentKartOtomasyonu.Views;

public partial class KullaniciArayuzu : ContentPage
{
	private readonly IKullaniciServices _services;
	private Kullanici _Kullanici;
	public KullaniciArayuzu()
	{
		InitializeComponent();
        _services = new KullaniciServices();

	}

	public void SetKullanici(Kullanici kullanici)
	{
		_Kullanici = kullanici;
        BindingContext = null;
        BindingContext = _Kullanici;
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
		var Kullanicilar = await _services.GetTumKullanicilar();
		var Kullanici = Kullanicilar.FirstOrDefault(x => x.KentKartId == _Kullanici.KentKartId);
		moneybalance.Text = Kullanici.Bakiye.ToString();
		kenkartId.Text = Kullanici.KentKartId.ToString();
		kullaniciAdi.Text = Kullanici.Ad.ToString();
		kullaniciSoyadi.Text = Kullanici.Soyad.ToString();
		kayitTarihi.Text = Kullanici.KayitTarihi.ToString();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await buton1.FadeTo(0,140);
        await buton1.FadeTo(1, 140);
		SifreDegistir sifreDegistir = new SifreDegistir();
        sifreDegistir.setkulanici(_Kullanici);
		await Navigation.PushAsync(sifreDegistir);
    }

    private async void buton2_Clicked(object sender, EventArgs e)
    {
        await buton2.FadeTo(0, 140);
        await buton2.FadeTo(1, 140);
		BakiyeYukle bakiyeYukle = new BakiyeYukle();
        bakiyeYukle.setKullanicilar(_Kullanici);
		await Navigation.PushAsync(bakiyeYukle);

    }

    private async void buton3_Clicked(object sender, EventArgs e)
    {
        await buton3.FadeTo(0, 140);
        await buton3.FadeTo(1, 140);
        Transfer transfer = new Transfer();
        transfer.setKullanici(_Kullanici);
        await Navigation.PushAsync(transfer);


    }

    private async void buton4_Clicked(object sender, EventArgs e)
    {
        await buton4.FadeTo(0, 140);
        await buton4.FadeTo(1, 140);
        Hakkında hakkında = new Hakkında();
        await Navigation.PushAsync(hakkında);

    }

    private async void buton5_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100);
        await button.ScaleTo(1, 100);
        BilgilerimiGuncelle bilgilerimiGuncelle = new BilgilerimiGuncelle();
        bilgilerimiGuncelle.SetKullanici(_Kullanici);
        await Navigation.PushAsync(bilgilerimiGuncelle);
    }

    private async void buton6_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100);
        await button.ScaleTo(1, 100);
        QrKod qrKod = new QrKod();
        await Navigation.PushAsync(qrKod);
    }

    private async void buton7_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100);
        await button.ScaleTo(1, 100);
        bool secim = await DisplayAlert("","Cikis Yapmak Istediginize Emin misiniz ?","Evet","Hayir");
        if (secim == true) { 
        AnaMenu anaMenu = new AnaMenu();
        Application.Current.MainPage = new NavigationPage(anaMenu);
        }
    }
}