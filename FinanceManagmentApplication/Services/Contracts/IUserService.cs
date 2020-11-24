using FinanceManagmentApplication.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Services.Contracts
{
    public interface IUserService
    {
        Task<EditUserModel> GetEditModel(int Id);

        Task<UserDetailsModel> GetDetailsModel(int Id);
        Task<List<UserIndexModel>> GetAll();
        Task<Response> Edit(EditUserModel model, ClaimsPrincipal User);
        Task<Response> ChangePassword(ChangePasswordUserModel model, ClaimsPrincipal User);
        Task<UserIndexModel> GetUser(UserIndexModel model, ClaimsPrincipal User);


    }
}
