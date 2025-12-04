using System;
using System.Collections.Generic;

namespace Infraestructura.Data;

public partial class CanalVenta
{
    public short Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Descuento { get; set; }

    public bool Activo { get; set; }
}
