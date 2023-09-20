using System;
using System.Collections.Generic;

namespace FoodIntelligence.Data.Models;

public partial class CategoriasComidum
{
    public int Id { get; set; }

    public string? NombreCategoria { get; set; }

    public virtual ICollection<Comidum> Comida { get; set; } = new List<Comidum>();
}
