using FoodIntelligence.Data.Models;
using FoodIntelligence.Data.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIntelligence.Data.Repositories.CategoriasComidaRepositories
{
    public class CategoriasComidaRepository : GenericRepository<CategoriasComidum>, ICategoriasComidaRepository
    {
        public CategoriasComidaRepository(FIntelligenceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
