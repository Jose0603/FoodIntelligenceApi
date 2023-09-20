using System;
using System.Collections.Generic;

namespace FoodIntelligence.Data.Models;

public partial class Ingrediente
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<ComidaIngrediente> ComidaIngredientes { get; set; } = new List<ComidaIngrediente>();
}
