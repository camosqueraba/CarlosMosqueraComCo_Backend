using API.ControllerService;
using API.Filtros;
using BLL.Interfaces;
using BLL.Services;
using BLL.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository.DataContext;
using Repository.IdentityEF;
using Repository.Interfaces;
using Repository.Repositories;
using Repository.Utils;
using System.Text;

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

builder.Services.AddScoped<ApiResultFilter>();

var origenesPermitidos = builder.Configuration.GetValue<string>("OrigenesPermitidos")!.Split(",");



builder.Services.AddDbContext<ApplicationDBContext_SQLServer>(opciones =>
{
    opciones.UseSqlServer("name=SQLServerConnection", migration => migration.MigrationsAssembly("Repository"));
});

builder.Services.AddIdentityCore<CustomIdentityUser>()
                                                .AddEntityFrameworkStores<ApplicationDBContext_SQLServer>()
                                                .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddScoped<IPublicacionService,    PublicacionService>();
builder.Services.AddScoped<IPublicacionRepository, PublicacionRepository>();

builder.Services.AddScoped<IComentarioService,     ComentarioService>();
builder.Services.AddScoped<IComentarioRepository,  ComentarioRepository>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioControllerService, UsuarioControllerService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<IAutorizacionUtilsService, AutorizacionUtilsService>();
builder.Services.AddScoped<IAutorizacionUtilsRepository, AutorizacionUtilsRepository>();


builder.Services.AddScoped<UserManager<CustomIdentityUser>>();
builder.Services.AddScoped<SignInManager<CustomIdentityUser>>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication().AddJwtBearer(opciones =>
{
    opciones.MapInboundClaims = false;
    opciones.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["llavejwt"]!)),
        ClockSkew = TimeSpan.Zero
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Api-CarlosMosqueraComCo",
        Version = "v1"
    });
});


builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(origenesPermitidos)
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


var app = builder.Build();

///Creacion de migraciones para produccion, NO SE USA PORQUE LA BD YA SE CREO EN AZURE
/*
using (var scope = app.Services.CreateScope())
{
 var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext_SQLServer>();
    if (dbContext.Database.IsRelational())
    {
        dbContext.Database.Migrate();
    }
}
*/

// Configure the HTTP request pipeline.
/*
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api-CarlosMosqueraComCo v1");
});


app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();


