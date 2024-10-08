using Azure.Identity;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Xml.Schema;
using HockeyClubAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using HockeyClubAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HockeyClubContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<HockeyClubContext>()
    .AddDefaultTokenProviders();

// Add Mvc service
builder.Services.AddControllersWithViews();

builder.Services.AddControllers();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Enable authentication
app.UseAuthorization(); // Enable auhorization

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await CreateRoles(services);
        await SeedUsers(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occured while seeding roles and users: {ex.Message}");
    }
}

app.Run();

async Task CreateRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Define Roles
    string[] roleNames = { "Admin", "Leader", "Office", "Trainer", "Helper"};
    IdentityResult roleResult;

    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            if (!roleResult.Succeeded)
            {
                throw new Exception($"Failed to create role : {roleName}");
            }
        }
    }
}


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await CreateRoles(services);
}

async Task SeedUsers(IServiceProvider serviceProvider)
{   
    // Email, Password, Role/username
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    
    var users = new List<(string Email, string Password, string Role)>
    {
        ("admin@hockey.com", "Admin123", "Admin"),
        ("leader@hockey.com", "Leader123", "Leader"),
        ("office@hockey.com", "office123", "office"),
        ("trainer@hockey.com", "Trainer123", "Trainer"),
        ("helper@hockey.com", "Helper123", "Helper")

    };

    foreach (var user in users)
    {
        if (await userManager.FindByEmailAsync(user.Email) == null)
        {
            var appUser = new ApplicationUser
            {
                UserName = user.Email,
                Email = user.Email
            };

            var result = await userManager.CreateAsync(appUser, user.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(appUser, user.Role);
            }
        }
    }
}

using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;
    await CreateRoles(services);
    await SeedUsers(services);
}