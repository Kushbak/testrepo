using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Services.Contracts
{
    public interface IAuthenticateService
    {
        Task<TokenModel> Login(LoginModel model);

        Task<Response> Register(RegisterModel model);
    }
}
