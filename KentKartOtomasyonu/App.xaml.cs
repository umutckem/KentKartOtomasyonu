using KentKartOtomasyonu.Views;

namespace KentKartOtomasyonu
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new AnaMenu());
        }


    }
}
