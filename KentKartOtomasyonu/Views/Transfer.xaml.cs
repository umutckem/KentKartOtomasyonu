using KentKartOtomasyonu.Dtos;
using KentKartOtomasyonu.Services; 
using KentKartOtomasyonu.Views;

namespace KentKartOtomasyonu.Views;

public partial class Transfer : ContentPage
{
	private readonly IKullaniciServices _kullaniciServices;
	private Kullanici _Kullanici;
	public Transfer()
	{
		InitializeComponent();
		_kullaniciServices = new KullaniciServices();
	}
	public void setKullanici(Kullanici kullanici)
	{
		_Kullanici = kullanici;
	}

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
		var kullanicilar = await _kullaniciServices.GetTumKullanicilar();
		var kullanici = kullanicilar.FirstOrDefault(x => x.KentKartId == _Kullanici.KentKartId);
		if (kullanici != null)
		{
			kullaniciBakiyesi.Text = kullanici.Bakiye.ToString(); 
		}
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
		bool secenek = await DisplayAlert("TRANSFER İŞLEMİ","Göndermek istediğinize emin misiniz ?","Evet","Hayır");
		if(secenek == true) {
			if (girilenKentkartID.Text is not null ) { 
				var kullanicilar = await _kullaniciServices.GetTumKullanicilar();
				

                var kullanici = kullanicilar.FirstOrDefault(x => x.KentKartId == double.Parse(girilenKentkartID.Text));
				if (kullanici is not null && kullanici.Ad == ad.Text && kullanici.Soyad == soyad.Text)
				{
					if (gonderilmekIstenenMiktar.Text is not null) { 
						if(kullanici.KentKartId.ToString() != _Kullanici.KentKartId.ToString()) { 
							if (_Kullanici.Bakiye >= int.Parse(gonderilmekIstenenMiktar.Text))
							{


								var gonderenKullaniciGuncelleDto = new KullaniciGuncelleDto
								{
									Ad = _Kullanici.Ad,
									Soyad = _Kullanici.Soyad,
									Bakiye = _Kullanici.Bakiye - int.Parse(gonderilmekIstenenMiktar.Text),
									Id = _Kullanici.Id,
									KentKartId = _Kullanici.KentKartId,
									ProfilResmi = _Kullanici.ProfilResmi,
									TelefonNo = _Kullanici.TelefonNo,
									Password = _Kullanici.Password,
								};
								await _kullaniciServices.KullaniciGuncelle(gonderenKullaniciGuncelleDto);

								var aliciKullaniciGuncelleDto = new KullaniciGuncelleDto
								{
									Ad = kullanici.Ad,
									Soyad = kullanici.Soyad,
									Bakiye = kullanici.Bakiye + int.Parse(gonderilmekIstenenMiktar.Text),
									Id = kullanici.Id,
									KentKartId = kullanici.KentKartId,
									ProfilResmi = kullanici.ProfilResmi,
									Password = kullanici.Password,
									TelefonNo = kullanici.TelefonNo,
								};
								await _kullaniciServices.KullaniciGuncelle(aliciKullaniciGuncelleDto);

								await DisplayAlert("BAŞARI", $"Para işlemi {kullanici.Ad} {kullanici.Soyad} Başarılı şekilde Gönderilmiştir \n Güncel Bakiyeniz: {_Kullanici.Bakiye - int.Parse(gonderilmekIstenenMiktar.Text)} Tl", "Tamam");

                                var _Kullanicilar = await _kullaniciServices.GetTumKullanicilar();
                                var _kullanici = _Kullanicilar.FirstOrDefault(x => x.Id == gonderenKullaniciGuncelleDto.Id);
                                if (_Kullanici is not null)
								{
                                    var yeniSayfa = new KullaniciArayuzu();
                                    yeniSayfa.SetKullanici(_kullanici);
                                    Application.Current.MainPage = new NavigationPage(yeniSayfa);


                                }
                            }
							else
							{
								await DisplayAlert("", "Bakiyeniz yetersiz", "Tamam");
							}
                        }
						else
						{
							await DisplayAlert("","Kullanıcı Kendine Para Gönderemez","Tamam");
						}
                    }
					else
					{
						await DisplayAlert("","Gönderilmek istenen miktarı giriniz !!","Tamam");
					}
				}
					else
					{
						await DisplayAlert("","Kullanici Bulunamad�","Tamam");
					}
            }
			else
			{
				await DisplayAlert("","Alanlar Bo� B�rak�lamaz","Tamam");
			}
        }
		
		else
		{
			await DisplayAlert("","��lem iptal edildi","Tamam");
		}
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var kullanicilar = await _kullaniciServices.GetTumKullanicilar();
        var kullanici = kullanicilar.FirstOrDefault(x => x.KentKartId == _Kullanici.KentKartId);
        if (kullanici != null)
        {
            kullaniciBakiyesi.Text = kullanici.Bakiye.ToString();
        }
    }
}