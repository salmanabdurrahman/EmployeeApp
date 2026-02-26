# Employee API

A simple REST API for managing employee data using .NET and MySQL.

## Features
- ✅ Get all employees
- ✅ Get employee by ID
- ✅ Create new employee
- ✅ Update employee
- ✅ Delete employee
- ✅ Input validation with detailed error messages
- ✅ MySQL database
- ✅ **Consistent response format** for all endpoints

## Response Format

All endpoints return responses in a consistent format:

**Success Response:**
```json
{
  "status": "success",
  "message": "Operation completed successfully",
  "data": { ... }
}
```

**Error Response:**
```json
{
  "status": "error",
  "message": "Brief error description",
  "errors": ["Detailed error 1", "Detailed error 2"]
}
```

📖 **[View Full Response Documentation →](RESPONSE_FORMAT.md)**

## Quick Start

### Prerequisites
- .NET 10 SDK
- MySQL Server

### Run the API
```bash
cd EmployeeApp.Api
dotnet run
```

API will run at `http://localhost:5000`

### Database Setup
```sql
CREATE DATABASE employee_db;
```

Run migrations:
```bash
dotnet ef database update
```

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/employees` | Get all employees |
| GET | `/api/employees/{id}` | Get employee by ID |
| POST | `/api/employees` | Create new employee |
| PUT | `/api/employees/{id}` | Update employee |
| DELETE | `/api/employees/{id}` | Delete employee |

## Example Requests

### Get All Employees
```bash
curl http://localhost:5000/api/employees
```

**Response:**
```json
{
  "status": "success",
  "message": "Employees retrieved successfully",
  "data": [
    {
      "id": 1,
      "name": "John Doe",
      "position": "Developer",
      "salary": 5000
    }
  ]
}
```

### Create Employee
```bash
curl -X POST http://localhost:5000/api/employees \
  -H "Content-Type: application/json" \
  -d '{"name":"John Doe","position":"Developer","salary":5000}'
```

**Response:**
```json
{
  "status": "success",
  "message": "Employee created successfully",
  "data": {
    "id": 1,
    "name": "John Doe",
    "position": "Developer",
    "salary": 5000
  }
}
```

### Validation Error Example
```bash
curl -X POST http://localhost:5000/api/employees \
  -H "Content-Type: application/json" \
  -d '{"name":"","position":"","salary":-100}'
```

**Response:**
```json
{
  "status": "error",
  "message": "Validation failed",
  "errors": [
    "Name is required",
    "Position is required",
    "Salary must be greater than zero"
  ]
}
```

## Configuration

Edit `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=employee_db;User=root;Password=your_password;"
  }
}
```

## Tech Stack
- .NET 10
- Entity Framework Core 9
- MySQL (Pomelo)
- Minimal APIs

## Project Structure
```
EmployeeApp.Api/
├── Models/              # Data models
│   ├── Employee.cs
│   └── AppDbContext.cs
├── Responses/           # API response formats
│   └── ApiResponse.cs
├── Endpoints/           # API endpoints (separated)
│   └── EmployeeEndpoints.cs
└── Program.cs           # Application startup (clean & simple!)
```

## Documentation
- [Response Format Guide](RESPONSE_FORMAT.md) - Complete API response documentation
