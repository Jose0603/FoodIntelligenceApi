using System;
using System.Collections.Generic;

namespace FoodIntelligence.Data.DTOs;

public partial class DetallesPedidoDto
{
    public int Id { get; set; }

    public int? Idpedido { get; set; }

    public int? Idcomida { get; set; }
    public string? IdcomidaNavigationImagenComida { get; set; }
    public string? IdcomidaNavigationNombre { get; set; }

    public int? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

}
