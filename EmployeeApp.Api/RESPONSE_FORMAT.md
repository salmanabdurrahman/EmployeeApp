# API Response Format Documentation

## Response Structure

All API responses follow a consistent format for both success and error cases.

## Success Response Format

```json
{
  "status": "success",
  "message": "Operation completed successfully",
  "data": { ... }
}
```

### Fields:
- **status**: Always `"success"` for successful operations
- **message**: Human-readable success message
- **data**: The actual response data (can be object, array, or simple value)

## Error Response Format

```json
{
  "status": "error",
  "message": "Brief error description",
  "errors": ["Detailed error 1", "Detailed error 2"]
}
```

### Fields:
- **status**: Always `"error"` for failed operations
- **message**: Brief error description
- **errors**: (Optional) Array of detailed error messages, useful for validation errors

## Examples

### 1. GET All Employees (Success)
**Request:**
```http
GET /api/employees
```

**Response:** `200 OK`
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
    },
    {
      "id": 2,
      "name": "Jane Smith",
      "position": "Designer",
      "salary": 4500
    }
  ]
}
```

### 2. GET Single Employee (Success)
**Request:**
```http
GET /api/employees/1
```

**Response:** `200 OK`
```json
{
  "status": "success",
  "message": "Employee retrieved successfully",
  "data": {
    "id": 1,
    "name": "John Doe",
    "position": "Developer",
    "salary": 5000
  }
}
```

### 3. GET Single Employee (Not Found)
**Request:**
```http
GET /api/employees/999
```

**Response:** `404 Not Found`
```json
{
  "status": "error",
  "message": "Employee not found",
  "errors": null
}
```

### 4. POST Create Employee (Success)
**Request:**
```http
POST /api/employees
Content-Type: application/json

{
  "name": "Alice Johnson",
  "position": "Manager",
  "salary": 7000
}
```

**Response:** `201 Created`
```json
{
  "status": "success",
  "message": "Employee created successfully",
  "data": {
    "id": 3,
    "name": "Alice Johnson",
    "position": "Manager",
    "salary": 7000
  }
}
```

### 5. POST Create Employee (Validation Error)
**Request:**
```http
POST /api/employees
Content-Type: application/json

{
  "name": "",
  "position": "",
  "salary": -100
}
```

**Response:** `400 Bad Request`
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

### 6. PUT Update Employee (Success)
**Request:**
```http
PUT /api/employees/1
Content-Type: application/json

{
  "name": "John Doe",
  "position": "Senior Developer",
  "salary": 6000
}
```

**Response:** `200 OK`
```json
{
  "status": "success",
  "message": "Employee updated successfully",
  "data": {
    "id": 1,
    "name": "John Doe",
    "position": "Senior Developer",
    "salary": 6000
  }
}
```

### 7. PUT Update Employee (Not Found)
**Request:**
```http
PUT /api/employees/999
Content-Type: application/json

{
  "name": "John Doe",
  "position": "Developer",
  "salary": 5000
}
```

**Response:** `404 Not Found`
```json
{
  "status": "error",
  "message": "Employee not found",
  "errors": null
}
```

### 8. DELETE Employee (Success)
**Request:**
```http
DELETE /api/employees/1
```

**Response:** `200 OK`
```json
{
  "status": "success",
  "message": "Employee deleted successfully",
  "data": {
    "id": 1
  }
}
```

### 9. DELETE Employee (Not Found)
**Request:**
```http
DELETE /api/employees/999
```

**Response:** `404 Not Found`
```json
{
  "status": "error",
  "message": "Employee not found",
  "errors": null
}
```

## HTTP Status Codes

| Status Code | Description | When Used |
|-------------|-------------|-----------|
| 200 OK | Success | GET (found), PUT, DELETE success |
| 201 Created | Resource created | POST success |
| 400 Bad Request | Validation error | Invalid input data |
| 404 Not Found | Resource not found | GET/PUT/DELETE non-existent ID |
| 500 Internal Server Error | Server error | Unexpected errors |

---

## Testing with cURL

### Get all employees
```bash
curl http://localhost:5080/api/employees
```

### Get single employee
```bash
curl http://localhost:5080/api/employees/1
```

### Create employee
```bash
curl -X POST http://localhost:5080/api/employees \
  -H "Content-Type: application/json" \
  -d '{"name":"John Doe","position":"Developer","salary":5000}'
```

### Update employee
```bash
curl -X PUT http://localhost:5080/api/employees/1 \
  -H "Content-Type: application/json" \
  -d '{"name":"John Doe","position":"Senior Developer","salary":6000}'
```

### Delete employee
```bash
curl -X DELETE http://localhost:5080/api/employees/1
```

### Test validation error
```bash
curl -X POST http://localhost:5080/api/employees \
  -H "Content-Type: application/json" \
  -d '{"name":"","position":"","salary":-100}'
```

---

## Benefits of This Format

✅ **Consistent Structure** - All responses follow the same pattern  
✅ **Clear Status** - Easy to distinguish success from error  
✅ **Descriptive Messages** - Human-readable feedback  
✅ **Multiple Errors** - Can show all validation errors at once  
✅ **Easy to Parse** - Simple for frontend developers to handle  
✅ **RESTful** - Proper HTTP status codes
