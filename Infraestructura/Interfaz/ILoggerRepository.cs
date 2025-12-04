namespace Infraestructura.Interfaz
{
    public interface ILoggerRepository
    {
        Task Registra(Exception ex);
        Task Registra(Exception ex, string mensajeAdicional);
    }
}
