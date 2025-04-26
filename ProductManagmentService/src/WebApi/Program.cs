using Application.IManagers;
using Application.MappingProfiles;
using Infrastructure.Managers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(

    typeof(ProductProfile),
    typeof(TagProfile)

);

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
