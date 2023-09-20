using System;
using System.Collections.Generic;

namespace FoodIntelligence.Data.Models;

public partial class Comidum
{
    public int Id { get; set; }

    public int? Idrestaurante { get; set; }

    public int? CategoriaId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public int? Calorias { get; set; }

    public decimal? Descuento { get; set; }

    public TimeSpan? HoraDisponible { get; set; }

    public string? ImagenComida { get; set; }

    public virtual CategoriasComidum? Categoria { get; set; }

    public virtual ICollection<ComidaIngrediente> ComidaIngredientes { get; set; } = new List<ComidaIngrediente>();

    public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();

    public virtual Restaurante? IdrestauranteNavigation { get; set; }
}
