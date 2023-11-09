using FoodIntelligence.Data.Repositories.CategoriasComidaRepositories;
using FoodIntelligence.Data.Repositories.ComidasRepositories;
using FoodIntelligence.Data.Repositories.RestaurantesRepositories;
using FoodIntelligence.Data.Repositories.UserRepositories;
using FoodIntelligence.Data.Repositories.UserRepositoryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIntelligence.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FIntelligenceDbContext _databaseContext;
        private bool _disposed;

        public UnitOfWork(FIntelligenceDbContext databaseContext)
        {
            _databaseContext = databaseContext;
            CategoriasComidaRepository = new CategoriasComidaRepository(databaseContext);
            RestaurantesRepository = new RestaurantesRepository(databaseContext);
            ComidasRepository = new ComidasRepository(databaseContext);
            UserRepository = new UserRepository(databaseContext);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public ICategoriasComidaRepository CategoriasComidaRepository { get; private set; }
        public IRestaurantesRepository RestaurantesRepository { get; private set; }
        public IComidasRepository ComidasRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }


        public Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return _databaseContext.SaveChangesAsync(cancellationToken);
        }
        public Task<int> CommitAsync()
        {
            return _databaseContext.SaveChangesAsync();
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _databaseContext.Dispose();
            _disposed = true;
        }
    }
}
