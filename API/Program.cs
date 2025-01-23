using API.Filtros;
using BLL.Interfaces;
using BLL.Services;
using DAL.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository.DataContext;
using Repository.Interfaces;
using Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllers(options =>
{
    // Add the filter globally
    options.Filters.Add(typeof(ModelStateValidationFilter)); 
}).ConfigureApiBehaviorOptions(options =>
{
    // Disable automatic model validation
    options.SuppressModelStateInvalidFilter = true; 
});


builder.Services.AddDbContext<ApplicationDBContext_SQLServer>(opciones => {
                                                                            opciones.UseSqlServer("name=SQLServerConnection");
                                                                            //opciones.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddScoped<IPublicacionService, PublicacionService>();
builder.Services.AddScoped<IPublicacionRepository, PublicacionRepository>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
/*
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
