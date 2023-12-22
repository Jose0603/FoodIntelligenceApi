using FoodIntelligence.Data.DTOs;
using FoodIntelligence.Service.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIntelligence.Service.Services.ComidaEstimatedRatingServices
{
    public interface IComidaEstimatedRatingService
    {
        Task<CustomHttpResponse> CreateAll();
    }
}
