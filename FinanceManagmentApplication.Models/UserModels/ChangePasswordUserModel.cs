using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.Models.UserModels
{
    public class ChangePasswordUserModel
    {
        // public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

    }
}