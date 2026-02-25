1. **Kullanıcının Kendi Profiline Erişmesi**
   - **API Metodu:** `GET /users/me`
   - **Açıklama:** Kullanıcı kendi hesap bilgilerini ve profil detaylarını görünteleyebilir.

1. **Kullanıcının Profilini Güncellemesi**
   - **API Metodu:** `PATCH /users/me`
   - **Açıklama:** Kullanıcı isim, fotoğraf veya diğer profil bilgilerini güncelleyebilir.

1. **Kullanıcının Kendi Hesabını Silmesi**
   - **API Metodu:** `DELETE /users/me`
   - **Açıklama:** Kullanıcı hesabını kalıcı olarak siler.  

1. **Quiz Çözüldükten Sonra Kaydını Tutma**
   - **API Metodu:** `POST /attempts/{quizId}`
   - **Açıklama:** Kullanıcının yaptığı çözüm girişimi sonucu kaydedilir. Bu sonuçlar quizlerle ilgili istatististk hazırlamakta ve bunları kullanıcıya göstermekte kullanılacak. 

1. **Bir Quiz'in Çözüm Girişimlerini Alma**
   - **API Metodu:** `GET /attempts/{quizId}`
   - **Açıklama:** Quiz için yapılan tüm çözüm girişimleri listelenir.

1. **Quiz Konu Başlıklarını (Topic) Alma**
   - **API Metodu:** `GET /topics`
   - **Açıklama:** Kullanılabilir tüm konu başlıkları listelenir.

1. **Quiz Konu Başlığı (Topic) Yaratma (Sadece Admin)**
   - **API Metodu:** `POST /topics`
   - **Açıklama:** Admin yeni bir konu başlığı ekleyebilir.

1. **Quiz Konu Başlığı (Topic) Silme (Sadece Admin)**
   - **API Metodu:** `DELETE /topics`
   - **Açıklama:** Admin bir konu başlığını kaldırabilir.

1. **Quiz Konu Başlığı (Topic) Güncelleme (Sadece Admin)**
   - **API Metodu:** `PUT /topics`
   - **Açıklama:** Admin konu başlığının adını veya detaylarını düzenleyebilir.
