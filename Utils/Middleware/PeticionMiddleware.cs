using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Utils.Response;
using Utils.Security;

namespace Utils.Middleware
{
    public class PeticionMiddleware : IMiddleware
    {
        public PeticionMiddleware()
        {

        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Stream OriginalResponseBody = context.Response.Body;
            #region Validacion de token
            if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                if (JWT.Expirado() || JWT.Valida())
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    try
                    {
                        using var mStream = new MemoryStream();
                        mStream.Position = 0;
                        string responseSerializado = JsonConvert.SerializeObject(new Respuesta<string>() { exitoso = false, mensaje = "Debe iniciar sesion" });
                        var streamRespuesta = new MemoryStream();
                        var writer = new StreamWriter(streamRespuesta);
                        writer.Write(responseSerializado);
                        writer.Flush();
                        streamRespuesta.Position = 0;
                        mStream.Position = 0;
                        await streamRespuesta.CopyToAsync(OriginalResponseBody);
                    }
                    finally
                    {
                        context.Response.Body = OriginalResponseBody;
                    }
                }
            }
            #endregion
            if (context.Response.StatusCode == StatusCodes.Status200OK)
            {
                await next(context);
            }
        }
    }
}
