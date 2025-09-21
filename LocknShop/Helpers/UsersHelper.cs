using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LocknShop.Data;
using LocknShop.Models;
using System.Security.Claims;

namespace LocknShop.Helpers
{
    public class UsersHelper
    {
        public static IdentityUser GetUser(ApplicationDbContext context, ClaimsPrincipal user)
        {
            var userid = user.Identities.First().Claims.First().Value;
             
            return context.Users.FirstOrDefault(x => x.Id == userid);
        }

        

        public static bool UserExists()
        {
            bool ret = false;

            return ret;
        }
    }
}
