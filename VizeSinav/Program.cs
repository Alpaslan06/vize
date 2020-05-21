using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VizeSinav
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Kitap Ekle : 1\nKitap Listesini görüntüle : 2\nUygulamayı kapat : 3\nLütfen kitap basım tarihini yıl-ay-gün şeklinde giriniz.(Ornek : 2020-06-05 vb)\n");
                    Console.Write("Hoşgeldiniz.Lütfen yapmak istediğiniz işlemin numarasını giriniz : ");                                 
                    int islemno = int.Parse(Console.ReadLine());//islem secebılmek ıcın girdi alındı
                    //koşullar 
                    if (islemno == 1)
                    {
                        Console.Write("Lütfen girmek istediğiniz kitap sayısını yazınız : ");
                        int ksayisi = int.Parse(Console.ReadLine());//sayı girisi

                        Kitap[] Kitaplar = new Kitap[ksayisi];//kitap türünde dizinin uzunlugu kullanıcının gırdıgı sayı
                        int i = 0;
                        do
                        {
                            Kitap kitap = new Kitap();//kitap nesnesi
                            Console.Write("Kitap Adı : ");//veriler alınıyor
                            kitap.KitapAdi = Console.ReadLine();
                            Console.Write("Yazar Adı : ");
                            kitap.Yazar = Console.ReadLine();
                            Console.Write("Basım Tarihi : ");
                            kitap.BasimTarihi = DateTime.Parse(Console.ReadLine());
                            if (2020 < kitap.BasimTarihi.Year)//tarih kontrol
                            {
                                Console.WriteLine("2020 den büyük olamaz. Lütfen tekrar giriniz.");
                                continue;//bu komuttan sonra  bir sonraki işlem için başa döner                              
                            }
                            Console.Write("Türü : ");
                            kitap.Tur = Console.ReadLine();

                            Kitaplar[i] = kitap;//diziye degerler metot aracılıgıyla atanıyor
                            i++;
                        } while (i < ksayisi);//do while ile ,for da dongu sayısını degıstıremedıgımız için  yanlıs gırılme durumunda 3 kitap yerine 2 kitap kaydolmasının onune gecmek ıstedım

                        Kitap.dosyayaekle(Kitaplar);
                        Console.WriteLine("Kitaplar başarıyle eklendi !!!");
                    }
                    else if (islemno == 2)
                    {//okuma ıslemlerı yapılacak
                        string dyolu = @"D:\metinbelgesi.txt";
                        string line;
                        StreamReader file = new StreamReader(dyolu);
                        while ((line = file.ReadLine()) != null)//boş değilse yazdır
                        {
                            Console.WriteLine(line);
                        }
                        file.Close();//dosya kapat
                    }
                    else
                    {
                        Console.WriteLine("Uygulama kapatılıyor...");
                        Environment.Exit(0);//uygulamayı kapatıyoruz*
                        break;
                    }
                }
            }//hata kontrolleri
            catch (FormatException)
            {
                Console.WriteLine("Lütfen sayı giriniz \t!");
            }
            catch (Exception)
            {
                Console.WriteLine("bilinmeyen bir hata olustu !");
            }
            Console.ReadLine();
        }
    }
    class Kitap
    {
        //alanlar propfull kulandım
        private string _yazari;
        public string Yazar
        {
            get { return _yazari; }
            set { _yazari = value; }
        }
        private string _turu;
        public string Tur
        {
            get { return _turu; }
            set { _turu = value; }
        }
        public DateTime BasimTarihi { get; set; }

        private string _kitapAdi;
        public string KitapAdi
        {
            get { return _kitapAdi; }
            set { _kitapAdi = value; }
        }
        public Kitap() { }//kısa yazılmıs constructor

        public Kitap(string kitapAdi, string yazari, string turu, DateTime tarih)
        {
            //metot overload,parametre değerleri atandı
            _kitapAdi = kitapAdi;
            _yazari = yazari;
            _turu = turu;
            BasimTarihi = tarih;
        }
        public static void dosyayaekle(Kitap[] kitaplar)//ekleme metodu
        {
            try
            {
                string dyolu = @"D:\metinbelgesi.txt";
                //İşlem yapacağımız dosyanın yolunu belirtiyoruz.Sureklı yazmaktansa degıskene bır kere aktarıp her yerde kullanmak daha kolay ve pratık geldı
                bool varmi = File.Exists(dyolu);
                FileStream fs = new FileStream(dyolu, FileMode.Append, FileAccess.Write);
                //Bir file stream nesnesi oluşturdm. 1.parametre dosya yolunu belirtir
                //2.parametre dosya varsa açılacağını yoksa oluşturulacağını belirtir,
                //3.parametre dosyaya erişimin veri yazmak için olacağını gösterir.
                StreamWriter sw = new StreamWriter(fs);
                //Yazma işlemi için bir StreamWriter nesnesi oluşturduk.
                if (varmi == false) sw.WriteLine("KİTAP ADI     KİTAP YAZARI     KİTAP TÜRÜ   BASIM YILI");
                //dosya var mı yok mu kontrol edecek ona göre de kitap adı kitap yazarı tarzında bir kolon oluşacak buradakı amac her eklenen kıtap ardından bu sekılde kolon oluşmasını önlemek
                foreach (var kitap in kitaplar)
                {
                    //verilerimizi yazdırıyoruz
                    sw.Write("\r\nKitap adı : " + kitap.KitapAdi);
                    sw.Write("\r\nKitabın yazarı : " + kitap.Yazar);
                    sw.Write("\r\nKitabın türü : " + kitap.Tur);
                    sw.Write("\r\nKitabın basım yılı : " + kitap.BasimTarihi.ToShortDateString() + "\n\n");//*
                }
                sw.Flush();
                //Veriyi tampon bölgeden dosyaya aktardık.
                sw.Close();
                fs.Close();
                //burada nesnelerimizi kapattık.
            }
            catch (Exception e)
            {
                Console.WriteLine("Dosya ekleme sırasında hata meydana geldi. " + e.Message);
                //hata meydana gelırse uyarı verecek e.message sayesinde ise hatanın ne oldugunu programcılar anlayabılır.
            }
        }
    }
}

