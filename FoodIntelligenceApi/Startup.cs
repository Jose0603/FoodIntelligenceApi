using FoodIntelligence.Data;
using FoodIntelligence.Data.Autentication;
using FoodIntelligence.Service;
using FoodIntelligence.Service.CategoriasComidaServices;
using FoodIntelligence.Service.Services;
using FoodIntelligence.Service.Services.CategoriasComidaServices;
using FoodIntelligence.Service.Services.ComidasServices;
using FoodIntelligence.Service.Services.DetallePedidoServices;
using FoodIntelligence.Service.Services.PedidosServices;
using FoodIntelligence.Service.Services.RestaurantesServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace FoodIntelligenceApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAutoMapper(c => c.AddProfile<AutoMapping>(), typeof(Startup));

            var _GetConnectionString = Configuration.GetConnectionString("connMSSQL");
            services.AddDbContext<FIntelligenceDbContext>(options => options.UseSqlServer(_GetConnectionString));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_GetConnectionString));
            services.AddIdentity<ApplicationUser, IdentityRole>()
             .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();



            //all bll services
            services.AddScoped<ICategoriasComidaService, CategoriasComidaService>();
            services.AddScoped<IRestaurantesService, RestaurantesService>();
            services.AddScoped<IComidasService, ComidasService>();
            services.AddScoped<IPedidosService, PedidosService>();
            services.AddScoped<IDetallePedidoService, DetallePedidoService>();

            services.AddScoped<IUserInfoService, UserInfoService>();

            services.AddCors();
            services.AddControllers().AddNewtonsoftJson();

            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTKey:Secret"])),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero
            };
            services.AddSingleton(tokenValidationParams);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                jwt.SaveToken = false;
                jwt.TokenValidationParameters = tokenValidationParams;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FoodIntelligenceApi", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FoodIntelligenceApi v1"));
                // app.UseHangfireDashboard();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles(); // For the wwwroot folder

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // endpoints.MapHangfireDashboard();
            });

            // var IncompleteApplicantUserProfile = serviceProvider.GetRequiredService<IEMV_Email_BLL>();
            // recurringJobManager.AddOrUpdate("Email Controller", () => IncompleteApplicantUserProfile.IncompleteApplicantUserProfile(), Cron.Daily(9));
        }
    }
}
