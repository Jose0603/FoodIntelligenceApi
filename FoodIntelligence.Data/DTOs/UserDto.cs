
using Microsoft.AspNetCore.Identity;

namespace FoodIntelligence.Data.DTOs;

public partial class UserDto : IdentityUser
{

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

}
