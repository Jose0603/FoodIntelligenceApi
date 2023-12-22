﻿using FoodIntelligence.Data.Models;
using FoodIntelligence.Data.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIntelligence.Data.Repositories.ComidaEstimatedRatingRepositories
{
    public class ComidaEstimatedRatingRepository : GenericRepository<ComidaEstimatedRating>, IComidaEstimatedRatingRepository
    {
        public ComidaEstimatedRatingRepository(FIntelligenceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
