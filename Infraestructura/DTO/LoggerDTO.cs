namespace Infraestructura.DTO
{
    public class LoggerDTO
    {
        public long Id { get; set; }
        public string? Url { get; set; } = null!;
        public string? Api { get; set; } = null!;
        public string? Mensaje { get; set; } = null!;
        public string? Stacktrace { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public DateTime Fecha { get; set; }
    }
}
