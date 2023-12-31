﻿using FoodIntelligence.Data.Repositories.BaseRepositories;
using FoodIntelligence.Data.Repositories.CategoriasComidaRepositories;
using FoodIntelligence.Data.Repositories.ComidaEstimatedRatingRepositories;
using FoodIntelligence.Data.Repositories.ComidasRepositories;
using FoodIntelligence.Data.Repositories.DetallesPedidoRepositories;
using FoodIntelligence.Data.Repositories.PedidosRepositories;
using FoodIntelligence.Data.Repositories.RestauranteEstimatedRatingRepositories;
using FoodIntelligence.Data.Repositories.RestaurantesRepositories;
using FoodIntelligence.Data.Repositories.UserRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIntelligence.Data
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoriasComidaRepository CategoriasComidaRepository { get; }
        IRestaurantesRepository RestaurantesRepository { get; }
        IComidasRepository ComidasRepository { get; }
        IComidaEstimatedRatingRepository ComidaEstimatedRatingRepository { get; }
        IRestauranteEstimatedRatingRepository RestauranteEstimatedRatingRepository { get; }
        IPedidosRepository PedidosRepository { get; }
        IDetallesPedidoRepository DetallesPedidoRepository { get; }


        IUserRepository UserRepository { get; }

        //int Save();
        Task<int> CommitAsync(CancellationToken cancellationToken);
        Task<int> CommitAsync();
    }
}
