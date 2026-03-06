# EmployeeApp.Web

Frontend MVC app untuk Employee Management System.
UI utama menggunakan **Kendo UI Grid** untuk operasi CRUD tanpa reload halaman penuh.

## Responsibilities

- Menyediakan halaman web (`/`, `/Employee`)
- Menjadi proxy API untuk Kendo Grid melalui `/api/EmployeeApi`
- Menghubungkan UI dengan backend `EmployeeApp.Api`
- Menangani feedback error/success dari operasi CRUD

## Important: Kendo Setup Required

Sebelum menjalankan project ini, Kendo UI harus di-setup secara lokal.

Ikuti panduan lengkap:
- [KENDO_SETUP.md](KENDO_SETUP.md)

Minimal file yang harus ada:
- `wwwroot/lib/kendo-ui/js/kendo.all.min.js`
- `wwwroot/lib/kendo-ui/styles/default-main.css`
- `wwwroot/js/kendo-ui-license.js`

## Tech Stack

- ASP.NET Core MVC (`net10.0`)
- Kendo UI for jQuery (local assets)
- jQuery (local)
- Bootstrap 5 + Bootstrap Icons
- Razor Views
- HttpClient (`EmployeeApiService`) untuk panggil backend API

## Key Files

```text
EmployeeApp.Web/
├── Controllers/
│   ├── HomeController.cs
│   ├── EmployeeController.cs
│   └── EmployeeApiController.cs
├── Services/EmployeeApiService.cs
├── Views/Employee/Index.cshtml
├── Views/Shared/_Layout.cshtml
├── wwwroot/
├── Program.cs
├── appsettings.json
└── KENDO_SETUP.md
```

## Prerequisites

- .NET 10 SDK
- `EmployeeApp.Api` berjalan di `http://localhost:5080`
- Kendo UI assets + license key sudah terpasang lokal

## Configuration

`appsettings.json`:

```json
{
  "ApiSettings": {
    "BaseUrl": "http://localhost:5080"
  }
}
```

`Properties/launchSettings.json` (default):
- HTTP: `http://localhost:5142`
- HTTPS profile juga membuka `https://localhost:7298`

## Run Application

### 1. Jalankan API dulu

```bash
cd EmployeeApp.Api
dotnet run
```

### 2. Jalankan Web app

```bash
cd EmployeeApp.Web
dotnet run
```

Akses:
- `http://localhost:5142`

## Routes

| Route | Purpose |
|---|---|
| `/` | Home page |
| `/Employee` | Halaman Kendo Grid employee |
| `/api/EmployeeApi` | Endpoint proxy list/create |
| `/api/EmployeeApi/{id}` | Endpoint proxy get/update/delete |

## Data Flow

1. Kendo Grid memanggil endpoint `/api/EmployeeApi`
2. `EmployeeApiController` menerima request dari Grid
3. Controller memanggil `EmployeeApiService`
4. `EmployeeApiService` meneruskan ke backend API (`/api/employees`)
5. Respons dikembalikan ke Grid dan UI di-refresh

## Quick Verification

1. Buka `http://localhost:5142/Employee`
2. Pastikan Grid tampil normal
3. Coba Create, Edit, Delete employee
4. Pastikan data berubah tanpa reload halaman penuh
5. Cek DevTools:
- tidak ada error lisensi Kendo
- tidak ada `404` ke `/lib/kendo-ui/...`

## Troubleshooting

Grid tidak tampil:
- Cek file Kendo tersedia
- Cek `_Layout.cshtml` memuat CSS/JS Kendo
- Cek console browser untuk JavaScript error

License warning Kendo:
- Pastikan `wwwroot/js/kendo-ui-license.js` ada
- Pastikan script key valid
- Pastikan file license dimuat setelah `kendo.all.min.js`

Gagal load data employee:
- Pastikan API hidup di `http://localhost:5080`
- Test API langsung: `curl http://localhost:5080/api/employees`
- Pastikan `ApiSettings:BaseUrl` sesuai

Port 5142 sudah dipakai:
- `lsof -i :5142`
- Ubah port via `launchSettings.json` atau jalankan `dotnet run --urls "http://localhost:5143"`

## Related Docs

- [Root README](../README.md)
- [API README](../EmployeeApp.Api/README.md)
- [Kendo Setup Guide](KENDO_SETUP.md)
