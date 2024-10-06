using Azure.Identity;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Xml.Schema;

var builder = WebApplication.CreateBuilder(args);

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
        }
    }
}

var app = builder.Build();
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