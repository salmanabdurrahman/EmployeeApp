# EmployeeApp.Api

Backend REST API untuk manajemen data employee.
Project ini memakai **.NET Minimal API**, **Entity Framework Core**, dan **MySQL**.

## Responsibilities

- Menyediakan endpoint CRUD employee
- Melakukan validasi input (`name`, `position`, `salary`)
- Menyimpan data ke MySQL melalui EF Core
- Mengembalikan format respons JSON yang konsisten

## Tech Stack

- .NET SDK `10.0` (`net10.0`)
- ASP.NET Core Minimal APIs
- Entity Framework Core `9.x`
- Pomelo MySQL provider

## Key Files

```text
EmployeeApp.Api/
├── Endpoints/EmployeeEndpoints.cs
├── Models/AppDbContext.cs
├── Models/Employee.cs
├── Responses/
├── Migrations/
├── Program.cs
└── appsettings.json
```

## Prerequisites

- .NET 10 SDK
- MySQL server aktif
- Database `employee_db` tersedia

## Configuration

Connection string disimpan di `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=employee_db;User=root;Password=your_password;"
  }
}
```

Default URL dari `Properties/launchSettings.json`:
- HTTP: `http://localhost:5080`
- HTTPS profile juga membuka `https://localhost:7293`

## First-Time Setup

### 1. Buat database

```bash
mysql -u root -p -e "CREATE DATABASE IF NOT EXISTS employee_db;"
```

### 2. Jalankan migration

```bash
cd EmployeeApp.Api
dotnet ef database update
```

Jika `dotnet ef` belum tersedia:

```bash
dotnet tool install --global dotnet-ef
dotnet ef database update
```

## Run API

```bash
cd EmployeeApp.Api
dotnet run
```

Endpoint utama:
- `http://localhost:5080`

## Smoke Test

```bash
curl http://localhost:5080/
curl http://localhost:5080/api/employees
```

## Endpoints

Base URL: `http://localhost:5080`

| Method | Endpoint | Description |
|---|---|---|
| GET | `/` | API welcome/health response |
| GET | `/api/employees` | Ambil semua employee |
| GET | `/api/employees/{id}` | Ambil employee by id |
| POST | `/api/employees` | Tambah employee baru |
| PUT | `/api/employees/{id}` | Update employee |
| DELETE | `/api/employees/{id}` | Hapus employee |

## Validation Rules

- `name` wajib diisi
- `position` wajib diisi
- `salary` harus lebih dari 0

Jika validasi gagal, API mengembalikan `400 Bad Request` dengan daftar error.

## Response Format

Semua endpoint memakai wrapper respons yang konsisten.

Contoh success:

```json
{
  "status": "success",
  "message": "Employees retrieved successfully",
  "data": []
}
```

Contoh error:

```json
{
  "status": "error",
  "message": "Validation failed",
  "errors": [
    "Name is required",
    "Salary must be greater than zero"
  ]
}
```

Dokumentasi lengkap format respons: [RESPONSE_FORMAT.md](RESPONSE_FORMAT.md)

## cURL Examples

Create:

```bash
curl -X POST http://localhost:5080/api/employees \
  -H "Content-Type: application/json" \
  -d '{"name":"John Doe","position":"Developer","salary":5000}'
```

Update:

```bash
curl -X PUT http://localhost:5080/api/employees/1 \
  -H "Content-Type: application/json" \
  -d '{"name":"John Doe","position":"Senior Developer","salary":6000}'
```

Delete:

```bash
curl -X DELETE http://localhost:5080/api/employees/1
```

## Troubleshooting

MySQL connection gagal:
- Cek service MySQL berjalan
- Cek credential di `appsettings.json`
- Pastikan database `employee_db` sudah ada

Migration gagal:
- Pastikan package restore sukses (`dotnet restore`)
- Pastikan `dotnet ef` tersedia
- Jalankan ulang `dotnet ef database update`

Port 5080 sudah dipakai:
- `lsof -i :5080`
- Ubah port di `Properties/launchSettings.json` jika perlu

## Related Docs

- [Root README](../README.md)
- [Web README](../EmployeeApp.Web/README.md)
- [Response Format](RESPONSE_FORMAT.md)
