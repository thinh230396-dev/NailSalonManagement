namespace NailSalon.Application.Common.Models;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = new();
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string TraceId { get; set; } = Guid.NewGuid().ToString();

    public static ApiResponse<T> Ok(
        T data,
        string message = "Success")
    {
        return new ApiResponse<T>
        {
            Success = true,
            StatusCode = 200,
            Message = message,
            Data = data
        };
    }

    public static ApiResponse<T> Created(
        T data,
        string message = "Created successfully")
    {
        return new ApiResponse<T>
        {
            Success = true,
            StatusCode = 201,
            Message = message,
            Data = data
        };
    }

    public static ApiResponse<T> Fail(
        string message,
        int statusCode = 400)
    {
        return new ApiResponse<T>
        {
            Success = false,
            StatusCode = statusCode,
            Message = message
        };
    }

    public static ApiResponse<T> ValidationFail(
        IEnumerable<string> errors,
        string message = "Validation failed")
    {
        return new ApiResponse<T>
        {
            Success = false,
            StatusCode = 400,
            Message = message,
            Errors = errors.ToList()
        };
    }
}