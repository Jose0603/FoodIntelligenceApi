﻿
using FoodIntelligence.Data.Models;

namespace FoodIntelligence.Service.Services
{
    public interface IAuthService
    {
        Task<(int, string)> Registeration(RegistrationModel model, string role);
        Task<(int, string)> Login(LoginModel model);
    }
}
