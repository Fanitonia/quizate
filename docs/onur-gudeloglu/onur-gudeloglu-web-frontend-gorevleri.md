# Onur Güdeloğlu'nun Web Frontend Görevleri
**Front-end Test Videosu:** [YouTube Videosu](https://www.youtube.com/watch?v=kcN27FTeIoA)

## 1. Kayıt Olma Sayfası
- **API Endpoint:** `POST /auth/register`
- **Görev:** Kullanıcı kayıt işlemi için web sayfası tasarımı ve implementasyonu
- **UI Bileşenleri:**
  - Responsive kayıt formu (desktop ve mobile uyumlu)
  - "Kayıt Ol" butonu
  - "Zaten hesabınız var mı? Giriş Yap" linki
  - Loading spinner (kayıt işlemi sırasında)
  - Form container (card veya centered layout)
- **Teknik Detaylar:**
  - Framework: React
  - Form library: React Hook Form
  - State management React Query (kayıt durumu, hata yönetimi), React Hook Form (form state)

## 2. Giriş Yapma Sayfası
- **API Endpoint:** `POST /auth/login`
- **Görev:** Kullanıcı giriş işlemi için web sayfası tasarımı ve implementasyonu
- **UI Bileşenleri:**
  - Responsive giriş formu (desktop ve mobile uyumlu)
  - "Giriş Yap" butonu
  - "Hesabınız yok mu? Kayıt Ol" linki
  - Loading spinner (giriş işlemi sırasında)
  - Form container (card veya centered layout)
- **Form Validasyonu:**
  - Zod kütüphanesi ile form validasyonu (email formatı, password min length)
- **Teknik Detaylar:**
  - Framework: React
  - Form library: React Hook Form
  - State management React Query (giriş durumu, hata yönetimi), React Hook Form (form state)

## 3. Quiz Oluşturma Sayfası
- **API Endpoint:** `POST /quizzes`
- **Görev:** Kullanıcıların yeni quiz oluşturabileceği sayfa tasarımı ve implementasyonu
- **UI Bileşenleri:**
  - Quiz başlığı input alanı
  - Quiz açıklaması textarea
  - Soru ekleme alanı (multiple choice)
  - Soru ve seçenekler için dinamik form alanları, drag-and-drop ile sıralama.
- **Teknik Detaylar:**
  - React Hook Form ile dinamik form yönetimi
  - Zod ile form validasyonu (başlık zorunlu, en az 1 soru, her soru en az 2 seçenek vb.)
  - dnd-kit ile sürükle bırak sıralama
  - State management React Query (quiz oluşturma durumu, hata yönetimi)

## 4. Quiz Sayfası
- **API Endpoint:** `GET /quizzes/{quizId}`
- **Görev:** Kullanıcıların quizleri görüntüleyebileceği ve katılabileceği sayfa tasarımı ve implementasyonu
- **UI Bileşenleri:**
  - Responsive quiz layout
  - Quiz başlığı ve açıklaması
  - Quiz detayları (soru sayısı, dili, konuları)
  - Quizi almak için "Quiz'e Başla" butonu

## 5. Quiz'i Alma Sayfası
- **API Endpoint:** `GET /quizzes/{quizId}/questions`
- **Görev:** Kullanıcıların quiz sorularını görüntüleyebileceği sayfa tasarımı ve implementasyonu
- **UI Bileşenleri:**
    - Quiz başlığı ve açıklaması
    - Soru listesi (soru metni, seçenekler)
    - Quiz'i Bitir butonu
