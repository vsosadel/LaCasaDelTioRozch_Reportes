using Core.Interfaz;
using Infraestructura.Interfaz;
using Infraestructura.ViewModel;
using Utils.Response;

namespace Core.Logic
{
    public class ReportesLogic(ILoggerRepository logger, IReporteRepository reporte) : IReportesLogic
    {
        private readonly ILoggerRepository _logger = logger;
        private readonly IReporteRepository _reporte = reporte;

        public async Task<Respuesta<ReporteVentas>> Ventas(short anio, short mes, short dia)
        {
            Respuesta<ReporteVentas> _Respuesta = new();
            try
            {
                _Respuesta.lsCuerpo = await _reporte.Ventas(anio, mes, dia);
                _Respuesta.mensaje = "¡Reporte de ventas consultado exitosamente!";
                _Respuesta.exitoso = true;

            }
            catch (Exception ex)
            {
                await _logger.Registra(ex);
                _Respuesta.mensaje = "¡Sucedio un error inesperado, ya fue notificado al administrador.!";
            }

            return _Respuesta;
        }
    }
}
