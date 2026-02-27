namespace EmployeeApp.Web.Models;

// API Response wrapper
public class ApiResponse<T>
{
    public string Status { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
}

// Error response from API
public class ErrorResponse
{
    public string Status { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public List<string>? Errors { get; set; }
}
