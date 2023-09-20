using System;
using System.Collections.Generic;

namespace FoodIntelligence.Data.Models;

public partial class MetodosDePago
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }
}
