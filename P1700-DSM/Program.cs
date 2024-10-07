using GST.Web.Utilidades;
using Models;
using Web.Utilidades;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUtilidades, UtilidadesWeb>();

builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    options.Cookie.Name = "Gym.Cookie";
    options.LoginPath = "/Usuario/InicioSesion";
    options.AccessDeniedPath = "/Forbidden";
    options.LogoutPath = "/Home/Index";
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });


builder.Services.AddAuthorization();

var app = builder.Build();

ApiData.URL = builder.Configuration.GetConnectionString("API");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
