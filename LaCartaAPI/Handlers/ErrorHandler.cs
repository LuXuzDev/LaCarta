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
            "NotUniqueEmailException" or "NotUniquePhoneNumber" or "NotUniqueNameException" => 422,
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

            422 => ex is Domain.Modules.Shared.Exceptions.NotUniqueEmailException
                ? "El correo electrónico ya está en uso."
                : ex is Domain.Modules.Shared.Exceptions.NotUniquePhoneNumber
                    ? "El número de teléfono ya está en uso."
                : ex is Domain.Modules.Restaurants.Exceptions.NotUniqueNameException
                    ? "El nombre ya está en uso."
                : "Entrada no válida.",

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
                "NotUniqueEmailException" => "NOT_UNIQUE_EMAIL",
                "NotUniquePhoneNumber" => "NOT_UNIQUE_PHONE_NUMBER",
                "NotUniqueNameException" => "NOT_UNIQUE_NAME",
                _ => "INTERNAL_ERROR"
            },
            ExceptionType = ex.GetType().FullName,
            ExceptionMessage = ex.Message,
            StackTrace = ex.StackTrace
        };

        string jsonResponse = JsonSerializer.Serialize(errorResponse);
        await sender.StringAsync(jsonResponse, statusCode, "application/json; charset=utf-8", ct);
    }



}
