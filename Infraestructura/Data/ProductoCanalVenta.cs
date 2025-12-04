using System;
using System.Collections.Generic;

namespace Infraestructura.Data;

public partial class ProductoCanalVenta
{
    public short Idproducto { get; set; }

    public short Idcanalventa { get; set; }

    public decimal Precio { get; set; }

    public bool Activo { get; set; }
}
