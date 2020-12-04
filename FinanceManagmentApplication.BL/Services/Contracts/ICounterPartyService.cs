using FinanceManagmentApplication.Models;
using FinanceManagmentApplication.Models.CounterPartiesModel;
using FinanceManagmentApplication.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.BL.Services.Contracts
{
    public interface ICounterPartyService
    {
        Task<Response> Create(CounterPartyCreateModel model);

        Task<CounterPartyCreateModel> GetCreateModel();

        Task<Response> Edit(CounterPartyEditModel model);

        Task<CounterPartyEditModel> GetEditModel(int id);

        Task<Response> Delete(int Id);

        Task<List<CounterPartyIndexModel>> GetAll();
    }
}
