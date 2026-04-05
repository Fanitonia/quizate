# Hüseyin Kahraman'ın REST API Metotları

**API Test Videosu:** [Video](https://youtu.be/V5Yw1vAiQbs)

## 1. Kullanıcının Kendi Profiline Erişmesi
- **Endpoint:** `GET users/me`
- **Request Body:** 
- **Authentication:** Cookie token ya da bearer token gerekli.
- **Response:** `200 OK` - Kullanıcı bilgileri, `401 Unauthorized` - Yetki hatası.

## 2. Kullanıcının Profilini Güncellemesi
- **Endpoint:** `PATCH users/me`
- **Request Body:**
```json
{
  "username": "string",
  "email": "string"
}
```
- **Response:** `204 No Content` - Profil başarıyla güncellendi, `400 Bad Request` - Hata.

## 3. Kullanıcının Hesabını Silmesi
- **Endpoint:** `DELETE /users/me`
- **Authentication:** Cookie token ya da bearer token gerekli.
- **Response:** `204 No Content` - Hesap başarıyla silindi, `401 Unauthorized` - Yetki hatası.

## 4. Bir Kullanıcının Quizlerini Listeleme
- **Endpoint:** `GET /users/{userId}/quizzes`
- **Response:** `200 OK` - Kullanıcının quizleri

## 5. Quiz Konu Başlıklarını (Topic) Listeleme
- **Endpoint:** `GET /topics`
- **Response:** `200 OK` - Topic listesi

## 6. Quiz Konu Başlığı (Topic) Yaratma
- **Endpoint:** `POST /topics`
- **Authorization:** Admin yetkili cookie/bearer token gerekli.
- **Request Body:**
```json
{
{
  "name": "string",
  "displayName": "string",
  "description": "string"
}
}
```
- **Response:** `201 Created` - Topic yaratıldı, `400 Bad Request` - Hata.

## 7. Quiz Konu Başlığını (Topic) Silme
- **Endpoint:** `DELETE /topics/{topicId}`
- **Authorization:** Admin yetkili cookie/bearer token gerekli.
- **Response:** `204 No Content` - Topic silindi, `404 Not Found` - Topic bulunamadı, `403 Forbidden` - Silme yetkisi yok. `400 Bad Request` - Hata.

## 8. Quiz Konu Başlığı (Topic) Güncelleme
- **Endpoint:** `PATCH /topics/{topicId}`
- **Authorization:** Admin yetkili cookie/bearer token gerekli.
- **Request Body:**
```json
{
  "displayName": "string",
  "description": "string"
}
```
- **Response:** `204 No Content` - Topic güncellendi, `404 Not Found` - Topic bulunamadı, `403 Forbidden` - Güncelleme yetkisi yok, `400 Bad Request` - Hata.