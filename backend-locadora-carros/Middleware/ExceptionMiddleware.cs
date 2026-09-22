using LocadoraCarrosBackEnd.Exceptions;
using LocadoraCarrosBackEnd.Utils;
using System.Net;
using System.Text.Json;

namespace LocadoraCarrosBackEnd.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Chama o próximo middleware
            }
            catch (RegraNegocioException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Regra de negócio violada. Path: {Path}",
                    context.Request.Path);

                await HandleExceptionAsync(
                    context,
                    HttpStatusCode.Conflict,
                    ex.Message);
            }
            catch (Exception ex)
            {
                //TODO Enteder melhor logger
                /*
                // Opcional: logar o erro para análise posterior
                _logger.LogError(ex, ex.Message);
                
                // Opcional: logar o erro para análise posterior
                Console.WriteLine($"Erro capturado: {ex.Message}");
                */

                _logger.LogError(
                 ex,
                 "Erro não tratado. Path: {Path}",
                 context.Request.Path);

                var details = _env.IsDevelopment()
                    ? ex.StackTrace
                    : null;

                await HandleExceptionAsync(
                    context,
                    HttpStatusCode.InternalServerError,
                    "Ocorreu um erro interno no servidor.",
                    details);

            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            HttpStatusCode statusCode,
            string message,
            string? details = null)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var response = new ApiException(
                (int)statusCode,
                message,
                details);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}