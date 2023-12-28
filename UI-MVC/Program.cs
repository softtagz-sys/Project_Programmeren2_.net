using Microsoft.EntityFrameworkCore;
using MTGM.BL;
using MTGM.DAL;
using MTGM.DAL.EF;
using UI_MVC.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext, Repository and Manager to the service container
builder.Services.AddDbContext<MtgmDbContext>(options => options.UseSqlite("Data Source=MTGM.db"));
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IManager, Manager>();

var app = builder.Build();

// Get the service provider and use it to get the DbContext
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var context = services.GetRequiredService<MtgmDbContext>();

    // Ensure the database is created
    context.Database.EnsureCreated();

    // Call the Seed routine if necessary
    if (!context.Cards.Any())
    {
        DataSeeder.Seed(context);
    }
}

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();