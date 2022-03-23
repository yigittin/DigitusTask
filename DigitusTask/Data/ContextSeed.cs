﻿using DigitusTask.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitusTask.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Models.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Models.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Models.Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Models.Roles.Basic.ToString()));
        }
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "superadmin@gmail.com",
                FirstName = "Yiğit",
                LastName = "Tın",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Admin123.");
                    await userManager.AddToRoleAsync(defaultUser, Models.Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Models.Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Models.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Models.Roles.SuperAdmin.ToString());
                }

            }
        }

    }
}
