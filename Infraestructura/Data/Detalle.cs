using System;
using System.Collections.Generic;

namespace Infraestructura.Data;

public partial class Detalle
{
    public long Id { get; set; }

    public short Producto { get; set; }

    public short Promocion { get; set; }

    public List<short> Adicionales { get; set; } = null!;

    public short Cantidad { get; set; }

    public decimal Precio { get; set; }

    public decimal Total { get; set; }

    public DateTime? Solicitado { get; set; }

    public DateTime? Entregado { get; set; }

    public string? Observaciones { get; set; }

    public bool Estado { get; set; }

    public short? Idorden { get; set; }

    public bool Comanda { get; set; }

    public long? Iddetalleorigen { get; set; }
}
