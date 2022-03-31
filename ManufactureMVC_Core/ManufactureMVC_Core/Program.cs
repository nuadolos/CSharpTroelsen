using ManufactureEF_Core.Context;
using ManufactureEF_Core.DataInitializer;
using ManufactureEF_Core.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Конфигурирование контекстов для внедрения зависимостей

//Создание пула классов контекста внутри DI
builder.Services.AddDbContextPool<ManufactureContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Manufacture"),
        o => o.EnableRetryOnFailure()));

//Добавление UserRepo и RoleRepo в контейнер DI
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IRoleRepo, RoleRepo>();

#endregion

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

app.UseAuthorization();

//Составление таблицы маршрутов
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Privacy",
        pattern: "Privacy/{*pathinfo}",
        new { controller = "Home", action = "Privacy"});

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

//Получение области действия контейнера DI
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    //Извлечение объекта ManufactureContext из контейнера
    var context = services.GetRequiredService<ManufactureContext>();

    Initializer.RecreateDatabase(context);
    Initializer.InitializeData(context);
}

app.Run();
