using System;
using System.Collections.Generic;

namespace FoodIntelligence.Data.Models;

public partial class RestauranteEstimatedRating
{
    public int Id { get; set; }

    public string? UsuarioId { get; set; }

    public int? RestauranteId { get; set; }

    public decimal? Rating { get; set; }
}
