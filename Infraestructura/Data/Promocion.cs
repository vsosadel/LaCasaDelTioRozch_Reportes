using System;
using System.Collections.Generic;

namespace Infraestructura.Data;

public partial class Promocion
{
    public short Id { get; set; }

    public string Nombre { get; set; } = null!;

    public short Idproducto { get; set; }

    public decimal Precioventa { get; set; }

    public DateTime Inicio { get; set; }

    public DateTime Fin { get; set; }

    public bool Activo { get; set; }

    public short? Cantidad { get; set; }

    public bool? Porprecio { get; set; }

    public bool? Porcantidad { get; set; }
}
