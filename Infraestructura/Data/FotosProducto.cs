using System;
using System.Collections.Generic;

namespace Infraestructura.Data;

public partial class Fotosproducto
{
    public short Id { get; set; }

    public string? Foto { get; set; }

    public short Idproducto { get; set; }

    public DateTime? Registro { get; set; }

    public bool Activo { get; set; }

    public string? Nombre { get; set; }
}
