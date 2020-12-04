using FinanceManagmentApplication.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.BL.Services.Contracts
{
    public interface IAuthenticateService
    {
        Task<TokenModel> Login(LoginModel model);

        Task<Response> Register(RegisterModel model);
    }
}
