using System;
using System.Collections.Generic;

namespace FoodIntelligence.Data.Models;

public partial class UsuariosAlergia
{
    public int Id { get; set; }

    public string? Idusuario { get; set; }

    public int? Idalergia { get; set; }

    public virtual Alergia? IdalergiaNavigation { get; set; }

    public virtual AspNetUser? IdusuarioNavigation { get; set; }
}
