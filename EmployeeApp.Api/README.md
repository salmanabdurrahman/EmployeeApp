# EmployeeApp.Api

RESTful API for Employee Management System built with .NET Minimal APIs.

## Quick Start

```bash
cd EmployeeApp.Api
dotnet ef database update
dotnet run
```

**API URL:  **`http://localhost:5080`

## Features

- RESTful CRUD operations
- MySQL database with Entity Framework Core
- Input validation with detailed error messages
- Consistent JSON response format
- Modular endpoint organization
- Clean architecture

## API Endpoints

### Base URL
```
http://localhost:5080
```

### Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/` | API welcome message |
| GET | `/api/employees` | Get all employees |
| GET | `/api/employees/{id}` | Get employee by ID |
| POST | `/api/employees` | Create new employee |
| PUT | `/api/employees/{id}` | Update employee |
| DELETE | `/api/employees/{id}` | Delete employee |

## Response Format

All endpoints return consistent JSON format.

### Success Response
```json
{
 "status": "success",
 "message": "Operation completed successfully",
 "data": { ... }
}
```

### Error Response
```json
{
 "status": "error",
 "message": "Brief error description",
 "errors": ["Detailed error 1", "Detailed error 2"]
}
```

📄**[View Full Response Documentation →](RESPONSE_FORMAT.md)**

## Testing with cURL

### Get All Employees
```bash
curl http://localhost:5080/api/employees
```

### Create Employee
```bash
curl -X POST http://localhost:5080/api/employees \
 -H "Content-Type: application/json" \
 -d '{"name":"John Doe","position":"Developer","salary":5000}'
```

### Update Employee
```bash
curl -X PUT http://localhost:5080/api/employees/1 \
 -H "Content-Type: application/json" \
 -d '{"name":"John Doe","position":"Senior Developer","salary":6000}'
```

### Delete Employee
```bash
curl -X DELETE http://localhost:5080/api/employees/1
```

**API runs at:  **`http://localhost:5080`
