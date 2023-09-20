using System;
using System.Collections.Generic;

namespace FoodIntelligence.Data.Models;

public partial class Alergia
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<UsuariosAlergia> UsuariosAlergia { get; set; } = new List<UsuariosAlergia>();
}
