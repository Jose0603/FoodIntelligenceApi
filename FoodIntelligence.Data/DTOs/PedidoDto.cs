using FoodIntelligence.Data.Models;
using System;
using System.Collections.Generic;

namespace FoodIntelligence.Data.DTOs;

public partial class PedidoDto
{
    public int Id { get; set; }

    public string? Idusuario { get; set; }

    public DateTime? FechaHoraPedido { get; set; }

    public string? EstadoPedido { get; set; }

    public string? DireccionEntrega { get; set; }

    public decimal? MontoTotal { get; set; }

    public bool? Entrega { get; set; }
    public int CantidadTotal { get; set; }
    public TimeSpan? HoraEntregaEstimada { get; set; }
    public int? RestauranteId { get; set; }
    public string RestauranteName { get; set; }
    public string RestauranteImagen { get; set; }
    public List<DetallesPedidoDto> DetallesPedido { get; set; } = new List<DetallesPedidoDto>();


}
