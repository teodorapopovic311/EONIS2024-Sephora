using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Teodora",
                    Email = "teodora@test.com",
                    UserName = "teodora@test.com",
                    Address = new Address
                    {
                        FirstName = "Teodora",
                        LastName = "Popovic",
                        Street = "Zorana Petrovica 13",
                        City = "Novi Sad",
                        PhoneNumber = "0669041327"
                    }
                };

                await userManager.CreateAsync(user, "Te0dOr@");
            }
        }
    }
}