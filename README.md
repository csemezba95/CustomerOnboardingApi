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
