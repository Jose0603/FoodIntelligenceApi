using Microsoft.AspNetCore.Identity;

namespace FoodIntelligence.Data.Autentication;

public class ApplicationUser: IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
