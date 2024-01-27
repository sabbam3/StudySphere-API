using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudySphere_API.Db;
using StudySphere_API.Models.Entities;
using System.Security.Claims;
using System.Text;

namespace StudySphere_API.Auth
{
    public class AuthConfigurator
    {
        public static async void Configure(WebApplicationBuilder builder)
        {
            var issuer = builder.Configuration["Jwt:Issuer"]!;
            var audience = builder.Configuration["Jwt:Audience"]!;
            var key = builder.Configuration["Jwt:Key"]!;
            builder.Services.Configure<JwtSettings>(options =>
            {
                options.Issuer = issuer;
                options.Audience = audience;
                options.SecretKey = key;
            });
            builder.Services.AddTransient<TokenGenerator>();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Student", policy => policy.RequireClaim(ClaimTypes.Role, "api-student"));
                options.AddPolicy("Lecturer", policy => policy.RequireClaim(ClaimTypes.Role, "api-lecturer"));
                options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "api-admin"));

            });
            builder.Services.AddIdentity<UserEntity, RoleEntity>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        }
        
    }
}
