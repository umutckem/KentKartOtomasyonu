using KentKartOtomasyonu.Dtos;
using KentKartOtomasyonu.Services;
using KentKartOtomasyonu.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KentKartOtomasyonu.Views;

public partial class KullaniciBilgileri : ContentPage
{
    private readonly IKullaniciServices _kullaniciServices;
    private ObservableCollection<Kullanici> _tumKullanicilar;
    private Kullanici _kullanici;

    public void setKullanici(Kullanici kullanici)
    {
        _kullanici = kullanici;
    }

    public KullaniciBilgileri()
    {
        _kullaniciServices = new KullaniciServices();
        InitializeComponent();
    }

    private async Task GetKullanicilar()
    {
        var kullanicilar = await _kullaniciServices.GetTumKullanicilar();
        _tumKullanicilar = new ObservableCollection<Kullanici>(kullanicilar);
        CollectionViewKullanici.ItemsSource = _tumKullanicilar;
    }

    private async void ContentPage_Loaded_1(object sender, EventArgs e)
    {
        await GetKullanicilar();
    }



    private void SearchBarKullanici_TextChanged(object sender, TextChangedEventArgs e)
    {
        var filtre = e.NewTextValue?.ToLower() ?? "";

        var filtrelenmisListe = _tumKullanicilar
            .Where(k => k.Ad.ToLower().Contains(filtre) || k.Soyad.ToLower().Contains(filtre))
            .ToList();

        CollectionViewKullanici.ItemsSource = filtrelenmisListe;
    }

    private async void CollectionViewKullanici_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            var secilenKullanici = e.CurrentSelection[0] as Kullanici;
            if (secilenKullanici is not null)
            {
                KullaniciBilgileriniGoruntule kullaniciBilgileriniGoruntule = new KullaniciBilgileriniGoruntule();
                kullaniciBilgileriniGoruntule.setKullanici(secilenKullanici);
                await Navigation.PushAsync(kullaniciBilgileriniGoruntule);
            }
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

        AdminPaneli adminPaneli = new AdminPaneli();
        adminPaneli.setKullanici(_kullanici);
        Application.Current.MainPage = new NavigationPage(adminPaneli);
    }
}
