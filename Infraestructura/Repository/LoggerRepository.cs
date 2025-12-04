using Infraestructura.DTO;
using Infraestructura.Interfaz;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Utils.Peticiones;
using Utils.Security;

namespace Infraestructura.Repository
{
    public class LoggerRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : ILoggerRepository
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public Task Registra(Exception ex)
        {
            HttpRequest? request = _httpContextAccessor.HttpContext?.Request;
            LoggerDTO logger = new()
            {
                Url = request != null ? $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}" : string.Empty,
                Api = _configuration["Keys:NombreApi"] ?? string.Empty,
                Mensaje = ex.Message,
                Stacktrace = Utils.helpers.ExceptionHelper.GetFullExceptionDetails(ex),
                Email = JWT.ObtieneEmail(),
                Fecha = DateTime.Now
            };

            Task.Run(async () => { await Http<bool>.ConsumirApiAsync((_configuration["Recursos:Logger"] ?? string.Empty) + "Logger", HttpMethod.Post, logger); });
            return Task.CompletedTask;
        }
        public Task Registra(Exception ex, string mensajeAdicional)
        {
            HttpRequest? request = _httpContextAccessor.HttpContext?.Request;
            LoggerDTO logger = new()
            {
                Url = request != null ? $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}" : string.Empty,
                Api = _configuration["Keys:NombreApi"] ?? string.Empty,
                Mensaje = ex.Message,
                Stacktrace = mensajeAdicional + " --> " + Utils.helpers.ExceptionHelper.GetFullExceptionDetails(ex),
                Email = JWT.ObtieneEmail(),
                Fecha = DateTime.Now
            };

            Task.Run(async () => { await Http<bool>.ConsumirApiAsync((_configuration["Recursos:Logger"] ?? string.Empty) + "Logger", HttpMethod.Post, logger); });
            return Task.CompletedTask;
        }
    }
}
