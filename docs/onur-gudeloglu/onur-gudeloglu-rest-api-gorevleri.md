# Onur Güdeloğlu'nun REST API Metotları

**API Test Videosu:** [YouTube Videosu](https://www.youtube.com/watch?v=aaBuQnbR4Xk)

## 1. Üye Olma
- **Endpoint:** `POST /auth/register`
- **Request Body:** 
```json
{
  "username": "string",
  "email": "string",
  "password": "string" 
}
```
- **Response:** `201 Created` - Kulanıcı yaratıldı, `400 Bad Request` - Üye olurken hata.

## 2. Giriş Yapma
- **Endpoint:** `POST /auth/login`
- **Request Body:**
```json
{
  "usernameOrEmail": "string",
  "password": "string"
}
```
- **Response:** `200 OK` - Başarıyla giriş yapıldı (tokenler cookie olarak browserda tutuluyor). `400 Bad Request` - Giriş hatası.

## 3. Çıkış Yapma
- **Endpoint:** `PUT /auth/logout`
- **Authentication:** Cookie token ya da bearer token gerekli.
- **Response:** `200 OK` - Başarıyla çıkış yapıldı, `401 Unauthorized` - Yetki hatasız

## 4. Bir Kullanıcının Profiline Erişme
- **Endpoint:** `GET /users/{userId}`
- **Response:** `200 OK` - Kullanıcının bilgileri, `404 Not Found` - Kullanıcı bulunamadı.

## 5. Quiz Oluşturma
- **Endpoint:** `POST /quizzes`
- **Response:** `201 Created` - Quiz yaratıldı, `200 Bad Request` - Hata.

## 6. Quizleri Listeleme
- **Endpoint:** `GET /quizzes`
- **Path Parameters:** 
  - `page` (number, optional) - kaçıncı sayfa.
  - `pageSize` (number, optional) - sayfaların büyüklüğü.
- **Response:** `200 OK` - Quiz listesi, `200 Bad Request` - Hata.

## 7. Bir Quize Erişme
- **Endpoint:** `GET /quizzes/{quizId}`
- **Response:** `200 OK` - Quiz bilgileri, `404 Not Found` - Quiz bulunamadı.

## 8. Bir Quizin Sorularını Alma
- **Endpoint:** `GET /quizzes/{quizId}/questions`
- **Response:** `200 OK` - Quiz soruları, `404 Not Found` - Quiz bulunamadı

## 9. Bir Quizi Güncelleme
- **Endpoint:** `PATCH /quizzes/{quizId}`
- **Authorization:** Quiz sahibi ya da admin yetkili cookie/bearer token gerekli.
- **Request Body:**
```json
{
  "title": "string",
  "description": "string",
  "thumbnailUrl": "string",
  "isPublic": true,
  "languageCode": "string"
}
```
- **Response:** `204 No Content` - Güncellendi, `404 Not Found` - Quiz bulunamadı, `403 Forbidden` - Güncelleme yetkisi yok.

## 10. Kullanıcının Kendi Quizlerini Listelemesi
- **Endpoint:** `GET /users/me/quizzes`
- **Path Parameters:** 
  - `page` (number, optional) - kaçıncı sayfa.
  - `pageSize` (number, optional) - sayfaların büyüklüğü.
- **Authentication:** Cookie ya da bearer token gerekli.
- **Response:** `200 OK` - Kullanıcının Quiz'lerinin listesi, `401 Unauthorized` - Yetki hatası

## 11. Bir Quizi Silme
- **Endpoint:** `DELETE /quizzes/{quizId}`
- **Authorization:** Quiz sahibi ya da admin yetkili cookie/bearer token gerekli.
- **Response:** `204 No Content` - Quiz soruları, `404 Not Found` - Quiz bulunamadı, `403 Forbidden` - Silme yetkisi yok.

## 12. Bir Kullanıcıyı Silme
- **Endpoint:** `DELETE /users/{userId}`
- **Authorization:** Admin yetkili cookie/bearer token gerekli.
- **Response:** `200 OK` - Kullanıcı silindi, `404 Not Found` - Kullanıcı bulunamadı.







