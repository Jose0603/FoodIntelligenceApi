using FoodIntelligence.Data.Repositories.BaseRepositories;
using FoodIntelligence.Data.Repositories.CategoriasComidaRepositories;
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
        IUserRepository UserRepository { get; }

        //int Save();
        Task<int> CommitAsync(CancellationToken cancellationToken);
        Task<int> CommitAsync();
    }
}
