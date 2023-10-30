using FoodIntelligence.Data.Models;
using FoodIntelligence.Data.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIntelligence.Data.Repositories.RestaurantesRepositories
{
    public class RestaurantesRepository : GenericRepository<Restaurante>, IRestaurantesRepository
    {
        public RestaurantesRepository(FIntelligenceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
