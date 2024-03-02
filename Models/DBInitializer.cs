using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DBInitializer
    {

            public static async Task SeedRolesAndAssignToUsers(IServiceProvider serviceProvider)
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                // Seed roles
                await SeedAdminUser(roleManager,userManager);

            }
        
        public static async Task SeedAdminUser(RoleManager<IdentityRole> _roleManager,UserManager<ApplicationUser>_userManager)
        {
            // Create the admin role if it doesn't exist
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Check if the user exists
            var user = await _userManager.FindByEmailAsync("sara.hamdi@gmail.com");
            if (user == null)
            {
                var AdminUser = new ApplicationUser
                {
                    UserName = "sarah",
                    Email = "sara.hamdi@gmail.com",
                    // Add more properties as needed
                };

                var result = await _userManager.CreateAsync(AdminUser, "123456Sa/");
                // Assign the admin role to the user
                await _userManager.AddToRoleAsync(AdminUser, "Admin");
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }


        }
    
}
}
