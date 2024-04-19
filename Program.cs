using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JCMdotNet.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("JCMdbContextContextConnection") ?? throw new InvalidOperationException("Connection string 'JCMdbContextContextConnection' not found.");

builder.Services.AddDbContext<JCMdbContextContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<JCMdotNet.Areas.Identity.Data.AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<JCMdbContextContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
}
);



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




app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
