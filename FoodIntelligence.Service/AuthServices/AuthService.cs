using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using FoodIntelligence.Data.Autentication;
using FoodIntelligence.Data;
using FoodIntelligence.Data.Models;

namespace FoodIntelligence.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly FIntelligenceDbContext _context;
        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, FIntelligenceDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            this._context = context;
        }
        public async Task<(int, string)> Registeration(RegistrationModel model, string role)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return (0, "User already exists");

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
            };
            var createUserResult = await userManager.CreateAsync(user, model.Password);
            if (!createUserResult.Succeeded)
                return (0, "User creation failed! Please check user details and try again.");

            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

            if (await roleManager.RoleExistsAsync(role))
                await userManager.AddToRoleAsync(user, role);

            return (1, "User created successfully!");
        }

        public async Task<(int, object)> Login(LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                user = await userManager.FindByEmailAsync(model.Username);
                if (user == null)
                    return (0, "Invalid username");
            }
            if (!await userManager.CheckPasswordAsync(user, model.Password))
                return (0, "Invalid password");

            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserName),
               new Claim("UserId", user.Id),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var token = GenerateToken(authClaims, user);
            return (1, token);
        }

        public async Task<(int, object)> SendVerificationCode(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                if (user == null)
                    return (0, "Invalid username");

            Random random = new Random();
            int randomNumber = random.Next(0, 1000);
            string formattedNumber = randomNumber.ToString("D4");

            CodigosVerificacion newCode = new CodigosVerificacion();
            newCode.FechaCreacion = DateTime.Now;
            newCode.Codigo = formattedNumber;
            newCode.FechaExpiracion = DateTime.Now.AddHours(8);
            newCode.UsuarioId = user.Id;
            newCode.Utilizado = false;

            _context.CodigosVerificacions.Add(newCode);
            await _context.SaveChangesAsync();

            return (1, formattedNumber);
        }
        public async Task<(int, object)> VerifyCode(string code)
        {
            var codigoVerificacion = _context.CodigosVerificacions.FirstOrDefault(x => x.Codigo == code && x.Utilizado == false && x.FechaExpiracion > DateTime.Now);

            if (codigoVerificacion == null)
                return (0, "Código inválido o expirado");

            var user = await userManager.FindByIdAsync(codigoVerificacion.UsuarioId ?? "");
            if (user == null)
                if (user == null)
                    return (0, "Invalid username");

            codigoVerificacion.Utilizado = true;
            await _context.SaveChangesAsync();
            var tokenTemp = await userManager.GeneratePasswordResetTokenAsync(user);
            return (1, tokenTemp);
        }
        public async Task<(int, object)> ResetPassword(ResetPassword model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return (0, "Invalid username");

            user.SecurityStamp = Guid.NewGuid().ToString();
            var tokenTemp = await userManager.GeneratePasswordResetTokenAsync(user);

            var createUserResult = await userManager.ResetPasswordAsync(user, tokenTemp ?? "", model.Password ?? "");
            if (!createUserResult.Succeeded)
                return (0, "Error al reiniciar contrasenia");

            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserName),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var token = GenerateToken(authClaims, user);
            return (1, token);
        }

        private UserInfoToken GenerateToken(IEnumerable<Claim> claims, ApplicationUser user)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]));
            var _TokenExpiryTimeInHour = Convert.ToInt64(_configuration["JWTKey:TokenExpiryTimeInHour"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWTKey:ValidIssuer"],
                Audience = _configuration["JWTKey:ValidAudience"],
                //Expires = DateTime.UtcNow.AddHours(_TokenExpiryTimeInHour),
                Expires = DateTime.UtcNow.AddDays(29),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new UserInfoToken
            {
                Token = tokenHandler.WriteToken(token),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Id = user.Id,
            };
        }
    }
}
