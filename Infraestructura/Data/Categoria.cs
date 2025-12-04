using System;
using System.Collections.Generic;

namespace Infraestructura.Data;

public partial class Categoria
{
    public short Id { get; set; }

    public string? Nombre { get; set; }

    public bool? Activo { get; set; }

    public bool Cocina { get; set; }
}
