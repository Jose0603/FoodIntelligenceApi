

using FoodIntelligence.Data.Autentication;

namespace FoodIntelligence.Service.Services
{
    public interface IAuthService
    {
        Task<(int, string)> Registeration(RegistrationModel model, string role);
        Task<(int, object)> Login(LoginModel model);
    }
}
