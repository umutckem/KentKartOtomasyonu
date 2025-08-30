using KentKartOtomasyonu.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KentKartOtomasyonu.Services
{
    public class UrlHelper
    {
        private static string BaseUrl = "https://localhost:7158" +
            "";
        public static string kullaniciUrl = $"{BaseUrl}/Kullanici";

    }

    public abstract class BaseServices
    {
        protected HttpClient _client;
        protected JsonSerializerOptions _serializerOptions;
        protected BaseServices()
        {
#if DEBUG && ANDROID
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            _client = new HttpClient(handler.GetPlatformMessageHandler());
#else
            _client = new HttpClient();
#endif

            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }
    }

    public class HttpsClientHandlerService
    {

        public HttpMessageHandler GetPlatformMessageHandler()
        {
#if ANDROID
            var handler = new Xamarin.Android.Net.AndroidMessageHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
#elif IOS
        var handler = new NSUrlSessionHandler
        {
            TrustOverrideForUrl = IsHttpsLocalhost
        };
        return handler;
#else
     throw new PlatformNotSupportedException("Only Android and iOS supported.");
#endif
        }

#if IOS
    public bool IsHttpsLocalhost(NSUrlSessionHandler sender, string url, Security.SecTrust trust)
    {
        return url.StartsWith("https://localhost");
    }
#endif
    }





    public class KullaniciServices : BaseServices, IKullaniciServices
    {
        public async Task<List<Kullanici>> GetTumKullanicilar()
        {
            Uri uri = new Uri(UrlHelper.kullaniciUrl);
            HttpResponseMessage response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var sonuc = JsonSerializer.Deserialize<List<Kullanici>>(content, _serializerOptions);
                return sonuc ?? new List<Kullanici>();
            }

            return new List<Kullanici>();
        }

        public async Task KullaniciEkle(KullaniciEkleDto kullanici)
        {
            Uri uri = new Uri(UrlHelper.kullaniciUrl);
            JsonContent content = JsonContent.Create(kullanici);
            HttpResponseMessage response = await _client.PostAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Kullanıcı eklenemedi.");
            }
        }

        public async Task KullaniciGuncelle(KullaniciGuncelleDto kullanici)
        {
            Uri uri = new Uri(UrlHelper.kullaniciUrl);
            JsonContent content = JsonContent.Create(kullanici);
            HttpResponseMessage response = await _client.PutAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Kullanıcı güncellenemedi.");
            }
        }

        public async Task KullaniciSil(int id)
        {
            Uri uri = new Uri($"{UrlHelper.kullaniciUrl}?id={id}");
            HttpResponseMessage response = await _client.DeleteAsync(uri); // Silme işlemi Get değil Delete olmalı

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Kullanıcı silinemedi.");
            }
        }

        public async Task<Kullanici> GetKullaniciById(int id)
        {
            Uri uri = new Uri($"{UrlHelper.kullaniciUrl}/{id}");
            HttpResponseMessage response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var kullanici = JsonSerializer.Deserialize<Kullanici>(content, _serializerOptions);
                return kullanici;
            }

            throw new Exception("Kullanıcı bulunamadı.");
        }
    }

}

