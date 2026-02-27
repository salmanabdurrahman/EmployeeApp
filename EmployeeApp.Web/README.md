# EmployeeApp.Web

Web UI for Employee Management System built with ASP.NET Core MVC.

## 🚀 Quick Start

```bash
cd EmployeeApp.Web
dotnet run
```

**Web URL:** `http://localhost:5142`

⚠️ **Important:** API must be running at `http://localhost:5080` first!

## 📋 Features

- ✅ View all employees in a clean table
- ✅ Add new employees
- ✅ Edit existing employees
- ✅ Delete employees with confirmation
- ✅ Responsive Bootstrap UI
- ✅ Real-time validation
- ✅ Integration with Employee API

## 🎨 Pages

### Home Page
- Dashboard with quick links
- Navigate to employee management
- Add new employee shortcut

### Employee List (`/Employee`)
- View all employees in table
- Edit and Delete buttons
- Success/Error notifications

### Create Employee (`/Employee/Create`)
- Form with validation
- Real-time error display

### Edit Employee (`/Employee/Edit/{id}`)
- Pre-filled form
- Update employee data

### Delete Employee (`/Employee/Delete/{id}`)
- Confirmation page
- Shows employee details

## ⚙️ Configuration

### API Settings
`appsettings.json`:
```json
{
  "ApiSettings": {
    "BaseUrl": "http://localhost:5080"
  }
}
```

### Port Configuration
`Properties/launchSettings.json`:
```json
{
  "profiles": {
    "http": {
      "applicationUrl": "http://localhost:5142"
    }
  }
}
```

## 🗂️ Project Structure

```
EmployeeApp.Web/
├── Controllers/
│   ├── HomeController.cs
│   └── EmployeeController.cs
├── Models/
│   ├── Employee.cs
│   ├── ApiResponse.cs
│   └── ErrorViewModel.cs
├── Services/
│   └── EmployeeApiService.cs    # API integration
├── Views/
│   ├── Employee/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   └── Delete.cshtml
│   ├── Home/
│   │   └── Index.cshtml
│   └── Shared/
│       └── _Layout.cshtml
└── Program.cs
```

## 🔗 API Integration

The web app uses `EmployeeApiService` to communicate with API:

```csharp
// Get all employees
var employees = await _apiService.GetAllEmployeesAsync();

// Create employee
var (success, message, errors) = await _apiService.CreateEmployeeAsync(employee);

// Update employee
var (success, message, errors) = await _apiService.UpdateEmployeeAsync(id, employee);

// Delete employee
var (success, message) = await _apiService.DeleteEmployeeAsync(id);
```

## 🚦 Running the Application

### Step 1: Start API First
```bash
# Terminal 1
cd EmployeeApp.Api
dotnet run
# Wait for: Now listening on: http://localhost:5080
```

### Step 2: Start Web App
```bash
# Terminal 2
cd EmployeeApp.Web
dotnet run
# Wait for: Now listening on: http://localhost:5142
```

### Step 3: Open Browser
```
http://localhost:5142
```

## 🐛 Troubleshooting

### Cannot connect to API
**Error:** "Failed to load employees"

**Solution:**
1. Check API is running: `curl http://localhost:5080/api/employees`
2. Verify API URL in `appsettings.json`
3. Check firewall settings

### Port 5142 already in use
```bash
# Check what's using the port
lsof -i :5142

# Or run on different port
dotnet run --urls "http://localhost:5143"
```

### Build errors
```bash
dotnet clean
dotnet restore
dotnet build
```

## 🎯 Development

### Run with Hot Reload
```bash
dotnet watch run
```

### Test API Connection
```bash
curl http://localhost:5080/api/employees
```

## 📚 Tech Stack

- ASP.NET Core 10 MVC
- Bootstrap 5
- Bootstrap Icons
- Razor Views
- HttpClient for API calls

**Web runs at:** `http://localhost:5142`  
**API must run at:** `http://localhost:5080`
