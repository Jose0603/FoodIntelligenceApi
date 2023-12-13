using AutoMapper;
using FoodIntelligence.Data.DTOs;
using FoodIntelligence.Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIntelligence.Service
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<CategoriasComidum, CategoriasComidumDto>().ReverseMap();
            CreateMap<Restaurante, RestauranteDto>().ReverseMap();
            CreateMap<Comidum, ComidumDto>().ReverseMap();
            CreateMap<Pedido, PedidoDto>().ReverseMap();
            CreateMap<DetallesPedido, DetallesPedidoDto>().ReverseMap();

        }
    }
}
