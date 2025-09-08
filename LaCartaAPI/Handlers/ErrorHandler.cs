// Handlers/ErrorHandler.cs

using Domain.Modules.Restaurants.Exceptions;
using Domain.Modules.Shared.Exceptions;
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
            "ArgumentException" or "ValidationException" or "InvalidTimeRangeException" => 400, // ✅ Agregado
            "NotUniqueEmailException" or "NotUniquePhoneNumber" or "NotUniqueNameException" => 422,
            "UnauthorizedAccessException" => 401,
            "ForbiddenAccessException" or "InvalidUserRoleException" => 403,
            _ => 500
        };

        string message = statusCode switch
        {
            404 => "Recurso no encontrado.",
            400 => ex is InvalidTimeRangeException
                ? ex.Message 
                : "Solicitud inválida.",
            401 => "No autorizado.",
            403 => ex is InvalidUserRoleException
                ? ex.Message
                : "Acceso denegado.",
            422 => ex is NotUniqueEmailException
                ? ex.Message
                : ex is NotUniquePhoneNumber
                    ? ex.Message
                    : ex is NotUniqueNameException
                        ? ex.Message
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
                "InvalidUserRoleException" => "INVALID_USER_ROLE",
                "InvalidTimeRangeException" => "INVALID_TIME_RANGE",
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