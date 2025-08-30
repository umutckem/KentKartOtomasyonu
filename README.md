# KentKart Otomasyonu

KentKart Otomasyonu, .NET MAUI framework kullanılarak geliştirilmiş çok platformlu bir mobil uygulamadır. Bu uygulama, kullanıcıların KentKart işlemlerini kolayca yönetmelerini sağlar.

## 🚀 Özellikler

### Kullanıcı İşlemleri
- **Kullanıcı Kaydı**: Yeni kullanıcı hesabı oluşturma
- **Giriş Yapma**: Telefon numarası ve şifre ile giriş
- **Profil Yönetimi**: Kullanıcı bilgilerini görüntüleme ve güncelleme
- **Şifre İşlemleri**: Şifre değiştirme ve şifre unuttum

### KentKart İşlemleri
- **Bakiye Yönetimi**: Bakiye yükleme ve düzenleme
- **Transfer İşlemleri**: Kartlar arası para transferi
- **QR Kod**: QR kod ile hızlı işlem yapma
- **Bakiye Sorgulama**: Güncel bakiye bilgisi

### Admin Paneli
- **Kullanıcı Yönetimi**: Tüm kullanıcıları görüntüleme
- **Kullanıcı Düzenleme**: Kullanıcı bilgilerini güncelleme
- **Kullanıcı Silme**: Kullanıcı hesabı silme

## 🛠️ Teknolojiler

- **.NET 8.0**: En güncel .NET sürümü
- **.NET MAUI**: Çok platformlu uygulama geliştirme framework'ü
- **C#**: Programlama dili
- **XAML**: Kullanıcı arayüzü tanımlama

## 📱 Desteklenen Platformlar

- **Android**: API Level 21+ (Android 5.0+)
- **iOS**: iOS 11.0+
- **macOS**: macOS 13.1+ (Catalina+)
- **Windows**: Windows 10.0.17763.0+

## 🏗️ Proje Yapısı

```
MauiApp1/
├── Dtos/                    # Veri transfer nesneleri
│   ├── Kullanici.cs
│   ├── KullaniciEkleDto.cs
│   └── KullaniciGüncelleDto.cs
├── Services/                # İş mantığı servisleri
│   ├── IKullaniciServices.cs
│   └── KullaniciServices.cs
├── Views/                   # Kullanıcı arayüzü sayfaları
│   ├── AnaMenu.xaml        # Ana giriş sayfası
│   ├── AdminPaneli.xaml    # Admin kontrol paneli
│   ├── KullaniciArayuzu.xaml # Kullanıcı ana sayfası
│   ├── BakiyeYukle.xaml    # Bakiye yükleme
│   ├── Transfer.xaml       # Transfer işlemleri
│   └── ...                 # Diğer sayfalar
├── Resources/               # Uygulama kaynakları
│   ├── Images/             # Görseller
│   ├── Fonts/              # Fontlar
│   └── AppIcon/            # Uygulama ikonu
└── Platforms/               # Platform özel kodlar
    ├── Android/
    ├── iOS/
    ├── Windows/
    └── MacCatalyst/
```

## 🔧 Kurulum

### Gereksinimler
- Visual Studio 2022 17.0+
- .NET 8.0 SDK
- MAUI workload

### Kurulum Adımları
1. Projeyi klonlayın:
   ```bash
   git clone [repository-url]
   cd KentKartOtomasyonu
   ```

2. Visual Studio'da `KentKartOtomasyonu.sln` dosyasını açın

3. NuGet paketlerini restore edin

4. Projeyi derleyin ve çalıştırın

## 📋 Kullanım

### Kullanıcı Girişi
1. Ana menüde telefon numaranızı girin
2. Şifrenizi girin
3. "Giriş Yap" butonuna tıklayın

### Yeni Kullanıcı Kaydı
1. Ana menüde "Kayıt Ol" butonuna tıklayın
2. Gerekli bilgileri doldurun
3. Kayıt işlemini tamamlayın

### Bakiye İşlemleri
1. Kullanıcı arayüzünde "Bakiye Yükle" seçeneğini seçin
2. Yüklenecek miktarı girin
3. İşlemi onaylayın

## 👨‍💻 Geliştirici

**Umutcan Kemahlı** - Beta 0.01

## 📄 Lisans

Bu proje özel kullanım için geliştirilmiştir.

## 🤝 Katkıda Bulunma

1. Projeyi fork edin
2. Feature branch oluşturun (`git checkout -b feature/AmazingFeature`)
3. Değişikliklerinizi commit edin (`git commit -m 'Add some AmazingFeature'`)
4. Branch'inizi push edin (`git push origin feature/AmazingFeature`)
5. Pull Request oluşturun

## 📞 İletişim

Proje hakkında sorularınız için lütfen iletişime geçin.

---

**Not**: Bu uygulama beta aşamasındadır ve geliştirme sürecinde olabilir.