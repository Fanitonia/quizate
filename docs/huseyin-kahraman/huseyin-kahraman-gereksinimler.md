1. **Kullanıcının Kendi Profiline Erişmesi**
   - **API Metodu:** `GET /users/me`
   - **Açıklama:** Kullanıcı kendi hesap bilgilerini ve profil detaylarını görünteleyebilir.

1. **Kullanıcının Profilini Güncellemesi**
   - **API Metodu:** `PATCH /users/me`
   - **Açıklama:** Kullanıcı isim, fotoğraf veya diğer profil bilgilerini güncelleyebilir.

1. **Kullanıcının Kendi Hesabını Silmesi**
   - **API Metodu:** `DELETE /users/me`
   - **Açıklama:** Kullanıcı hesabını kalıcı olarak siler.  

1. **Bir Kullanıcının Quizlerini Listeleme** (Hüseyin Kahraman)
   - **API Metodu:** `GET /users/{userId}/quizzes`
   - **Açıklama:** Belirli bir kullanıcının oluşturduğu quizler listelenir.
   
1. **Quiz Konu Başlıklarını (Topic) Alma**
   - **API Metodu:** `GET /topics`
   - **Açıklama:** Kullanılabilir tüm konu başlıkları listelenir.

1. **Quiz Konu Başlığı (Topic) Yaratma (Sadece Admin)**
   - **API Metodu:** `POST /topics`
   - **Açıklama:** Admin yeni bir konu başlığı ekleyebilir.

1. **Quiz Konu Başlığı (Topic) Silme (Sadece Admin)**
   - **API Metodu:** `DELETE /topics/{topicName}`
   - **Açıklama:** Admin bir konu başlığını kaldırabilir.

1. **Quiz Konu Başlığı (Topic) Güncelleme (Sadece Admin)**
   - **API Metodu:** `PUT /topics/{topicName}`
   - **Açıklama:** Admin konu başlığının adını veya detaylarını düzenleyebilir.
