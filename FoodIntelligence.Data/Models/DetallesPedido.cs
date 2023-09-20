using System;
using System.Collections.Generic;

namespace FoodIntelligence.Data.Models;

public partial class DetallesPedido
{
    public int Id { get; set; }

    public int? Idpedido { get; set; }

    public int? Idcomida { get; set; }

    public int? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public virtual Comidum? IdcomidaNavigation { get; set; }

    public virtual Pedido? IdpedidoNavigation { get; set; }
}
