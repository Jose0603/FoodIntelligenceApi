using System;
using System.Collections.Generic;

namespace FoodIntelligence.Data.Models;

public partial class Pedido
{
    public int Id { get; set; }

    public string? Idusuario { get; set; }

    public DateTime? FechaHoraPedido { get; set; }

    public string? EstadoPedido { get; set; }

    public string? DireccionEntrega { get; set; }

    public decimal? MontoTotal { get; set; }

    public bool? Entrega { get; set; }

    public TimeSpan? HoraEntregaEstimada { get; set; }

    public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();

   public virtual AspNetUsers? IdusuarioNavigation { get; set; }
}
