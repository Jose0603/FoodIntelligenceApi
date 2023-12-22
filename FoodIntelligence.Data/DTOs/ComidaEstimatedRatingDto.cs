using System;
using System.Collections.Generic;

namespace FoodIntelligence.Data.DTOs;


public partial class ComidaEstimatedRatingDTO
{
    public int Id { get; set; }

    public string? UsuarioId { get; set; }

    public int? ComidaId { get; set; }

    public decimal? Rating { get; set; }
}
