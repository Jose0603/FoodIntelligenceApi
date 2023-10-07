using System;
using System.Collections.Generic;

namespace FoodIntelligence.Data.DTOs;

public partial class ComidumDto
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

}
