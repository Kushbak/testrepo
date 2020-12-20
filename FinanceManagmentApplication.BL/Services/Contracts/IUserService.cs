using FinanceManagmentApplication.Models.UserModels;
using FinanceManagmentApplication.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.BL.Services.Contracts
{
    public interface IUserService
    {
        Task<EditUserModel> GetEditModel(int Id);
        Task<UserDetailsModel> GetDetailsModel(int Id);
        Task<List<UserIndexModel>> GetAll();
        Task<TokenModel> Edit(EditUserModel model, ClaimsPrincipal User);
        Task<Response> ChangePassword(ChangePasswordUserModel model, ClaimsPrincipal User);
        Task<UserIndexModel> GetUser(UserIndexModel model, ClaimsPrincipal User);


    }
}
