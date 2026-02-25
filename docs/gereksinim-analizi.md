# Gereksinim Analizi

## Tüm Gereksinimler 

1. **Giriş Yapma**
   - **API Metodu:** `POST /auth/login`
   - **Açıklama:** Kullanıcı kullanıcı adı ve şifresiyle giriş yapar.

1. **Üye Olma**
   - **API Metodu:** `POST /auth/register`
   - **Açıklama:** Yeni kullanıcı bilgileri sisteme kaydedilir ve hesap oluşturulur.

1. **Kullanıcının Kendi Profiline Erişmesi**
   - **API Metodu:** `GET /users/me`
   - **Açıklama:** Kullanıcı kendi hesap bilgilerini ve profil detaylarını görüntüler.

1. **Kullanıcının Profilini Güncellemesi**
   - **API Metodu:** `PATCH /users/me`
   - **Açıklama:** Kullanıcı isim, fotoğraf veya diğer profil bilgilerini güncelleyebilir.

1. **Kullanıcının Kendi Hesabını Silmesi**
   - **API Metodu:** `DELETE /users/me`
   - **Açıklama:** Kullanıcı hesabını kalıcı olarak siler.

1. **Kullanıcının Kendi Quizlerini Listelemesi**
   - **API Metodu:** `GET /users/me/quizzes`
   - **Açıklama:** Kullanıcı tarafından oluşturulan quizler listelenir.

1. **Bir Kullanıcının Profiline Erişme**
   - **API Metodu:** `GET /users/{userId}`
   - **Açıklama:** Belirli bir kullanıcının genel profil bilgileri alınır.

1. **Bir Kullanıcıyı Silme (Sadece Admin)**
   - **API Metodu:** `DELETE /users/{userId}`
   - **Açıklama:** Admin yetkilerine sahip biri kullanıcı silebilir.

1. **Quiz Oluşturma**
   - **API Metodu:** `POST /quizzes`
   - **Açıklama:** Kullanıcı yeni bir quiz hazırlar ve sisteme ekler.

1. **Quizleri Listeleme**
   - **API Metodu:** `GET /quizzes`
   - **Açıklama:** Sistemdeki tüm quizler temel bilgileriyle listelenir.

1. **Quizleri Filtreleme**
   - **API Metodu:** `GET /quizzes?topic={topic}&keyword={keyword}`
   - **Açıklama:** Quizler konuya veya anahtar kelimeye göre aranır.

1. **Bir Quiz'e Erişme**
   - **API Metodu:** `GET /quizzes/{quizId}`
   - **Açıklama:** Seçilen quizin soruları ve detayları görüntülenir.

1. **Bir Quiz'i Silme**
   - **API Metodu:** `DELETE /quizzes/{quizId}`
   - **Açıklama:** Quiz sahibi veya admin tarafından quiz silinir.

1. **Bir Quiz'in Detaylarını Güncelleme**
   - **API Metodu:** `PATCH /quizzes/{quizId}`
   - **Açıklama:** Quiz başlığı, açıklama veya içerik bilgileri güncellenir.

1. **Quiz Çözüldükten Sonra Kaydını Tutma**
   - **API Metodu:** `POST /attempts/{quizId}`
   - **Açıklama:** Kullanıcının yaptığı çözüm girişimi ve sonuçlar kaydedilir.

1. **Bir Quiz'in Çözüm Girişimlerini Alma**
   - **API Metodu:** `GET /attempts/{quizId}`
   - **Açıklama:** Quiz için yapılan tüm çözüm girişimleri listelenir.

1. **Quiz Konu Başlıklarını (Topic) Alma**
   - **API Metodu:** `GET /topics`
   - **Açıklama:** Kullanılabilir tüm konu başlıkları listelenir.

1. **Quiz Konu Başlığı (Topic) Yaratma (Sadece Admin)**
   - **API Metodu:** `POST /topics`
   - **Açıklama:** Admin yeni bir konu başlığı ekler.

1. **Quiz Konu Başlığı (Topic) Silme (Sadece Admin)**
   - **API Metodu:** `DELETE /topics`
   - **Açıklama:** Admin bir konu başlığını kaldırır.

1. **Quiz Konu Başlığı (Topic) Güncelleme (Sadece Admin)**
   - **API Metodu:** `PUT /topics`
   - **Açıklama:** Admin konu başlığının adını veya detaylarını düzenler.

1. **Quiz Tiplerini Listeleme**
   - **API Metodu:** `GET /quizTypes`
   - **Açıklama:** Sistemdeki tüm quiz tipleri listelenir.

1. **Quiz Tipi Ekleme (Sadece Admin)**
   - **API Metodu:** `POST /quizTypes`
   - **Açıklama:** Admin yeni bir quiz tipi ekler.

1. **Quiz Tipi Güncelleme (Sadece Admin)**
   - **API Metodu:** `PUT /quizTypes/{quizTypeId}`
   - **Açıklama:** Admin mevcut olan bir quiz tipini günceller.

1. **Quiz Tipi Silme (Sadece Admin)**
   - **API Metodu:** `DELETE /quizTypes/{quizTypeId}`
   - **Açıklama:** Admin quiz tipini sistemden kaldırır.






# Gereksinim Dağılımları

1. [Onur Güdeloğlu'nun Gereksinimleri](onur-gudeloglu/onur-gudeloglu-gereksinimler.md)
2. [Hüseyin Kahraman'ın Gereksinimleri](huseyin-kahraman/huseyin-kahraman-gereksinimler.md)