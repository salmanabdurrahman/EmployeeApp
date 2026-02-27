# Employee Management System

Full-stack employee management application with RESTful API and Web UI.

## Overview

This project consists of two applications:

- **EmployeeApp.API **- RESTful API backend with MySQL database
- **EmployeeApp.Web **- Web UI built with ASP.NET Core MVC

## Features

### API Features

- RESTful CRUD operations
- MySQL database with Entity Framework Core
- Input validation with detailed error messages
- Consistent JSON response format
- Minimal API design

### Web Features

- Kendo UI Grid with jQuery for interactive data management
- Modal CRUD - Add, edit, delete without page reload
- Auto-refresh - Grid updates automatically after operations
- Sortable, filterable, and paginated employee list
- NumericTextBox for salary with currency format
- Real-time validation feedback
- Responsive UI with Bootstrap 5

## Tech Stack

**Backend (API):  **
- .NET 10
- Entity Framework Core 9
- MySQL (Pomelo)
- Minimal APIs

**Frontend (Web):  **
- ASP.NET Core 10 MVC
- Kendo UI 2026.1.212 (jQuery) -**Locally hosted**(210 MB)
- jQuery 3.7.0
- Bootstrap 5
- Bootstrap Icons
- Razor Views

## Project Structure

```
EmployeeApp/
├── EmployeeApp.Api/ # Backend API
│ ├── Endpoints/ # API endpoints
│ ├── Models/ # Data models
│ ├── Responses/ # Response formats
│ └── Program.cs # API startup
├── EmployeeApp.Web/ # Frontend Web UI
│ ├── Controllers/ # MVC controllers
│ ├── Models/ # View models
│ ├── Services/ # API service client
│ ├── Views/ # Razor views
│ └── Program.cs # Web startup
└── README.md # This file
```

## Quick Start

### Prerequisites

- .NET 10 SDK
- MySQL Server
- Kendo UI for jQuery - See [Setup Guide](EmployeeApp.Web/KENDO_SETUP.md)

**IMPORTANT:  **Kendo UI files (~210 MB) are NOT included in the repository. You must download them separately. See the [Kendo UI Setup Guide](EmployeeApp.Web/KENDO_SETUP.md) for detailed instructions.

### 1. Setup Kendo UI (First Time Only)
```bash
# Follow the complete guide:
cat EmployeeApp.Web/KENDO_SETUP.md

# Quick summary:
# 1. Download from https://www.telerik.com/download/kendo-ui (free trial)
# 2. Copy files to wwwroot/lib/kendo-ui/
# 3. Get license key from https://www.telerik.com/account/product-keys
# 4. Create wwwroot/js/kendo-ui-license.js with your key
```

### 2. Setup Database
```bash
# Create database
mysql -u root -p
CREATE DATABASE employee_db;
exit
```

### 3. Run API (Terminal 1)
```bash
cd EmployeeApp.Api
dotnet ef database update
dotnet run
```

**API runs at:  **http://localhost:5080

### 4. Run Web App (Terminal 2)

```bash
cd EmployeeApp.Web
dotnet run
```

**Web runs at:  **http://localhost:5142

### 5. Open Browser

Navigate to: http://localhost:5142

## API Documentation

### Base URL
```
http://localhost:5080
```

### Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/employees` | Get all employees |
| GET | `/api/employees/{id}` | Get employee by ID |
| POST | `/api/employees` | Create new employee |
| PUT | `/api/employees/{id}` | Update employee |
| DELETE | `/api/employees/{id}` | Delete employee |

### Response Format

**Success:  **
```json
{
 "status": "success",
 "message": "Employees retrieved successfully",
 "data": [...]
}
```

**Error:  **
```json
{
 "status": "error",
 "message": "Validation failed",
 "errors": ["Name is required", "Salary must be greater than zero"]
}
```

See [Full API Documentation](EmployeeApp.Api/RESPONSE_FORMAT.md) for more details.

## Configuration

### API Configuration
`EmployeeApp.Api/appsettings.json`:
```json
{
 "ConnectionStrings": {
 "DefaultConnection": "Server=localhost;Database=employee_db;User=root;Password=your_password;"
 }
}
```

**Port Configuration:  **
`EmployeeApp.Api/Properties/launchSettings.json` - Set to `http://localhost:5080`

### Web Configuration
`EmployeeApp.Web/appsettings.json`:
```json
{
 "ApiSettings": {
 "BaseUrl": "http://localhost:5080"
 }
}
```

**Port Configuration:  **
`EmployeeApp.Web/Properties/launchSettings.json` - Set to `http://localhost:5142`

## Testing

