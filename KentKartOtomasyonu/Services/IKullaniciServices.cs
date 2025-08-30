using KentKartOtomasyonu.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KentKartOtomasyonu.Services
{
    internal interface IKullaniciServices
    {
        Task<List<Kullanici>> GetTumKullanicilar();

        Task KullaniciEkle(KullaniciEkleDto kullanici);

        Task KullaniciGuncelle(KullaniciGuncelleDto kullanici);

        Task KullaniciSil(int id);

    }
}
