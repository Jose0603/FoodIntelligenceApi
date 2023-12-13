using FoodIntelligence.Data.Models;
using FoodIntelligence.Data.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIntelligence.Data.Repositories.DetallesPedidoRepositories
{
    public class DetallesPedidoRepository : GenericRepository<DetallesPedido>, IDetallesPedidoRepository
    {
        public DetallesPedidoRepository(FIntelligenceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
