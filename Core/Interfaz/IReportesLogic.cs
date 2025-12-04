using Infraestructura.DTO;
using Infraestructura.ViewModel;
using Utils.Response;

namespace Core.Interfaz
{
    public interface IReportesLogic
    {
        Task<Respuesta<ReporteVentas>> Ventas(short anio, short mes, short dia);
    }
}
