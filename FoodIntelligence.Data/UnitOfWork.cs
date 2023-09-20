using FoodIntelligence.Data.Repositories.BaseRepositories;
using FoodIntelligence.Data.Repositories.CategoriasComidaRepositories;
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
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public ICategoriasComidaRepository CategoriasComidaRepository { get; private set; }

        public Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return _databaseContext.SaveChangesAsync(cancellationToken);
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
