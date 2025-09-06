using FastEndpoints;
using System.Text.Json;

namespace LaCartaAPI.Handlers;

public static class ErrorHandler
{
    public static async Task HandleExceptionAsync<TRequest, TResponse>(
    Exception ex,
    ResponseSender<TRequest, TResponse> sender,
    CancellationToken ct)
    where TRequest : notnull
    {
        int statusCode = ex.GetType().Name switch
        {
            "EntityNotFoundException" or "KeyNotFoundException" => 404,
            "ArgumentException" or "ValidationException" => 400,
            "UnauthorizedAccessException" => 401,
            "ForbiddenAccessException" => 403,
            _ => 500
        };

        string message = statusCode switch
        {
            404 => "Recurso no encontrado.",
            400 => "Solicitud inválida.",
            401 => "No autorizado.",
            403 => "Acceso denegado.",
            _ => "Ha ocurrido un error inesperado. Intente más tarde."
        };

        var errorResponse = new
        {
            StatusCode = statusCode,
            Message = message,
            ErrorCode = ex.GetType().Name switch
            {
                "EntityNotFoundException" => "NOT_FOUND",
                "ValidationException" => "VALIDATION_ERROR",
                _ => "INTERNAL_ERROR"
            }
        };

        // Serialize the error response to JSON and send it as a string
        string jsonResponse = JsonSerializer.Serialize(errorResponse);
        await sender.StringAsync(jsonResponse, statusCode, "application/json; charset=utf-8", ct);
    }
}
