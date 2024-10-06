using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

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