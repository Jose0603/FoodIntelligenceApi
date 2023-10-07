﻿using System;
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

    public TimeSpan? HoraEntregaEstimada { get; set; }

}