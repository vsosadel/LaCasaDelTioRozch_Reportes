using System;
using System.Collections.Generic;

namespace Infraestructura.Data;

public partial class Orden
{
    public short Id { get; set; }

    public string? Mesero { get; set; }

    public short? Mesa { get; set; }

    public string? Nombre { get; set; }

    public string? Observaciones { get; set; }

    public bool? Pagada { get; set; }

    public bool? Estado { get; set; }

    public DateTime? Apertura { get; set; }

    public DateTime? Cierre { get; set; }

    public short Personas { get; set; }

    public bool Ticket { get; set; }

    public bool Cajon { get; set; }

    public short Idcanalventa { get; set; }

    public decimal Descuentocanalventa { get; set; }
}
