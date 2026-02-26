using EmployeeApp.Api.Models;
using EmployeeApp.Api.Responses;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Api.Endpoints;

public static class EmployeeEndpoints
{
    public static void MapEmployeeEndpoints(this WebApplication app)
    {
        // Root endpoint
        app.MapGet("/", GetRoot);

        // Employee endpoints
        app.MapGet("/api/employees", GetAllEmployees);
        app.MapGet("/api/employees/{id}", GetEmployeeById);
        app.MapPost("/api/employees", CreateEmployee);
        app.MapPut("/api/employees/{id}", UpdateEmployee);
        app.MapDelete("/api/employees/{id}", DeleteEmployee);
    }

    // GET / - Root endpoint
    private static IResult GetRoot()
    {
        return Results.Ok(new ApiResponse<string>(
            data: "Employee API is running",
            message: "Welcome to Employee API"
        ));
    }

    // GET /api/employees - Get all employees
    private static async Task<IResult> GetAllEmployees(AppDbContext db)
    {
        var employees = await db.Employees.ToListAsync();
        return Results.Ok(new ApiResponse<List<Employee>>(
            data: employees,
            message: "Employees retrieved successfully"
        ));
    }

    // GET /api/employees/{id} - Get single employee
    private static async Task<IResult> GetEmployeeById(int id, AppDbContext db)
    {
        var employee = await db.Employees.FindAsync(id);

        if (employee is null)
            return Results.NotFound(new ErrorResponse("Employee not found"));

        return Results.Ok(new ApiResponse<Employee>(
            data: employee,
            message: "Employee retrieved successfully"
        ));
    }

    // POST /api/employees - Create new employee
    private static async Task<IResult> CreateEmployee(Employee employee, AppDbContext db)
    {
        // Validate input
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(employee.Name))
            errors.Add("Name is required");

        if (string.IsNullOrWhiteSpace(employee.Position))
            errors.Add("Position is required");

        if (employee.Salary <= 0)
            errors.Add("Salary must be greater than zero");

        if (errors.Any())
            return Results.BadRequest(new ErrorResponse(
                message: "Validation failed",
                errors: errors
            ));

        // Add to database
        db.Employees.Add(employee);
        await db.SaveChangesAsync();

        return Results.Created($"/api/employees/{employee.Id}", new ApiResponse<Employee>(
            data: employee,
            message: "Employee created successfully"
        ));
    }

    // PUT /api/employees/{id} - Update employee
    private static async Task<IResult> UpdateEmployee(int id, Employee updatedEmployee, AppDbContext db)
    {
        var employee = await db.Employees.FindAsync(id);
        if (employee is null)
            return Results.NotFound(new ErrorResponse("Employee not found"));

        // Validate input
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(updatedEmployee.Name))
            errors.Add("Name is required");

        if (string.IsNullOrWhiteSpace(updatedEmployee.Position))
            errors.Add("Position is required");

        if (updatedEmployee.Salary <= 0)
            errors.Add("Salary must be greater than zero");

        if (errors.Any())
            return Results.BadRequest(new ErrorResponse(
                message: "Validation failed",
                errors: errors
            ));

        // Update properties
        employee.Name = updatedEmployee.Name;
        employee.Position = updatedEmployee.Position;
        employee.Salary = updatedEmployee.Salary;

        await db.SaveChangesAsync();

        return Results.Ok(new ApiResponse<Employee>(
            data: employee,
            message: "Employee updated successfully"
        ));
    }

    // DELETE /api/employees/{id} - Delete employee
    private static async Task<IResult> DeleteEmployee(int id, AppDbContext db)
    {
        var employee = await db.Employees.FindAsync(id);
        if (employee is null)
            return Results.NotFound(new ErrorResponse("Employee not found"));

        db.Employees.Remove(employee);
        await db.SaveChangesAsync();

        return Results.Ok(new ApiResponse<object>(
            data: new { id = id },
            message: "Employee deleted successfully"
        ));
    }
}
