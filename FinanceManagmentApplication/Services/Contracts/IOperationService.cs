using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.Models.OperationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Services.Contracts
{
    public interface IOperationService
    {
        Task<Response> Create(OperationCreateModel model);
        Task<Response> Edit(OperationEditModel model);
        Task<List<OperationDetailsModel>> GetAll();

        Task<OperationEditModel> GetEditModel(int id);

         Task<Response> Delete(int Id);
         Task<OperationCreateModel> GetCreateModel();


    }
}
