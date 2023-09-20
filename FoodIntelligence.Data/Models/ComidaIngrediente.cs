using System;
using System.Collections.Generic;

namespace FoodIntelligence.Data.Models;

public partial class ComidaIngrediente
{
    public int Id { get; set; }

    public int? Idcomida { get; set; }

    public int? Idingrediente { get; set; }

    public virtual Comidum? IdcomidaNavigation { get; set; }

    public virtual Ingrediente? IdingredienteNavigation { get; set; }
}
