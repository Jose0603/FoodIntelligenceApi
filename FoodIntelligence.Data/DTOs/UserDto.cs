
using Microsoft.AspNetCore.Identity;

namespace FoodIntelligence.Data.DTOs;

public partial class UserDto
{
    public string Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
}
