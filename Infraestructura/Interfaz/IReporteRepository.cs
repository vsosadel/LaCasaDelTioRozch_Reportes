using Infraestructura.ViewModel;

namespace Infraestructura.Interfaz
{
    public interface IReporteRepository
    {
        Task<List<ReporteVentas>> Ventas(short idAnio, short idMes, short idDia);
    }
}
