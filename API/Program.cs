using System.Text;
using API.Filtros;
using BLL.Interfaces;
using BLL.Services;
using BLL.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
                                                                            opciones.UseSqlServer("name=SQLServerConnection", migration => migration.MigrationsAssembly("Repository"));
                                                                            //opciones.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddIdentityCore<IdentityUser>()
                                                .AddEntityFrameworkStores<ApplicationDBContext_SQLServer>()
                                                .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddScoped<IPublicacionService,    PublicacionService>();
builder.Services.AddScoped<IPublicacionRepository, PublicacionRepository>();

builder.Services.AddScoped<IComentarioService,     ComentarioService>();
builder.Services.AddScoped<IComentarioRepository,  ComentarioRepository>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<UserManager<IdentityUser>>();
builder.Services.AddScoped<SignInManager<IdentityUser>>();
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

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
