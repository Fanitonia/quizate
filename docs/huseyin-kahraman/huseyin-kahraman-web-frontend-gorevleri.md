# Hüseyin Kahraman'ın Web Frontend Görevleri
**Front-end Test Videosu:** [Video](https://youtu.be/0gRjOihQDx4)

## 1. Ana Sayfa ve Quiz Listesi
- **API Endpoint:** `GET /quizzes`
- **Görev:** Kullanıcıların mevcut quizleri görüntüleyebileceği ana sayfa tasarımı ve implementasyonu
- **UI Bileşenleri:**
  - Responsive quiz listesi (desktop ve mobile uyumlu)
  - Quiz başlığı ve açıklaması
  - Quiz detayları (soru sayısı, dili, konuları)
  - Loading spinner (quiz listesi yüklenirken)
- **Teknik Detaylar:**
  - Framework: React

## 2. Profil Sayfası
- **API Endpoint:** `GET /users/me`
- **Görev:** Kullanıcı profil bilgilerini görüntüleyebileceği sayfa tasarımı ve implementasyonu
- **UI Bileşenleri:**
  - Responsive profil sayfası (desktop ve mobile uyumlu)
  - Kullanıcı bilgileri (isim, email, profil fotoğrafı vb.)
  - "Hesabı Düzenle" butonu
  - Şifre değiştirme butonu
  - Loading spinner (profil bilgileri yüklenirken)
  - Kullanıcının oluşturduğu quizlerin listesi
- **Teknik Detaylar:**
  - Framework: React
  - State management React Query (profil durumu, hata yönetimi), React Hook Form (form state)

## 3. Footer
- **Görev:** Kullanıcıların site hakkında bilgi alabileceği ve sosyal medya bağlantılarına erişebileceği footer tasarımı ve implementasyonu
- **UI Bileşenleri:**
  - Responsive footer layout
  - Site hakkında bilgi
  - Bazı önemli linkler (Hakkımızda, İletişim, Gizlilik Politikası vb.) (şu anlık yok)
  - Sosyal medya bağlantıları (şu anlık yok)