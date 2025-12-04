using Infraestructura.DTO;

namespace Core.Interfaz
{
    public interface INotificacionesLogic
    {
        Task EnviarNotificacion(string email, NotificacionDTO notificacion);
    }
}
