using FoodIntelligence.Data.Models;
using FoodIntelligence.Data.Repositories.BaseRepositories;
using FoodIntelligence.Data.Repositories.UserRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIntelligence.Data.Repositories.UserRepositoryRepositories
{
    public class UserRepository : GenericRepository<AspNetUsers>, IUserRepository
    {
        public UserRepository(FIntelligenceDbContext dbContext) : base(dbContext)
        {
        }

        
    }
}
