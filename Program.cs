using AppTareas.Models;
using AppTareas.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Register application services
builder.Services.AddScoped<LoginServicio>();
builder.Services.AddScoped<TransactionServices>();

// Add services to the container
builder.Services.AddControllersWithViews();

// Configure the database context
builder.Services.AddDbContext<ServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ServiceConnection")));

// Configure cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
        options.AccessDeniedPath = "/AccessDenied";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Enable Swagger middleware in development
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppTareas API v1");
        c.RoutePrefix = "api-docs"; // Swagger UI will be available at /api-docs
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add the authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
