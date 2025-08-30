using KentKartOtomasyonu.Dtos;
using KentKartOtomasyonu.Services;
using KentKartOtomasyonu.Views;


namespace KentKartOtomasyonu.Views;

public partial class BakiyeYukle : ContentPage
{
	private readonly IKullaniciServices _kullaniciServices;
	private Kullanici _Kullanici;
    private int _selectedAmount = 0;
    public BakiyeYukle()
	{
		InitializeComponent();
		_kullaniciServices = new KullaniciServices();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		
		if (_selectedAmount != 0)
		{

			

			if(kartNumarasi.Text is not null )
			{
				if (kartNumarasi.Text.Length == 12) { 
					if(ayPicker.SelectedIndex != -1 && yilPicker.SelectedIndex != -1 ) {
						
							
						if(cvv.Text	 is not null) { 
								if (cvv.Text.Length == 3) {
									if (check.IsChecked == true) { 
										var yuklenmekIstenenBakiye = _selectedAmount;

										var kullaniciGuncelleDtos = new KullaniciGuncelleDto
										{
											Ad = _Kullanici.Ad,
											Soyad = _Kullanici.Soyad,
											Bakiye = yuklenmekIstenenBakiye + _Kullanici.Bakiye,
											ProfilResmi = _Kullanici.ProfilResmi,
											Id = _Kullanici.Id,
											KentKartId = _Kullanici.KentKartId,
											Password = _Kullanici.Password,
											TelefonNo =_Kullanici.TelefonNo,
							
										};
										await _kullaniciServices.KullaniciGuncelle(kullaniciGuncelleDtos);
										await DisplayAlert("BAŞARILI",$"Bakiyenize {yuklenmekIstenenBakiye} Tl başarılı şekilde yükleme yapılmıştır","Tamam");
									var kullanicilar = await _kullaniciServices.GetTumKullanicilar();
									var _kullanici = kullanicilar.FirstOrDefault(x => x.Id == kullaniciGuncelleDtos.Id);
									if(_kullanici is not null)
									{
                                        KullaniciArayuzu kullaniciArayuzu = new KullaniciArayuzu();
										kullaniciArayuzu.SetKullanici(_kullanici);
                                        Application.Current.MainPage = new NavigationPage(kullaniciArayuzu); 

                                    }

									}
									else
									{
										await DisplayAlert("HATA", "Sözleşmeyi kabul etmediniz ","Tamam");
									}

								}
                            else
                            {
                                await DisplayAlert("HATA", "CVV eksik girdiniz", "Tamam");
                            }
                        }
						else
						{
							await DisplayAlert("","CVV Alanını Doldurmadınız","Tamam");
						}
                        

				}
				else
				{
					await DisplayAlert("HATA", "Son kullanma tarihini girmediniz", "Tamam");
				}
                }
				else
				{
                    await DisplayAlert("HATA", "Kart numaranızı doğru giriniz 12 karakter olmak zorunda ", "Tamam");
                }
            }
			else
			{
				await DisplayAlert("HATA", "Kart Numaralı Alanı Doldurunuz.", "Tamam");
			}
		}
		else
		{
			await DisplayAlert("HATA", "Lütfen Seçim Yapınız...","Tamam");
		}
    }
	public void setKullanicilar(Kullanici kullanici)
	{
		_Kullanici = kullanici;
	}

    private async void Radiobutton1_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var radioButton = sender as RadioButton;
		if(radioButton is not null)
		{
		 _selectedAmount = int.Parse(radioButton.Content.ToString());
		}
		else
		{
            await DisplayAlert("", "Lütfen Seçim Yapınız...", "Tamam");
        }
    }
}