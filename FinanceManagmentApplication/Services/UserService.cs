using FinanceManagmentApplication.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Services
{
    public class UserService
    {
        private UserManager<User> UserManager { get; set; }

        public UserService(UserManager<User> userManager)
        { 
            

        }
    }
}
