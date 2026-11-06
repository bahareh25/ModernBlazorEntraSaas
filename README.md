# 🔗 URL Shortener

A modern URL Shortener built with **ASP.NET Core**, designed to convert long URLs into short, shareable links with fast redirection, analytics, and a clean REST API.

---

## 🚀 Features

- Create short URLs
- Redirect short URLs to the original destination
- Custom short codes (optional)
- URL validation
- Expiration date support
- Click tracking
- RESTful API
- Swagger/OpenAPI documentation
- SQL Server database
- Entity Framework Core
- Clean Architecture
- Dependency Injection
- Logging and Error Handling

---

## 🛠️ Tech Stack

### Backend
- ASP.NET Core 8
- C#
- Entity Framework Core
- SQL Server
- LINQ

### Architecture
- Clean Architecture
- Repository Pattern
- Dependency Injection
- SOLID Principles

### Tools
- Visual Studio 2022
- Swagger
- Git
- GitHub

---

## 📂 Project Structure

```
UrlShortener
│
├── Api                 # REST API
├── Application         # Business Logic
├── Domain              # Entities
├── Infrastructure      # Database & Repositories
└── Persistence         # EF Core Configurations
```

---

## ⚙️ Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server
- Visual Studio 2022

### Clone Repository

```bash
git clone https://github.com/yourusername/UrlShortener.git

cd UrlShortener
```

### Update Connection String

Edit:

```json
appsettings.json
```

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=UrlShortenerDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### Apply Migrations

```bash
dotnet ef database update
```

### Run

```bash
dotnet run
```

Swagger will be available at:

```
https://localhost:5001/swagger
```

---

## 📌 API Endpoints

### Create Short URL

```
POST /api/urls
```

Request

```json
{
    "originalUrl": "https://www.google.com"
}
```

Response

```json
{
    "shortCode": "Ab12Cd",
    "shortUrl": "https://localhost:5001/Ab12Cd"
}
```

---

### Redirect

```
GET /{shortCode}
```

Redirects the user to the original URL.

---

### Get URL Details

```
GET /api/urls/{shortCode}
```

Returns

- Original URL
- Short URL
- Click Count
- Created Date
- Expiration Date

---

## 🗄️ Database

Main Entity

| Field | Type |
|--------|------|
| Id | Guid |
| OriginalUrl | string |
| ShortCode | string |
| ClickCount | int |
| CreatedAt | DateTime |
| ExpirationDate | DateTime? |

---

## 🏗️ Architecture

```
Client
   │
   ▼
Controllers
   │
   ▼
Application Services
   │
   ▼
Repositories
   │
   ▼
SQL Server
```

---

## 📸 Screenshots

Add screenshots here.

Example:

```
/docs/swagger.png

/docs/homepage.png
```

---

## 🧪 Future Improvements

- JWT Authentication
- Rate Limiting
- Redis Caching
- QR Code Generation
- URL Analytics Dashboard
- Docker Support
- Azure Deployment
- Unit & Integration Tests
- Custom Domains
- Bulk URL Import

---

## 📖 Learning Objectives

This project demonstrates:

- REST API development
- Entity Framework Core
- Clean Architecture
- Dependency Injection
- Repository Pattern
- URL Redirection
- Database Design
- API Documentation
- Error Handling
- Scalable backend development

---

## 🤝 Contributing

Contributions are welcome.

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to your branch
5. Open a Pull Request

---

## 📄 License

This project is licensed under the MIT License.

---

## 👨‍💻 Author

**Tahereh Boroumandnejad**

Senior Full-Stack Software Engineer

- C#
- ASP.NET Core
- SQL Server
- React
- Azure
