using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace FinanceManagmentApplication.DAL.Entities
{
    public class Role:IdentityRole<int>, IEntity
    {
        public bool IsDelete { get; set; }
    }
}
