using System;
using System.Collections.Generic;

namespace Infraestructura.Data;

public partial class Pago
{
    public long Id { get; set; }

    public short? Idorden { get; set; }

    public string Metodo { get; set; } = null!;

    public decimal Porcentaje { get; set; }

    public decimal Monto { get; set; }

    public string? Referencia { get; set; }

    public string? Email { get; set; }

    public DateTime? Fecha { get; set; }
}
