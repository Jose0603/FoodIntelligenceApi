using System;
using System.Collections.Generic;

namespace FoodIntelligence.Data.Models;


public partial class ComidaEstimatedRating
{
    public long Id { get; set; }

    public string? UsuarioId { get; set; }

    public int? ComidaId { get; set; }

    public decimal? Rating { get; set; }
}
