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
            CreateMap<CategoriasComidum, CategoriasComidumDto>();
            CreateMap<CategoriasComidumDto, CategoriasComidum>();

            CreateMap<Restaurante, RestauranteDto>();
            CreateMap<RestauranteDto, Restaurante>();

            CreateMap<ComidumDto, Comidum>();
            CreateMap<Comidum, ComidumDto>();

            CreateMap<PedidoDto, Pedido>();
            CreateMap<Pedido, PedidoDto>();

            CreateMap<DetallesPedido, DetallesPedidoDto>();
            CreateMap<DetallesPedidoDto, DetallesPedido>();

        }
    }
}
