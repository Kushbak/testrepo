using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Models.WebModels
{
    public class TokenModel
    {   
        public string token { get; set; }

        public DateTime expiration { get; set; }
    }
}
