namespace Infraestructura.ViewModel
{
    public class ReporteVentas
    {
        public string? Canal { get; set; }
        public string? Categoria { get; set; }
        public string? Nombre { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }
        public decimal Ganancia { get; set; }
        public string? Estado { get; set; }
    }
}
