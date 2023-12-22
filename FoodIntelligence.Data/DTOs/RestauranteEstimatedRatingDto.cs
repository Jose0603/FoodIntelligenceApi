using System;
using System.Collections.Generic;

namespace FoodIntelligence.Data.DTOs;

public partial class RestauranteEstimatedRatingDTO
{
    public int Id { get; set; }

    public string? UsuarioId { get; set; }

    public int? RestauranteId { get; set; }

    public decimal? Rating { get; set; }
}
