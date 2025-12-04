using Core.Interfaz;
using Infraestructura.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utils.Response;

namespace Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ReportesController(IReportesLogic reporte) : ControllerBase
    {
        private readonly IReportesLogic _reporte = reporte;

        [HttpGet("Ventas/{idAnio}/{idMes}/{idDia}")]
        public async Task<Respuesta<ReporteVentas>> Ventas(short idAnio, short idMes, short idDia)
        {
            return await _reporte.Ventas(idAnio, idMes, idDia);
        }
    }
}
