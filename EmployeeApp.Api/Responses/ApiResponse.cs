namespace EmployeeApp.Api.Responses;

// Success response with data
public class ApiResponse<T>
{
    public string Status { get; set; } = "success";
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public ApiResponse(T data, string message = "")
    {
        Data = data;
        Message = message;
    }
}

// Error response
public class ErrorResponse
{
    public string Status { get; set; } = "error";
    public string Message { get; set; } = string.Empty;
    public List<string>? Errors { get; set; }

    public ErrorResponse(string message, List<string>? errors = null)
    {
        Message = message;
        Errors = errors;
    }
}
