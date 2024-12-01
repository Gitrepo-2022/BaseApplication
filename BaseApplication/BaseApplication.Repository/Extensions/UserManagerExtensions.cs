using BaseApplication.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApplication.Repository.Extensions
{
    public static class UserManagerExtensions
    {
        
            public static Task<ApplicationUser> FindByNameAsync(this UserManager<ApplicationUser> userManager, string name)
            {
                return userManager?.Users?.FirstOrDefaultAsync(um => um.UserName == name);
            }

            //public static Task<ApplicationUser> FindByCardIDAsync(this UserManager<ApplicationUser> userManager, string cardId)
            //{
            //    return userManager?.Users?.FirstOrDefaultAsync(um => um.CardId == cardId);
            //}


    }
}
