using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.Models.ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.BL.Services.Contracts
{
    public interface IProjectService
    {
        Task Create(ProjectCreateModel model);

        Task<List<ProjectIndexModel>> GetAll();

        Task Delete(int Id);

        Task Update(ProjectIndexModel model);
    }
}
