using System;
using System.Collections.Generic;

namespace Infraestructura.Data;

public partial class Empresa
{
    public short Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Saludo { get; set; } = null!;
}
