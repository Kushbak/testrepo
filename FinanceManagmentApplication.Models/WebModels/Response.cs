using FinanceManagmentApplication.Models.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Models.WebModels
{
    public class Response
    {
        public StatusEnum Status { get; set; }
        public string Message { get; set; }
    }
}
