using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIntelligence.Data.Autentication
{
    public class UserInfoToken
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Token { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FullName { get { return FirstName + " " + LastName; } }

    }
}
