using System;
using System.Collections.Generic;

namespace Infraestructura.Data;

public partial class Perfil
{
    public short Id { get; set; }

    public string? Nombre { get; set; }

    public bool? Activo { get; set; }
}
