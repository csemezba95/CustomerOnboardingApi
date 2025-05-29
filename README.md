# Customer Onboarding API

Customer Onboarding API is a .NET 8 Web API project designed to support the customer onboarding process for a mobile application. It provides a full suite of endpoints for registering new customers and allowing existing customers to securely log in using OTP verification, PIN creation, and optional fingerprint authentication.

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

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- Visual Studio 2022 or later
- Git

### 2. Clone Repository

```bash
git clone https://github.com/csemezba95/CustomerOnboardingApi.git
cd CustomerOnboardingApi
3. Configure Database
Edit appsettings.json and update your connection string:

json
Copy
Edit
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;user=root;password=;database=CustomerDb;"
}
⚠️ Make sure your MySQL server is running and accessible.

4. Install Required NuGet Packages
Use the Package Manager Console or run these commands:

bash
Copy
Edit
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Pomelo.EntityFrameworkCore.MySql
Install-Package Swashbuckle.AspNetCore
Or simply run:

bash
Copy
Edit
dotnet restore
5. Apply Migrations and Create the Database
bash
Copy
Edit
dotnet ef migrations add InitialCreate
dotnet ef database update
6. Run the Application
bash
Copy
Edit
dotnet run
Navigate to:

bash
Copy
Edit
https://localhost:5001/swagger
To access the Swagger UI and test your APIs.

📘 API Endpoints Overview
🔐 New Customer Registration Flow
POST /api/customer/register – Register using IC Number

POST /api/customer/verify-otp – Verify received OTP (valid for 2 minutes)

POST /api/customer/set-pin/{id} – Set a secure 6-digit PIN

POST /api/customer/enable-fingerprint/{id} – Enable fingerprint login (optional)

🔑 Existing Customer Login Flow
POST /api/customer/send-resend-otp – Send/resend OTP using IC number

POST /api/customer/verify-otp – Verify OTP (must be used within 2 minutes)

🧰 Tools & Frameworks
.NET 8

MySQL

Entity Framework Core

Pomelo.EntityFrameworkCore.MySql

Swashbuckle (Swagger)

Visual Studio 2022+

