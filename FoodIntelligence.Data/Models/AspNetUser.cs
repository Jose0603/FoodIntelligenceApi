
using Microsoft.AspNetCore.Identity;

namespace FoodIntelligence.Data.Models;

public partial class AspNetUsers : IdentityUser
{

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;


    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<UsuariosAlergia> UsuariosAlergia { get; set; } = new List<UsuariosAlergia>();

}
