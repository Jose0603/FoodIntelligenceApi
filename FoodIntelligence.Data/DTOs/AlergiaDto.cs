using System;
using System.Collections.Generic;

namespace FoodIntelligence.Data.DTOs;

public partial class AlergiaDto
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }
}
