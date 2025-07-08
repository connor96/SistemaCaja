using Azure.Core;
using CajaSistema.Data;
using CajaSistema.Filters;
using CajaSistema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));



//builder.Services.AddDefaultIdentity<UserIdentity>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<UserIdentity>(options => options.SignIn.RequireConfirmedAccount = false)
//    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services
    .AddIdentity<UserIdentity, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new SessionCheckAtribute("idPersona"));
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    //options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);
    options.Lockout.MaxFailedAccessAttempts = 5;
    //options.Lockout.AllowedForNewUsers = true;

    // User settings.
    //options.User.AllowedUserNameCharacters =
    //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(1);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});



builder.Services.AddMvc();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Seguimiento.Session";
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();


//app.Use(async (context, next) =>
//{
//    // Por ejemplo, validamos si "Usuario" está en la sesión
//    var usuario = context.Session.GetString("idPersona");

//    if (string.IsNullOrEmpty(usuario) &&
//        !context.Request.Path.StartsWithSegments("/Identity/Account/Login"))
//    {
//        // Si no existe y no estamos ya en /login, redirigimos
//        context.Response.Redirect("/Identity/Account/Login?returnUrl=");
//        return ;
//    }

//    await next.Invoke();
//});



using (var scope=app.Services.CreateScope())
{
    var roleManager=
        scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "ADMINISTRADOR", "ACADEMICO","TESORERO", "CAJERO", "MARKETING","SECRESEDE" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

}

using (var scope = app.Services.CreateScope())
{
    var userManager =
        scope.ServiceProvider.GetRequiredService<UserManager<UserIdentity>>();

    //var roles = new[] { "ADMINISTRADOR", "ACADEMICO", "CAJERO", "MARKETING" };

    string email = "admin@admin.com";
    string passwor = "Aa123456";

    if(await userManager.FindByEmailAsync(email) == null)
    {
        var user = new UserIdentity();
        user.Email = email;
        user.UserName = email;
        await userManager.CreateAsync(user, passwor);

        await  userManager.AddToRoleAsync(user, "ADMINISTRADOR");

    }

}



app.Run();
