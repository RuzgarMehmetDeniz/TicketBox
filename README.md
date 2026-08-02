<div align="center">

# 🎟️ TicketBox

### Türkiye'nin yeni nesil etkinlik ve bilet platformu

Konserden tiyatroya, festivalden spor müsabakalarına — binlerce etkinliği keşfet, saniyeler içinde bilet al, yapay zeka destekli asistanla anında yanıt bul.

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0-512BD4?logo=dotnet&logoColor=white)](https://learn.microsoft.com/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/sql-server)
[![MediatR](https://img.shields.io/badge/MediatR-CQRS-blueviolet)](https://github.com/jbogard/MediatR)
[![OpenAI](https://img.shields.io/badge/OpenAI-Tabili%20AI-412991?logo=openai&logoColor=white)](https://openai.com/)

[🔗 Repo](https://github.com/RuzgarMehmetDeniz/TicketBox)

</div>

---

## 📖 Hakkında

**TicketBox**, kullanıcıların etkinlik keşfedip bilet satın alabildiği; organizatörlerin etkinlik yönetebildiği; yöneticilerin ise sistemdeki tüm verileri (kullanıcılar, biletler, ödemeler, iadeler, kuponlar ve daha fazlasını) tek bir panelden yönetebildiği uçtan uca bir etkinlik/bilet satış platformudur.

Platform, klasik CRUD işlemlerinin ötesinde **doğal dilde soru-cevap yapabilen bir yapay zeka asistanı (Tabili AI)** ile hem kullanıcı hem yönetici tarafında akıllı bir deneyim sunar.

---

## ✨ Öne Çıkan Özellikler

### 🌐 Kullanıcı Tarafı

- **Etkinlik Keşfi** — Kategoriye, şehre, tarihe göre filtrelenebilir etkinlik listesi ve detay sayfaları
- **Etkinlik Takvimi** — Yaklaşan etkinlikleri kronolojik zaman çizelgesinde görüntüleme
- **Şehir Haritası** — Türkiye geneli interaktif harita üzerinden şehir bazlı etkinlik keşfi
- **Rezervasyon & Ödeme** — Adet seçimi, kupon uygulama, kart/havale ile ödeme akışı
- **Anlık E-Bilet** — PNR kodlu bilet, satın alma sonrası e-posta ile otomatik gönderim
- **Profil Paneli** — Biletlerim, favorilerim, yorumlarım tek ekranda; iptal/iade talebi gönderme
- **Yorum & Puanlama** — Katıldığı etkinliklere yıldızlı değerlendirme ve yorum bırakma
- **Sohbetlerim** — Yapay zeka asistanıyla geçmiş konuşmaların kaydı ve devamı
- **Blog & SSS** — Etkinlik dünyasından haberler, sık sorulan sorular
- **Çoklu Tema** — Midnight, Aurora, Purple, Ocean, Graphite, Light temaları arasında anlık geçiş

### 🛠️ Admin Panel

Sistemdeki **14 farklı varlık** için tam CRUD yönetimi:

| Modül | Açıklama |
|---|---|
| 📊 Dashboard | Özet istatistikler, aylık gelir trendi grafiği, tüm modüllerden son kayıtlar |
| 🎟️ Etkinlikler | Sayfalanmış etkinlik listesi, oluşturma/düzenleme/silme |
| 🔷 Kategoriler | Etkinlik kategorileri ve aktif/pasif durum yönetimi |
| 🖼️ Etkinlik Galerisi | Etkinlik görselleri yönetimi |
| ⭐ Favoriler | Kullanıcı favori kayıtları |
| 💬 Yorumlar & Değerlendirmeler | Etkinlik yorumları ve puanlamaları |
| 🎫 Biletler | Bilet durumu, PNR, e-posta gönderim takibi |
| 💳 Ödemeler | Ödeme geçmişi, durum ve referans no takibi |
| ↩️ İade Talepleri | İade onay/red iş akışı |
| 🏷️ Kuponlar | İndirim kuponları, kullanım limiti takibi |
| 👥 Kullanıcılar | Kayıtlı kullanıcı profilleri |
| 🔔 Bildirimler | Kullanıcı bildirimleri yönetimi |
| 💬 Canlı Destek Oturumları & Sohbet Mesajları | Destek/bot konuşma geçmişi |
| 📜 Sistem / Denetim Logları | Kullanıcı işlemlerinin tam denetim izi |
| ⚠️ Hata Sayfaları | Özel tasarlanmış 401 / 404 / 500 hata ekranları |

### 🤖 Tabili AI

Platform verileri üzerinde **gerçek zamanlı, doğal dilde soru-cevap** yapabilen entegre yapay zeka asistanı:

- "Bu ay toplam gelir ne kadar?"
- "En çok satan 5 etkinlik hangisi?"
- "Hangi kategori en çok gelir getiriyor?"
- "İade istatistikleri nasıl?"

Hem kullanıcı tarafında (etkinlik önerisi, bilet iptali gibi konularda destek) hem admin panelinde (iş zekası sorguları) çalışır.

---

## 🏗️ Mimari

Proje **Clean Architecture** prensiplerine göre 4 katmanlı olarak tasarlanmıştır:

```
TicketBox
├── Core
│   ├── TicketBox.Domain          → Entity'ler (saf iş nesneleri)
│   └── TicketBox.Application     → CQRS (MediatR), Validation, Specification,
│                                    Mapping, Repository arayüzleri, Servis arayüzleri
├── Infrastructure
│   └── TicketBox.Persistance     → DbContext, Migrations, Repository implementasyonları,
│                                    Email/OpenAI/Tabili servisleri
└── Presentation
    └── TicketBox.WebUI           → Controllers, Views, ViewModels, ViewComponents
```

### Katman Detayları

**`TicketBox.Domain`**
Bağımsız entity sınıfları: `AppUser`, `Event`, `Category`, `Ticket`, `Payment`, `Refund`, `Coupon`, `Review`, `Favorite`, `Notification`, `AuditLog`, `ChatSession`, `ChatMessage`, `EventGallery`.

**`TicketBox.Application`**
- **CQRS** — Her entity için ayrı `Commands`/`Queries` klasörleri (MediatR ile)
- **Specification Pattern** — Sorgu filtreleme/sıralama için `ISpecification`, `BaseSpecification`
- **FluentValidation** — Her entity için ayrı validator sınıfları
- **AutoMapper** — `GeneralMapping.cs` ile DTO/ViewModel dönüşümleri
- **Repository Interfaces** — `IGenericRepository`, `IUnitOfWork`
- **Servis Arayüzleri** — `IEmailService`, `IOpenAiChatService`, `ITabiliAnalyticsService`

**`TicketBox.Persistance`**
- EF Core `TicketContext` + Migrations
- `GenericRepository` + `UnitOfWork` implementasyonu
- `EmailService` (MailKit/MimeKit ile e-bilet gönderimi)
- `OpenAiChatService`, `TabiliAnalyticsService` (yapay zeka entegrasyonu)
- `SpecificationEvaluator` (specification → EF Core sorgu dönüşümü)

**`TicketBox.WebUI`**
- **Controllers** — Kullanıcı tarafı (`EventController`, `HomeController`, `DiscoverController`...) ve admin tarafı (`Admin*Controller` — 14 modül) ayrı ayrı
- **ViewModels** — `DashboardViewModel`, `EventDetailViewModel`, `ReservationViewModel` vb.
- **ViewComponents** — `AdminLayoutComponent`, `ChatComponent`, `DiscoverComponent`
- **Identity** tabanlı kimlik doğrulama

---

## 🧰 Teknoloji Yığını

| Katman | Teknolojiler |
|---|---|
| Backend | ASP.NET Core 8.0 MVC |
| Mimari | Clean Architecture, CQRS (MediatR), Specification Pattern, Repository + Unit of Work |
| Veritabanı | SQL Server, Entity Framework Core 8 |
| Kimlik Doğrulama | ASP.NET Core Identity |
| Doğrulama | FluentValidation |
| Nesne Eşleme | AutoMapper |
| E-posta | MailKit / MimeKit |
| Yapay Zeka | OpenAI API (Tabili AI) |
| Görselleştirme | Chart.js |
| CI/CD | GitHub Actions |

---

## 🖥️ Ekran Görüntüleri

### 🌐 Kullanıcı Arayüzü

#### Ana Sayfa — Hero
Platformun karşılama alanı; öne çıkan etkinlik kartı, canlı katılımcı bilgisi ve "Etkinlikleri Keşfet" çağrısı.
<img width="1920" height="1080" alt="Anasayfa1" src="https://github.com/user-attachments/assets/ad12d39f-7792-4581-8daa-92fc47d7553f" />

#### Ana Sayfa — Kategorilere Göre Keşif
Konser, tiyatro, spor, festival gibi kategorilere göre hızlı filtreleme kartları.
<img width="1898" height="716" alt="Anasayfa2" src="https://github.com/user-attachments/assets/663bf6fc-7335-44f8-87ef-74caa11e3a3a" />


#### Ana Sayfa — Bu Hafta Popüler Etkinlikler
Kategori bazlı sekmelerle filtrelenebilen, haftanın öne çıkan etkinlik listesi.
<img width="1920" height="1080" alt="Anasayfa3" src="https://github.com/user-attachments/assets/5a2f3b94-ed49-4e89-b857-3b8fa334313c" />

#### Ana Sayfa — Etkinlik Takvimi
Yaklaşan etkinliklerin tarih sırasına göre listelendiği kronolojik zaman çizelgesi.
<img width="1920" height="1080" alt="Anasayfa4" src="https://github.com/user-attachments/assets/f2531bbb-17f0-4129-bd12-b386a222d879" />

#### Ana Sayfa — Yaklaşan Etkinlikler
Doluluk oranı, katılımcı sayısı ve başlangıç fiyatıyla birlikte yatay kaydırmalı yaklaşan etkinlik kartları.
<img width="1920" height="1080" alt="Anasayfa5" src="https://github.com/user-attachments/assets/40a38e00-1af4-48f7-97a8-a0e9d696d63e" />

#### Ana Sayfa — En Popüler Etkinlikler
Topluluğun en çok ilgi gösterdiği etkinliklerin görsel ağırlıklı vitrin bölümü.
<img width="1920" height="1080" alt="Anasayfa6" src="https://github.com/user-attachments/assets/a7d57fea-5483-4560-b3a7-96d586571744" />

#### Ana Sayfa — Organizatörler
Platformdaki güvenilir/köklü organizatörlerin etkinlik sayılarıyla listelendiği tanıtım bölümü.
<img width="1897" height="687" alt="Anasayfa7" src="https://github.com/user-attachments/assets/8d17a0ca-f412-4901-9b18-df76fa7996a7" />


#### Ana Sayfa — Şehir Haritası
Türkiye haritası üzerinde şehir bazlı etkinlik sayılarının interaktif olarak gösterildiği bölüm.
<img width="1920" height="1080" alt="Anasayfa8" src="https://github.com/user-attachments/assets/61533026-1425-4992-b15e-c17cff0dfd0b" />

#### Ana Sayfa — Kullanıcı Yorumları
Gerçek kullanıcıların video/metin yorumlarının ve puanlarının sergilendiği sosyal kanıt bölümü.
<img width="1920" height="1080" alt="Anasayfa9" src="https://github.com/user-attachments/assets/07e99294-4398-4d9e-bfad-e0477374e838" />

#### Ana Sayfa — Blog & Sponsorlar
Etkinlik dünyasından haberler içeren blog kartları ve iş ortağı/sponsor logoları.
<img width="1920" height="1080" alt="Anasayfa10" src="https://github.com/user-attachments/assets/5b797731-80fa-4b62-86ff-cee127c7bf7b" />

#### Ana Sayfa — Platform İstatistikleri
Toplam etkinlik, satılan bilet, hizmet verilen şehir ve organizatör sayılarının özet şeridi.
<img width="1920" height="1080" alt="Anasayfa11" src="https://github.com/user-attachments/assets/452b0ab2-4730-47c7-b9c8-e7a399bb165c" />

#### Ana Sayfa — Sık Sorulan Sorular
Bilet teslimi, iade ve ödeme güvenliği gibi konularda açılır/kapanır SSS bölümü.
<img width="1899" height="785" alt="Anasayfa12" src="https://github.com/user-attachments/assets/89818357-0d8e-4449-8729-5fd403c11d94" />

#### Ana Sayfa — Bülten & Footer
E-posta bültenine abone olma alanı ve site geneli footer bağlantıları.
<img width="1920" height="1080" alt="Anasayfa13" src="https://github.com/user-attachments/assets/a520adf1-0051-40dc-aa79-8b8750d2fed7" />

#### Ana Sayfa — Canlı ChatBot
Kullanıcıya destek olmaya çalışan bir yapayzeka aracı.
<img width="476" height="579" alt="UIAİ" src="https://github.com/user-attachments/assets/0afb7455-8df2-4e69-ad93-5fcd84091686" />

#### Etkinlik Detay Sayfası
Etkinlik görseli, açıklaması, galerisi, doluluk oranı, fiyat bilgisi ve kullanıcı yorumlarının yer aldığı detay ekranı.
<img width="1920" height="2080" alt="Detail1" src="https://github.com/user-attachments/assets/11bead6f-1e6e-4608-8258-17daef5480e9" />

#### Rezervasyon & Ödeme
Bilet adedi, kupon seçimi ve kart/havale bilgileriyle ödeme tamamlama ekranı.
<img width="1895" height="1072" alt="Reservation1" src="https://github.com/user-attachments/assets/c76d5db4-7325-4f60-899b-afe15313d708" />

#### Rezervasyon Onayı
Satın alma sonrası oluşturulan biletlerin PNR kodlarıyla listelendiği onay ekranı.
<img width="1880" height="895" alt="ReservationConfirmation" src="https://github.com/user-attachments/assets/ea191b19-9aa0-496d-9b3e-77e6cd3d7ffe" />

#### E-Bilet E-postası
Satın alma sonrası kullanıcıya otomatik gönderilen, PNR kodlu e-bilet e-postası.
<img width="1028" height="397" alt="EmailTicket" src="https://github.com/user-attachments/assets/b0e07a17-c3c7-4877-a723-762a41cd64bd" />

#### Profil Sayfası
Kullanıcının biletlerini, favorilerini ve yorumlarını tek ekranda yönetebildiği profil paneli.
<img width="1920" height="1577" alt="Profile1" src="https://github.com/user-attachments/assets/16504664-0b58-4f3f-b92e-b1a004ec5781" />

#### Tabili AI — Sohbet (Kullanıcı Tarafı)
Kullanıcının biletiyle ilgili sorular sorabildiği, geçmiş konuşmaların listelendiği yapay zeka sohbet ekranı.
<img width="1920" height="1053" alt="Chat" src="https://github.com/user-attachments/assets/7d93cbb8-cf9a-4f84-8891-79fa7e78e1ed" />

---

### 🛠️ Admin Panel

#### Dashboard
Toplam kullanıcı/etkinlik/bilet/gelir özet kartları, aylık gelir trendi grafiği ve 11 modülden son kayıtların tek ekranda toplandığı yönetim paneli.
<img width="1564" height="3074" alt="Dashboard1" src="https://github.com/user-attachments/assets/57982064-111e-4f2a-8880-d1b72667ee1c" />

#### Etkinlikler
Sayfalanmış etkinlik listesi; kategori, tarih, kapasite, fiyat ve durum bilgileriyle oluşturma/düzenleme/silme işlemleri.
<img width="1920" height="1080" alt="AdminEvent" src="https://github.com/user-attachments/assets/e067b9bb-b25e-4360-ae25-93ee892806e5" />

#### Kategoriler
Etkinlik kategorilerinin açıklama ve aktif/pasif durumlarıyla yönetildiği liste.
<img width="1920" height="1080" alt="AdminCategory" src="https://github.com/user-attachments/assets/bf86593a-5bcb-4281-9713-ec32ac89fd57" />

#### Etkinlik Galerisi
Etkinliklere ait galeri görsellerinin önizleme ve URL bilgileriyle listelendiği yönetim ekranı.
<img width="1920" height="1080" alt="AdminEventGallery" src="https://github.com/user-attachments/assets/755ee439-5265-4217-a95e-bb93ad108647" />

#### Favoriler
Kullanıcıların favorilediği etkinliklerin kullanıcı/etkinlik eşleşmeleriyle listelendiği kayıt tablosu.
<img width="1920" height="1080" alt="AdminFavorite" src="https://github.com/user-attachments/assets/083facea-b594-4191-b27c-703edb17dbd9" />

#### Yorumlar & Değerlendirmeler
Etkinliklere bırakılan puan ve yorumların kullanıcı bazında görüntülendiği liste.
<img width="1920" height="1080" alt="AdminReview" src="https://github.com/user-attachments/assets/490b1cf3-5976-43fd-ab14-03d11beab5ad" />

#### Biletler
Satın alınan biletlerin PNR kodu, durum, e-posta gönderim bilgisi ve tutarıyla yönetildiği ekran.
<img width="1920" height="1080" alt="AdminTicket" src="https://github.com/user-attachments/assets/596398d3-37fb-4374-976f-11c343991e6a" />

#### Ödemeler
Sistemdeki tüm ödemelerin yöntem, durum, referans no ve tarih bilgileriyle takip edildiği liste.
<img width="1920" height="1080" alt="AdminPayment" src="https://github.com/user-attachments/assets/3e4eaf4f-2b24-43d6-beaf-9b33642f40aa" />

#### İade Talepleri
Kullanıcı iade taleplerinin onaylama/reddetme iş akışıyla yönetildiği ekran.
<img width="1920" height="1080" alt="AdminRefund" src="https://github.com/user-attachments/assets/159f7b41-4ca3-4df7-ad5e-d935b1a341f8" />

#### Kuponlar
İndirim kuponlarının oran, son kullanma tarihi, kullanım limiti ve aktif/pasif durumuyla yönetildiği liste.
<img width="1920" height="1080" alt="AdminCoupon" src="https://github.com/user-attachments/assets/ea4984c2-c09a-4941-8f3e-c73d584b954a" />

#### Kullanıcılar
Sisteme kayıtlı kullanıcıların e-posta, konum, yaş ve kayıt tarihi bilgileriyle listelendiği yönetim ekranı.
<img width="1920" height="1080" alt="AdminAppUser" src="https://github.com/user-attachments/assets/6ceb8b75-e420-44f3-826c-d5c112091ddd" />

#### Bildirimler
Kullanıcılara gönderilen bildirimlerin başlık, mesaj ve okunma durumuyla yönetildiği liste.
<img width="1920" height="1080" alt="ADminNotifcation" src="https://github.com/user-attachments/assets/4e7bcaac-0b79-4728-916e-1fa63ae58ac7" />

#### Canlı Destek Oturumları
Kullanıcı ile bot/destek arasındaki sohbet oturumlarının başlangıç tarihiyle listelendiği ekran.
<img width="1920" height="1080" alt="AdminChatSesion" src="https://github.com/user-attachments/assets/b5501fd7-9c53-4186-acac-7d212f1fa708" />

#### Sohbet Mesajları
Oturumlara ait tekil mesajların gönderen (kullanıcı/bot) bilgisiyle birlikte döküm halinde listelendiği ekran.
<img width="1920" height="1080" alt="AdminCheatMessage" src="https://github.com/user-attachments/assets/96e0ace9-3719-4557-80a6-9517767d6d76" />

#### Tabili AI (Admin Tarafı)
Yöneticinin platform verileri üzerinde doğal dilde iş zekası sorguları yapabildiği yapay zeka paneli.
<img width="1920" height="1080" alt="AdminTabiliAi" src="https://github.com/user-attachments/assets/471927a2-6190-4068-b2bb-704991c8f0e3" />

#### Sistem / Denetim Logları
Kullanıcı işlemlerinin (bilet satın alma, iptal, yorum ekleme vb.) tam denetim izinin tutulduğu log ekranı.
<img width="1920" height="1080" alt="AdminAuidlo" src="https://github.com/user-attachments/assets/a9dc651e-48f9-4b5b-ad9d-9cc790143793" />

---

### ⚠️ Hata Sayfaları

#### 404 — Sayfa Bulunamadı
Var olmayan bir adrese erişildiğinde gösterilen, platform tasarımıyla uyumlu özel 404 ekranı.
<img width="1308" height="660" alt="404" src="https://github.com/user-attachments/assets/9fd900d0-4828-4bcb-9e92-e8cedde76ebb" />

#### 401 — Yetkisiz Erişim
Yetkisi olmayan bir kullanıcı korumalı bir sayfaya erişmeye çalıştığında gösterilen özel 401 ekranı.
<img width="971" height="661" alt="401" src="https://github.com/user-attachments/assets/6c608fb9-3289-409c-bfdb-4fba5f711981" />

#### 500 — Sunucu Hatası
Beklenmeyen bir sunucu hatası oluştuğunda kullanıcıya gösterilen özel 500 ekranı.
<img width="932" height="611" alt="500" src="https://github.com/user-attachments/assets/f4e88575-7d8f-4b39-ae9f-83a671ea68b0" />

---


<div align="center">

**Türkiye'de ❤️ ile geliştirildi.**

</div>
