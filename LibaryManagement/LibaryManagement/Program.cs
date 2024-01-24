using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace LibaryManagement
{
    public class Kitap
    {
        public string Baslik { get; set; }
        public string Yazar { get; set; }
        public string ISBN { get; set; }
        public int KopyaSayisi { get; set; }
        public int OduncAlinanKopyalar { get; set; }

        // Yapıcı Metot
        public Kitap(string baslik, string yazar, string isbn, int kopyaSayisi)
        {
            Baslik = baslik;
            Yazar = yazar;
            ISBN = isbn;
            KopyaSayisi = kopyaSayisi;
            OduncAlinanKopyalar = 0;
        }
    }

    // Kütüphane Sınıfı
    public class Kutuphane
    {
        private List<Kitap> kitaplar;

        // Yapıcı Metot
        public Kutuphane()
        {
            kitaplar = new List<Kitap>();
        }

        // Kitap Ekleme Metodu
        public void KitapEkle(Kitap kitap)
        {
            kitaplar.Add(kitap);
            Console.WriteLine("Kitap başarıyla eklendi.");
        }

        // Tüm Kitapları Listeleme Metodu
        public void TumKitaplariListele()
        {
            Console.WriteLine("Kütüphanedeki Tüm Kitaplar:");
            foreach (var kitap in kitaplar)
            {
                Console.WriteLine($"Başlık: {kitap.Baslik}, Yazar: {kitap.Yazar}, ISBN: {kitap.ISBN}, Kopya Sayısı: {kitap.KopyaSayisi}, Ödünç Alınan Kopyalar: {kitap.OduncAlinanKopyalar}");
            }
        }

        // Kitap Ara Metodu
        public void KitapAra(string anahtar)
        {
            Console.WriteLine($"Arama Sonuçları ({anahtar}):");
            foreach (var kitap in kitaplar)
            {
                if (kitap.Baslik.Contains(anahtar) || kitap.Yazar.Contains(anahtar))
                {
                    Console.WriteLine($"Başlık: {kitap.Baslik}, Yazar: {kitap.Yazar}, ISBN: {kitap.ISBN}, Kopya Sayısı: {kitap.KopyaSayisi}, Ödünç Alınan Kopyalar: {kitap.OduncAlinanKopyalar}");
                }
            }
        }

        // Kitap Ödünç Alma Metodu
        public void KitapOduncAl(string isbn)
        {
            var kitap = kitaplar.Find(x => x.ISBN == isbn);

            if (kitap != null && kitap.KopyaSayisi > 0 && kitap.OduncAlinanKopyalar < kitap.KopyaSayisi)
            {
                kitap.OduncAlinanKopyalar++;
                Console.WriteLine("Kitap başarıyla ödünç alındı.");
            }
            else
            {
                Console.WriteLine("Kitap ödünç alınamadı. Stokta yeterli kopya bulunmamaktadır.");
            }
        }

        // Kitap İade Metodu
        public void KitapIadeEt(string isbn)
        {
            var kitap = kitaplar.Find(x => x.ISBN == isbn);

            if (kitap != null && kitap.OduncAlinanKopyalar > 0)
            {
                kitap.OduncAlinanKopyalar--;
                Console.WriteLine("Kitap başarıyla iade edildi.");
            }
            else
            {
                Console.WriteLine("Kitap iade edilemedi. Ödünç alınan kopya bulunmamaktadır.");
            }
        }

        // Süresi Geçmiş Kitapları Listeleme Metodu
        public void SuresiGecmisKitaplar()
        {
            Console.WriteLine("Süresi Geçmiş Kitaplar:");
            foreach (var kitap in kitaplar)
            {
                if (kitap.OduncAlinanKopyalar > 0)
                {
                    Console.WriteLine($"Başlık: {kitap.Baslik}, Yazar: {kitap.Yazar}, ISBN: {kitap.ISBN}, Kopya Sayısı: {kitap.KopyaSayisi}, Ödünç Alınan Kopyalar: {kitap.OduncAlinanKopyalar}");
                }
            }
        }
    }

    // Konsol Uygulaması
    class Program
    {
        static void Main()
        {
            Kutuphane kutuphane = new Kutuphane();

            while (true)
            {
                Console.WriteLine("\nKütüphane Yönetim Sistemi");
                Console.WriteLine("1. Kitap Ekle");
                Console.WriteLine("2. Tüm Kitapları Listele");
                Console.WriteLine("3. Kitap Ara");
                Console.WriteLine("4. Kitap Ödünç Al");
                Console.WriteLine("5. Kitap İade Et");
                Console.WriteLine("6. Süresi Geçmiş Kitapları Listele");
                Console.WriteLine("7. Çıkış");

                Console.Write("Seçiminizi yapınız: ");
                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        Console.Write("Kitap Başlığı: ");
                        string baslik = Console.ReadLine();
                        Console.Write("Yazar: ");
                        string yazar = Console.ReadLine();
                        Console.Write("ISBN: ");
                        string isbn = Console.ReadLine();
                        Console.Write("Kopya Sayısı: ");
                        int kopyaSayisi = int.Parse(Console.ReadLine());

                        Kitap yeniKitap = new Kitap(baslik, yazar, isbn, kopyaSayisi);
                        kutuphane.KitapEkle(yeniKitap);
                        break;

                    case "2":
                        kutuphane.TumKitaplariListele();
                        break;

                    case "3":
                        Console.Write("Arama Anahtarı (Başlık veya Yazar): ");
                        string anahtar = Console.ReadLine();
                        kutuphane.KitapAra(anahtar);
                        break;

                    case "4":
                        Console.Write("ISBN: ");
                        string oduncAlIsbn = Console.ReadLine();
                        kutuphane.KitapOduncAl(oduncAlIsbn);
                        break;

                    case "5":
                        Console.Write("ISBN: ");
                        string iadeIsbn = Console.ReadLine();
                        kutuphane.KitapIadeEt(iadeIsbn);
                        break;

                    case "6":
                        kutuphane.SuresiGecmisKitaplar();
                        break;

                    case "7":
                        Console.WriteLine("Çıkış yapılıyor...");
                        return;

                    default:
                        Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                        break;
                }
            }
        }
    }
}