using AppTareas.Models;
using AppTareas.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<LoginServicio>();
builder.Services.AddScoped<TransactionServices>();
// Add services to the container.
builder.Services.AddControllersWithViews();


// Configuraci�n del contexto
builder.Services.AddDbContext<ServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ServiceConnection")));

// Configuraci�n de la autenticaci�n con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // Ruta de la p�gina de login
        options.LogoutPath = "/Logout"; // Ruta de la p�gina de logout
        options.AccessDeniedPath = "/AccessDenied"; // Ruta de acceso denegado (si lo necesitas)
        options.SlidingExpiration = true;  // Para que se actualice la cookie cada vez que se use
        options.ExpireTimeSpan = TimeSpan.FromMinutes(1); // Establece el tiempo de expiraci�n de la cookie
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// A�adir el middleware de autenticaci�n antes de la autorizaci�n
app.UseAuthentication(); // Este middleware es necesario para manejar la autenticaci�n
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
