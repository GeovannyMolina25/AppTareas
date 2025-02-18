using AppTareas.Models;
using AppTareas.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Registrar servicios de la aplicación
builder.Services.AddScoped<LoginServicio>();
builder.Services.AddScoped<TransactionServices>();

// Añadir servicios al contenedor
builder.Services.AddControllersWithViews();

// Configurar el contexto de la base de datos
builder.Services.AddDbContext<ServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ServiceConnection")));

// Obtener los orígenes permitidos desde la configuración
var origenesPermitidos = builder.Configuration.GetValue<string>("OrigenesPermitidos")!.Split(",");

// Configurar CORS
builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(politica =>
    {
        politica.WithOrigins(origenesPermitidos) // Permitir los orígenes especificados
                .AllowAnyHeader()               // Permitir cualquier encabezado
                .AllowAnyMethod();              // Permitir cualquier método (GET, POST, etc.)
    });
});

// Configurar autenticación por cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
        options.AccessDeniedPath = "/AccessDenied";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

// Añadir servicios de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Habilitar Swagger en desarrollo
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppTareas API v1");
        c.RoutePrefix = "api-docs"; // La interfaz de Swagger estará disponible en /api-docs
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Usar la política de CORS
app.UseCors(); // Asegúrate de que esto esté antes de UseAuthentication y UseAuthorization

// Añadir middleware de autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();