## Video Link 
<video width="640" height="360" controls autoplay muted>
  <source src="https://jam.dev/c/56a88e71-465f-493d-8c5b-b59920a25476" type="video/mp4">
  Your browser does not support the video tag.
</video>


---

## Customer Onboarding API

Customer Onboarding API is a .NET 8 Core Web API project designed to support the customer onboarding process for a mobile application. It provides a full suite of endpoints for registering new customers and allowing existing customers to securely log in using OTP verification, PIN creation, and optional fingerprint authentication.

This project uses MySQL as the database backend and Entity Framework Core for ORM. It follows best practices in validation, separation of concerns, and includes comprehensive Swagger (OpenAPI) documentation for testing and integration.

---

## 🚀 Features

- New and existing customer onboarding
- OTP (One-Time Password) generation and validation
- 2-minute OTP expiration control
- Secure 6-digit PIN setup
- Optional fingerprint login enablement
- Entity Framework Core with MySQL
- Fully documented with Swagger UI

---

## 🛠️ Setup Instructions

### 1. Prerequisites

Make sure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/)
- [Git](https://git-scm.com/)

---

### 2. Clone the Repository

```bash
git clone https://github.com/csemezba95/CustomerOnboardingApi.git
cd CustomerOnboardingApi
````

---

### 3. Configure the Database Connection

Edit the `appsettings.json` file and update your MySQL connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;user=root;password=;database=CustomerDb;"
}
```

> ⚠️ Ensure your MySQL server is running with the specified credentials and the database exists or can be created.

---

### 4. Install Required NuGet Packages

Use the terminal or Package Manager Console in Visual Studio to install the required dependencies:

```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Swashbuckle.AspNetCore
```

Or restore all dependencies at once:

```bash
dotnet restore
```

---

### 5. Apply Migrations & Create the Database

Run the following commands to generate and apply the database schema:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

### 6. Run the Application

```bash
dotnet run
```

Then navigate to:

```
https://localhost:5001/swagger
```

Use Swagger UI to test and explore the API endpoints.

---

## 📘 API Endpoints Overview

### 🔐 New Customer Registration Flow

| Method | Endpoint                                | Description                         |
| ------ | --------------------------------------- | ----------------------------------- |
| POST   | `/api/customer/register`                | Register customer by IC number      |
| POST   | `/api/customer/verify-otp`              | Verify OTP (valid for 2 minutes)    |
| POST   | `/api/customer/set-pin/{id}`            | Set secure 6-digit PIN              |
| POST   | `/api/customer/enable-fingerprint/{id}` | Enable fingerprint login (optional) |

### 🔑 Existing Customer Login Flow

| Method | Endpoint                        | Description                        |
| ------ | ------------------------------- | ---------------------------------- |
| POST   | `/api/customer/send-resend-otp` | Send/resend OTP using IC number    |
| POST   | `/api/customer/verify-otp`      | Verify login OTP (2-minute expiry) |

---

## 🧰 Technologies Used

* **.NET 8**
* **MySQL**
* **Entity Framework Core**
* **Pomelo.EntityFrameworkCore.MySql**
* **Swagger / Swashbuckle**
* **Visual Studio 2022+**

---

## ✅ Example `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;user=root;password=;database=CustomerDb;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

---

## 🧪 Swagger Documentation

Once the application is running, open your browser and go to:

```
https://localhost:5001/swagger
```

This will open Swagger UI, where you can test all API endpoints interactively.

---
