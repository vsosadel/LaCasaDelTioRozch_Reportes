using Infraestructura.Data;
using Infraestructura.Interfaz;
using Infraestructura.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repository
{
    public class ReportesRepository(ReportesContext context) : IReporteRepository
    {
        private readonly ReportesContext _context = context;

        public async Task<List<ReporteVentas>> Ventas(short idAnio, short idMes, short idDia)
        {
            return await _context.Set<ReporteVentas>().FromSqlInterpolated($@"
                                                                            WITH params AS (
                                                                                SELECT {idAnio} AS idAnio, {idMes} AS idMes, {idDia} AS idDia
                                                                            )
                                                                            SELECT e.nombre AS canal,
                                                                                   d.nombre AS categoria,
                                                                                   c.nombre,
                                                                                   c.precioCosto,
                                                                                   c.precioVenta,
                                                                                   SUM(b.cantidad) AS cantidad,
                                                                                   b.precio,
                                                                                   SUM(b.total) AS total,
                                                                                   CASE
                                                                                       WHEN b.estado = true THEN (SUM(b.total) - (c.precioCosto * SUM(b.cantidad)))
                                                                                       ELSE 0
                                                                                   END AS ganancia,
                                                                                   CASE WHEN b.estado = true THEN 'Vendido' ELSE 'Cancelado' END AS estado
                                                                            FROM fdw_restaurante.orden a
                                                                            JOIN fdw_restaurante.detalle b ON b.idOrden = a.id
                                                                            JOIN fdw_catalogos.producto c ON c.id = b.producto
                                                                            JOIN fdw_catalogos.categoria d ON d.id = c.idcategoria
                                                                            JOIN fdw_catalogos.canalventa e ON e.id = a.idcanalventa
                                                                            JOIN params p ON true
                                                                            WHERE a.estado = false
                                                                              AND EXTRACT(YEAR FROM a.apertura) = p.idAnio
                                                                              AND EXTRACT(MONTH FROM a.apertura) = p.idMes
                                                                              AND (p.idDia = 0 OR EXTRACT(DAY FROM a.apertura) = p.idDia)
                                                                            GROUP BY e.nombre, d.nombre, c.nombre, c.precioCosto, c.precioVenta, b.precio, b.estado
                                                                            ORDER BY e.nombre, d.nombre, c.nombre;
                                                                        ").ToListAsync();
        }
    }
}
