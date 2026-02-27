1. **Giriş Yapma**
   - **API Metodu:** `POST /auth/login`
   - **Açıklama:** Kullanıcı kullanıcı adı ve şifresiyle giriş yapabilir. Kayıt olurken email'i de sağladıysa kullanıcı adı yerine onu da kullanabilir.

1. **Üye Olma**
   - **API Metodu:** `POST /auth/register`
   - **Açıklama:** Yeni kullanıcı bilgileri sisteme kaydedilir ve hesap oluşturulur. Email zorunlu değildir fakat email olmadan şifresini yenileyemez.

1. **Kullanıcının Kendi Quizlerini Listelemesi**
   - **API Metodu:** `GET /users/me/quizzes`
   - **Açıklama:** Kullanıcı tarafından oluşturulan quizler listelenir. 

1. **Bir Kullanıcının Profiline Erişme**
   - **API Metodu:** `GET /users/{userId}`
   - **Açıklama:** Belirli bir kullanıcının profil bilgileri alınır.

1. **Bir Kullanıcıyı Silme (Sadece Admin)**
   - **API Metodu:** `DELETE /users/{userId}`
   - **Açıklama:** Admin yetkilerine sahip biri bir kullanıcıyı silebilir.

1. **Quiz Oluşturma**
   - **API Metodu:** `POST /quizzes`
   - **Açıklama:** Kullanıcı yeni bir quiz yaratır ve sisteme ekler.

1. **Quizleri Listeleme**
   - **API Metodu:** `GET /quizzes`
   - **Açıklama:** Sistemdeki tüm quizler temel bilgileriyle listelenir.

1. **Quizleri Filtreleme**
   - **API Metodu:** `GET /quizzes?user={userId}&topic={topic}&keyword={keyword}`
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
