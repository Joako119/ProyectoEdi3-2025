using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace GestionCompeticiones.WebAPI.Exceptions
{
    public class ManejadorExcepcionesMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ManejadorExcepcionesMiddleware> _logger;

        public ManejadorExcepcionesMiddleware(RequestDelegate next, ILogger<ManejadorExcepcionesMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode == StatusCodes.Status401Unauthorized ||
                    context.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    context.Response.ContentType = "application/json";
                    var payload = new
                    {
                        exito = false,
                        error = new
                        {
                            codigo = context.Response.StatusCode,
                            mensaje = context.Response.StatusCode == 401
                                ? "No autorizado"
                                : "Acceso prohibido"
                        }
                    };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción fuera de los parametros. \ncomuniquese con el Administrador!!");
                var (status, payload) = MapearExcepcion(ex);
                context.Response.StatusCode = status;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
            }
        }

        private static (int, object) MapearExcepcion(Exception ex) =>
            ex switch
            {
                ValidacionExcepcion ve => (400, new { exito = false, error = new { codigo = 400, mensaje = "Error de validación", detalles = ve.Errores } }),
                NoEncontradoExcepcion nf => (404, new { exito = false, error = new { codigo = 404, mensaje = nf.Message } }),
                AccesoExcepcion ae => (403, new { exito = false, error = new { codigo = 403, mensaje = ae.Message } }),
                ExcepcionDeDominio ed => (400, new { exito = false, error = new { codigo = 400, mensaje = ed.Message } }),
                _ => (500, new { exito = false, error = new { codigo = 500, mensaje = "Error interno del servidor" } })
            };
    }
}

public static class ManejadorExcepcionesExtensions
{
    public static IApplicationBuilder UsarManejadorExcepciones(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GestionCompeticiones.WebAPI.Exceptions.ManejadorExcepcionesMiddleware>();
    }
}
