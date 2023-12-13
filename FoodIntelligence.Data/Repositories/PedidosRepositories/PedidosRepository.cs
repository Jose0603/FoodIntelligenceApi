using FoodIntelligence.Data.Models;
using FoodIntelligence.Data.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIntelligence.Data.Repositories.PedidosRepositories
{
    public class PedidosRepository : GenericRepository<Pedido>, IPedidosRepository
    {
        public PedidosRepository(FIntelligenceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
