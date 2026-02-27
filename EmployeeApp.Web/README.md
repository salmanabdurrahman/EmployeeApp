# EmployeeApp.Web

Web UI for Employee Management System built with ASP.NET Core MVC and **Kendo UI Grid**.

## IMPORTANT: Kendo UI Setup Required

**Before running this project**, you must download and install Kendo UI files (~210 MB).

**[Complete Setup Guide →](KENDO_SETUP.md)**

**Quick steps:**

1. Download from https://www.telerik.com/download/kendo-ui (free 30-day trial)
2. Copy files to `wwwroot/lib/kendo-ui/`
3. Get license key and create `wwwroot/js/kendo-ui-license.js`

## Quick Start

```bash
cd EmployeeApp.Web
dotnet run
```

Web URL: `http://localhost:5142`

Important: API must be running at `http://localhost:5080` first!

## Features

- Kendo UI Grid with jQuery for data management
- Auto-refresh - Grid updates automatically after create/edit/delete
- View all employees with sorting, filtering, and pagination
- Add new employees via popup modal dialog
- Edit existing employees via popup modal dialog
- Delete employees with built-in confirmation dialog
- NumericTextBox for salary with currency format
- Real-time client-side validation
- Seamless API integration

## Pages

### Home Page (`/`)

- Dashboard with quick links
- Navigate to employee management
- Quick action cards

### Employee List (`/Employee`)

Kendo UI Grid with:
- Sorting: Click column headers to sort
- Filtering: Use filter row to search data
- Pagination: 5, 10, 20, or 50 items per page
- CRUD Operations: Add, Edit, Delete directly in grid
- Popup Editing: Modal dialog for create/edit
- Inline Actions: Edit and Delete buttons in each row

## Configuration

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

## Project Structure

```
EmployeeApp.Web/
├── Controllers/
│ ├── HomeController.cs
│ └── EmployeeApiController.cs # API proxy for Kendo Grid
├── Models/
│ ├── Employee.cs
│ ├── ApiResponse.cs
│ └── ErrorViewModel.cs
├── Services/
│ └── EmployeeApiService.cs # Backend API integration
├── Views/
│ ├── Employee/
│ │ └── Index.cshtml # Kendo Grid implementation
│ ├── Home/
│ │ └── Index.cshtml
│ └── Shared/
│ └── _Layout.cshtml # Kendo UI local references
├── wwwroot/
│ ├── js/
│ │ └── kendo-ui-license.js # License configuration
│ └── lib/
│ └── kendo-ui/ # ~210 MB Kendo UI files
│ ├── js/ # JavaScript files
│ └── styles/ # CSS themes
└── Program.cs
```

## API Integration

### Kendo Grid Integration
Kendo Grid connects to web API endpoints:

```javascript
// Grid configuration
dataSource: {
 transport: {
 read: { url: "/api/EmployeeApi", type: "GET" },
 create: { url: "/api/EmployeeApi", type: "POST" },
 update: { url: "/api/EmployeeApi/{id}", type: "PUT" },
 destroy: { url: "/api/EmployeeApi/{id}", type: "DELETE" }
 }
}
```

### Backend API Integration
`EmployeeApiController` uses `EmployeeApiService`:

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

## Running the Application

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

## Troubleshooting

### Kendo Grid not showing
**Error:  **Grid is empty or not rendered

**Solutions:  **
1. Check browser console for JavaScript errors
2. Verify Kendo files exist:
 ```bash
 ls wwwroot/lib/kendo-ui/js/kendo.all.min.js
 ls wwwroot/lib/kendo-ui/styles/default-main.css
 ls wwwroot/js/kendo-ui-license.js
 ```
3. Check license file is loaded (check browser Network tab)
4. Ensure API is running and returning data

### License warning
**Warning:  **"No license found for Kendo UI"

**Solution:  **
- License file is at `wwwroot/js/kendo-ui-license.js`
- Loaded in `_Layout.cshtml` after `kendo.all.min.js`
- 30-day trial license included

### Cannot connect to API
**Error:  **"Failed to load employees"

**Solution:  **
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

## Development

### Run with Hot Reload
```bash
dotnet watch run
```

### Test API Connection
```bash
curl http://localhost:5080/api/employees
```

## Tech Stack

- ASP.NET Core 10 MVC
- **Kendo UI 2026.1.212**(jQuery version) -**Locally hosted**
- jQuery 3.7.0 (local)
- Bootstrap 5
- Bootstrap Icons (CDN)
- Razor Views
- HttpClient for API calls

## Kendo UI Setup

>**Required:  **Kendo UI files must be downloaded separately (NOT included in git).

### Quick Setup
See**[KENDO_SETUP.md](KENDO_SETUP.md)**for complete instructions.

**Summary:  **
1. Download Kendo UI Professional from https://www.telerik.com/download/kendo-ui
2. Extract and copy to `wwwroot/lib/kendo-ui/`
3. Get license from https://www.telerik.com/account/product-keys
4. Create `wwwroot/js/kendo-ui-license.js` with your key

### Local Installation
Kendo UI files are hosted locally in `wwwroot/lib/kendo-ui/`:
- **JavaScript:  **`js/kendo.all.min.js` (includes all components)
- **CSS Theme:  **`styles/default-main.css` (default theme)
- **License:  **`wwwroot/js/kendo-ui-license.js` (your personal key)

### _Layout.cshtml References
```html
<!-- Kendo UI CSS -->
<link rel="stylesheet" href="~/lib/kendo-ui/styles/default-main.css" />

<!-- jQuery + Kendo UI JS -->
<script src="~/lib/jquery/dist/jquery.min.js"></script>
<script src="~/lib/kendo-ui/js/kendo.all.min.js"></script>
<script src="~/js/kendo-ui-license.js"></script>
```

### File Size
- Total Kendo UI: ~210 MB
- **NOT included in git **- Download separately
- See [KENDO_SETUP.md](KENDO_SETUP.md) for installation guide
- No CDN dependencies for Kendo (Bootstrap Icons still uses CDN)

## Kendo UI Features Used

- **Kendo Grid:  **Main data table component
- **Popup Editor:  **Modal dialog for create/edit operations
- **NumericTextBox:  **For salary input with decimal precision
- **Built-in validation:  **Required fields, min values
- **CRUD operations:  **Full create, read, update, delete support

**Web runs at:  **`http://localhost:5142` 
**API must run at:  **`http://localhost:5080`
