using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using StudySphere_API.Abstractions;
using StudySphere_API.Auth;
using StudySphere_API.Db;
using StudySphere_API.Models.Entities;
using StudySphere_API.Repositories;
using StudySphere_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter your token in the text input below.\r\n\r\nExample:Bearer fkalasd5695d2da",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
AuthConfigurator.Configure(builder);
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IStudyService, StudyService>();

builder.Services.AddScoped<IStudyRepository, StudyRepository>();
builder.Services.AddDbContext<AppDbContext>(c => c.UseSqlServer(builder.Configuration["AppDbContextConnection"]), ServiceLifetime.Scoped);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();
    await roleManager.CreateAsync(new RoleEntity() { Name = "api-student" });
    await roleManager.CreateAsync(new RoleEntity() { Name = "api-lecturer" });
    await roleManager.CreateAsync(new RoleEntity() { Name = "api-admin" });

}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
