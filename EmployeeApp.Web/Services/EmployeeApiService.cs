using System.Text;
using System.Text.Json;
using EmployeeApp.Web.Models;

namespace EmployeeApp.Web.Services;

public class EmployeeApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl;

    public EmployeeApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiBaseUrl = configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5000";
    }

    // Get all employees
    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/employees");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<Employee>>>(json, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return apiResponse?.Data ?? new List<Employee>();
    }

    // Get employee by ID
    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/employees/{id}");
        
        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<ApiResponse<Employee>>(json, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return apiResponse?.Data;
    }

    // Create employee
    public async Task<(bool Success, string Message, List<string>? Errors)> CreateEmployeeAsync(Employee employee)
    {
        var json = JsonSerializer.Serialize(employee);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/employees", content);
        var responseJson = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<Employee>>(responseJson, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return (true, apiResponse?.Message ?? "Employee created successfully", null);
        }
        else
        {
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseJson, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return (false, errorResponse?.Message ?? "Failed to create employee", errorResponse?.Errors);
        }
    }

    // Update employee
    public async Task<(bool Success, string Message, List<string>? Errors)> UpdateEmployeeAsync(int id, Employee employee)
    {
        var json = JsonSerializer.Serialize(employee);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"{_apiBaseUrl}/api/employees/{id}", content);
        var responseJson = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<Employee>>(responseJson, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return (true, apiResponse?.Message ?? "Employee updated successfully", null);
        }
        else
        {
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseJson, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return (false, errorResponse?.Message ?? "Failed to update employee", errorResponse?.Errors);
        }
    }

    // Delete employee
    public async Task<(bool Success, string Message)> DeleteEmployeeAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/api/employees/{id}");
        var responseJson = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return (true, "Employee deleted successfully");
        }
        else
        {
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseJson, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return (false, errorResponse?.Message ?? "Failed to delete employee");
        }
    }
}
