using System;
using System.Collections.Generic;


namespace FoodIntelligence.Data.DTOs;

public partial class CodigosVerificacionDto
{
    public int Id { get; set; }

    public string? UsuarioId { get; set; }

    public string Codigo { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaExpiracion { get; set; }

    public bool? Utilizado { get; set; }
}
