using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Servicios;
using Services.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUtilidades, UtilidadesService>();
builder.Services.AddScoped<IUsuarios, UsuariosService>();
builder.Services.AddScoped<IEmpleados, EmpleadosService>();
builder.Services.AddScoped<ITiendas, TiendasService>();
builder.Services.AddScoped<IPerfil, PerfilService>();

var strCon = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DbContextP1700>(options => options.UseSqlServer(strCon));


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

builder.WebHost.UseUrls("https://localhost:7788");

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "P1700 API");
});

app.MapControllers();

app.Run();
