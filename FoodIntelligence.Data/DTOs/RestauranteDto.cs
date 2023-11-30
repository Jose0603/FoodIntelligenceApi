using System;
using System.Collections.Generic;

namespace FoodIntelligence.Data.DTOs;

public partial class RestauranteDto
{
    public int Id { get; set; }

    public string? NombreRestaurante { get; set; }

    public string? DireccionRestaurante { get; set; }

    public string? TelefonoRestaurante { get; set; }

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }

    public string? LogoRestaurante { get; set; }
    public decimal Rating { get; set; }
}
