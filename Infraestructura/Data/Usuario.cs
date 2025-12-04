using System;
using System.Collections.Generic;

namespace Infraestructura.Data;

public partial class Usuario
{
    public string Email { get; set; } = null!;

    public string? Nombre { get; set; }

    public DateTime? Registro { get; set; }

    public DateTime? Ultimaconexion { get; set; }

    public short? Idperfil { get; set; }

    public bool Activo { get; set; }

    public string Codigo { get; set; } = null!;
}
