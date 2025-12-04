using Core.Interfaz;
using Infraestructura.DTO;


namespace Core.Logic
{
    public class NotificacionesLogic() : INotificacionesLogic
    {
        public async Task EnviarNotificacion(string email, NotificacionDTO notificacion)
        {
            //UsuarioConectadoDTO eUsuarioConectado = await _usuarioConectado.PorId(new UsuarioConectadoDTO() { Email = email });
            //if (eUsuarioConectado != null)
            //{
            //    if (eUsuarioConectado.Conexion != null)
            //    {
            //        await _hubContext.Clients.Client(eUsuarioConectado.Conexion).SendAsync("NotificacionLocal", email, JsonConvert.SerializeObject(notificacion));
            //    }
            //}
        }
    }
}