### Test API with cURL
```bash
# Get all employees
curl http://localhost:5080/api/employees

# Create employee
curl -X POST http://localhost:5080/api/employees \
 -H "Content-Type: application/json" \
 -d '{"name":"John Doe","position":"Developer","salary":5000}'

# Get by ID
curl http://localhost:5080/api/employees/1

# Update employee
curl -X PUT http://localhost:5080/api/employees/1 \
 -H "Content-Type: application/json" \
 -d '{"name":"John Doe","position":"Senior Developer","salary":6000}'

# Delete employee
curl -X DELETE http://localhost:5080/api/employees/1
```

### Test Web UI
1. Open browser: `http://localhost:5142`
2. Click "Go to Employee Grid" on home page
3. Click "Add New Employee" button in grid toolbar
4. Fill modal form and click "Save"
5.**Grid auto-refreshes **- new employee appears instantly
6. Click "Edit" button on any row to modify
7. Click "Delete" button to remove (with confirmation)

## Additional Documentation

- [API README](EmployeeApp.Api/README.md) - Detailed API documentation
- [Web README](EmployeeApp.Web/README.md) - Web UI documentation
- [Kendo UI Setup Guide](EmployeeApp.Web/KENDO_SETUP.md) -**How to install Kendo UI**
- [Response Format Guide](EmployeeApp.Api/RESPONSE_FORMAT.md) - API response examples

## Troubleshooting

### Kendo UI not loading
- Verify files exist: `ls EmployeeApp.Web/wwwroot/lib/kendo-ui/`
- Should have: `js/kendo.all.min.js` and `styles/default-main.css`
- Check browser console for 404 errors
- Ensure license file exists: `wwwroot/js/kendo-ui-license.js`

### API won't start
- Check MySQL is running: `sudo systemctl status mysql`
- Verify connection string in `appsettings.json`
- Run migrations: `dotnet ef database update`
- Check port 5080 is not in use: `lsof -i :5080`

### Web can't connect to API
- Ensure API is running first at `http://localhost:5080`
- Check API URL in `EmployeeApp.Web/appsettings.json`
- Verify firewall settings
- Test API directly: `curl http://localhost:5080/api/employees`

### Port already in use
```bash
# Check what's using the port
lsof -i :5080 # for API
lsof -i :5142 # for Web

# Change port in Properties/launchSettings.json
# Or run with custom port:
dotnet run --urls "http://localhost:5081"
```

### Build errors
```bash
dotnet clean
dotnet restore
dotnet build
```

## Running Both Projects

### Option 1: Two Terminals (Recommended)
```bash
# Terminal 1 - API
cd EmployeeApp.Api
dotnet run

# Terminal 2 - Web
cd EmployeeApp.Web
dotnet run
```

### Option 2: Background Process
```bash
# Start API in background
cd EmployeeApp.Api
dotnet run &

# Start Web in foreground
cd ../EmployeeApp.Web
dotnet run
```

### Option 3: Using dotnet watch (Hot Reload)
```bash
# Terminal 1 - API with hot reload
cd EmployeeApp.Api
dotnet watch run

# Terminal 2 - Web with hot reload
cd EmployeeApp.Web
dotnet watch run
```

## Access URLs

| Application | URL | Description |
|-------------|-----|-------------|
|**Web UI**| http://localhost:5142 | Main web interface |
|**API**| http://localhost:5080 | REST API endpoints |
|**API Docs**| http://localhost:5080/api/employees | Test endpoint |

## Development Workflow

1.**Start API first:  **
 ```bash
 cd EmployeeApp.Api && dotnet run
 ```
 Wait for: `Now listening on: http://localhost:5080`

2.**Start Web second:  **
 ```bash
 cd EmployeeApp.Web && dotnet run
 ```
 Wait for: `Now listening on: http://localhost:5142`

3.**Open browser:  **
 ```
 http://localhost:5142
 ```

4.**Make changes:  **
 - Edit files in either project
 - Save changes
 - Refresh browser or restart app

## Future Enhancements

- Authentication & Authorization
- Search and filtering
- Export to Excel/PDF
- Department management
- Employee photo upload
- Pagination for large datasets
- Logging and monitoring
- API versioning
- Docker containerization

## License

This project is for educational purposes.

## Development

Built using .NET 10, Entity Framework Core, and ASP.NET Core MVC.

## Quick Reference

**Start Everything:  **
```bash
# Terminal 1
cd EmployeeApp.Api && dotnet run

# Terminal 2 
cd EmployeeApp.Web && dotnet run

# Browser
http://localhost:5142
```

**Test API:  **
```bash
curl http://localhost:5080/api/employees
```

**Stop Everything:  **
```bash
# Press Ctrl+C in both terminals
```
