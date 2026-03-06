# Employee Management System

Employee management sample app with:
- `EmployeeApp.Api`: REST API (.NET Minimal API + EF Core + MySQL)
- `EmployeeApp.Web`: Web UI (ASP.NET Core MVC + Kendo UI Grid)

## Architecture

Request flow:

`Browser (Kendo Grid)` -> `EmployeeApp.Web (/api/EmployeeApi)` -> `EmployeeApp.Api (/api/employees)` -> `MySQL`

Why this split exists:
- UI can stay simple while API keeps data logic centralized
- Web app can transform API errors into user-friendly Grid messages
- API is reusable for mobile/other clients

## Features

API:
- CRUD endpoints for employees
- Input validation for `name`, `position`, and `salary`
- Consistent JSON response wrapper (`status`, `message`, `data/errors`)
- EF Core with MySQL (Pomelo provider)

Web:
- Kendo Grid with popup CRUD
- Sorting, filtering, paging, and inline action buttons
- Auto refresh after create/update/delete
- Salary editor with `kendoNumericTextBox`
- Bootstrap-based responsive layout

## Tech Stack

- .NET SDK `10.0` (`net10.0`)
- ASP.NET Core Minimal API + MVC
- Entity Framework Core `9.x`
- MySQL
- Kendo UI for jQuery (local install, not committed to git)
- jQuery + Bootstrap 5

## Repository Structure

```text
EmployeeApp/
├── EmployeeApp.Api/
│   ├── Endpoints/
│   ├── Migrations/
│   ├── Models/
│   ├── Responses/
│   └── Program.cs
├── EmployeeApp.Web/
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   ├── Views/
│   ├── wwwroot/
│   ├── KENDO_SETUP.md
│   └── Program.cs
├── QUICKSTART.md
└── README.md
```

## Prerequisites

- .NET 10 SDK
- MySQL server running locally
- Telerik account (for Kendo UI download/license)

Important:
- Kendo UI files (`wwwroot/lib/kendo-ui`) are intentionally excluded from git.
- `wwwroot/js/kendo-ui-license.js` is personal and excluded from git.
- Follow [Kendo setup guide](EmployeeApp.Web/KENDO_SETUP.md) before running the web app.

## First-Time Setup

### 1. Clone and restore

```bash
git clone <your-repo-url>
cd EmployeeApp
dotnet restore
```

### 2. Install Kendo UI assets

Follow [EmployeeApp.Web/KENDO_SETUP.md](EmployeeApp.Web/KENDO_SETUP.md).

Minimum required files:
- `EmployeeApp.Web/wwwroot/lib/kendo-ui/js/kendo.all.min.js`
- `EmployeeApp.Web/wwwroot/lib/kendo-ui/styles/default-main.css`
- `EmployeeApp.Web/wwwroot/js/kendo-ui-license.js`

### 3. Configure database connection

Edit `EmployeeApp.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=employee_db;User=root;Password=your_password;"
  }
}
```

### 4. Create DB and apply migration

```bash
mysql -u root -p -e "CREATE DATABASE IF NOT EXISTS employee_db;"

cd EmployeeApp.Api
dotnet ef database update
```

If `dotnet ef` is not available:

```bash
dotnet tool install --global dotnet-ef
dotnet ef database update
```

## Running the Application

### Terminal 1: API

```bash
cd EmployeeApp.Api
dotnet run
```

Expected URL:
- `http://localhost:5080`

### Terminal 2: Web

```bash
cd EmployeeApp.Web
dotnet run
```

Expected URL:
- `http://localhost:5142`

Open:
- `http://localhost:5142`

## Quick Validation

Check API:

```bash
curl http://localhost:5080/
curl http://localhost:5080/api/employees
```

Check Web manually:
1. Open `http://localhost:5142`
2. Go to `Employees`
3. Try create/edit/delete from Grid popup
4. Confirm Grid refreshes after each action

## API Summary

Base URL: `http://localhost:5080`

| Method | Endpoint | Description |
|---|---|---|
| GET | `/` | Health/welcome response |
| GET | `/api/employees` | List employees |
| GET | `/api/employees/{id}` | Get one employee |
| POST | `/api/employees` | Create employee |
| PUT | `/api/employees/{id}` | Update employee |
| DELETE | `/api/employees/{id}` | Delete employee |

Detailed response examples: [EmployeeApp.Api/RESPONSE_FORMAT.md](EmployeeApp.Api/RESPONSE_FORMAT.md)

## Key Configuration Files

- API URL/ports: `EmployeeApp.Api/Properties/launchSettings.json`
- Web URL/ports: `EmployeeApp.Web/Properties/launchSettings.json`
- API DB connection: `EmployeeApp.Api/appsettings.json`
- Web API target URL: `EmployeeApp.Web/appsettings.json` (`ApiSettings:BaseUrl`)

## Common Issues

Kendo assets 404:
- Recheck `wwwroot/lib/kendo-ui/js/kendo.all.min.js`
- Recheck `wwwroot/lib/kendo-ui/styles/default-main.css`
- Recheck `wwwroot/js/kendo-ui-license.js`

Web cannot load employee data:
- Ensure API is running on `http://localhost:5080`
- Verify `EmployeeApp.Web/appsettings.json` points to the same API URL
- Test with `curl http://localhost:5080/api/employees`

MySQL connection failure:
- Ensure MySQL service is running
- Validate credentials in API `appsettings.json`
- Ensure database `employee_db` exists

Port already in use:
- API: `lsof -i :5080`
- Web: `lsof -i :5142`

## Additional Docs

- [Quick Start](QUICKSTART.md)
- [API README](EmployeeApp.Api/README.md)
- [Web README](EmployeeApp.Web/README.md)
- [Kendo UI Setup Guide](EmployeeApp.Web/KENDO_SETUP.md)
