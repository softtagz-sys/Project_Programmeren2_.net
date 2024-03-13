using Microsoft.EntityFrameworkCore;
using MTGM.BL;
using MTGM.DAL;
using MTGM.DAL.EF;
using UI_MVC.Controllers;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext, Repository and Manager to the service container
builder.Services.AddDbContext<MtgmDbContext>(options => options.UseSqlite("Data Source=MTGM.db"));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MtgmDbContext>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IManager, Manager>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MtgmDbContext>();
    if (context.CreateDatabase(true))
    {
        var userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<IdentityUser>>();
        SeedIdentity(userManager);
        
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

app.MapRazorPages();

app.Run();
return;

void SeedIdentity(UserManager<IdentityUser> userManager)
{
    var kobe = new IdentityUser
    {
        Email = "kobe@kdg.be",
        UserName = "kobe@kdg.be",
        EmailConfirmed = true
    };
    userManager.CreateAsync(kobe, "Password1!").Wait();
    var jef = new IdentityUser
    {
        Email = "jef@kdg.be",
        UserName = "jef@kdg.be",
        EmailConfirmed = true
    };
    userManager.CreateAsync(jef, "Password1!").Wait();
}