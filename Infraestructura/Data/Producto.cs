using System;
using System.Collections.Generic;

namespace Infraestructura.Data;

public partial class Producto
{
    public short Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcionextensa { get; set; }

    public string? Descripcioncorta { get; set; }

    public decimal Preciocosto { get; set; }

    public decimal Precioventa { get; set; }

    public DateTime Registro { get; set; }

    public DateTime Actualizado { get; set; }

    public short Idcategoria { get; set; }

    public bool Activo { get; set; }
}
