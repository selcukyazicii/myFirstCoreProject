using coreProject.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject
{
    public class IdentityInitializer
    {
        public static void CreateAdmin(UserManager<AppUser>userManager,
            RoleManager<IdentityRole> roleManager)
        {
            AppUser appUser = new AppUser
            {
                Name = "ali",
                Surname = "Celik",
                UserName = "veli"
            };  
            if (userManager.FindByNameAsync("ali").Result==null)
            {
                var identityResult = userManager.CreateAsync(appUser,"1").Result;
            }
            if (roleManager.FindByNameAsync("ADMIN").Result==null)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = "ADMIN"
                };
                var identityResult = roleManager.CreateAsync(identityRole).Result;
                var result=userManager.AddToRoleAsync(appUser, identityRole.Name).Result;
            }
        }
    }
}
