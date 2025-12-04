namespace Infraestructura.Data;

public partial class AdicionalesProducto
{
    public short Id { get; set; }

    public string Nombre { get; set; } = null!;

    public short Idproducto { get; set; }

    public decimal Preciocosto { get; set; }

    public decimal Precioventa { get; set; }

    public bool Activo { get; set; }
}
