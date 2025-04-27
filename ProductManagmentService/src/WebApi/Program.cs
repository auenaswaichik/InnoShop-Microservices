using System.Text;
using Application.IManagers;
using Application.IService;
using Application.MappingProfiles;
using DotNetEnv;
using Infrastructure.DbContexts;
using Infrastructure.Managers;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var envConfig = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.env.json", optional: false, reloadOnChange: true)
    .Build();
builder.Configuration.AddConfiguration(envConfig);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductManagmentServiceDbContext>(options =>
    options.UseNpgsql($"Host={builder.Configuration["Database:Host"]};" +
                     $"Port={builder.Configuration["Database:Port"]};" +
                     $"Database={builder.Configuration["Database:Name"]};" +
                     $"Username={builder.Configuration["Database:Username"]};" +
                     $"Password={builder.Configuration["Database:Password"]}")    
);
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"] ,
        ValidAudience = builder.Configuration["Jwt:Audience"] ,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireClientRole", policy => policy.RequireRole("Client"));
});

builder.Services.AddAutoMapper(

    typeof(ProductProfile),
    typeof(TagProfile)

);

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
