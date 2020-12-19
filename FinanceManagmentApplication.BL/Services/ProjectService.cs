using AutoMapper;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Factories;
using FinanceManagmentApplication.Models.ProjectModels;
using FinanceManagmentApplication.BL.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.BL.Services
{
    public class ProjectService: IProjectService
    {
        private IUnitOfWorkFactory _unitOfWorkFactory { get; }

        public ProjectService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task Create(ProjectCreateModel model)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var Project = Mapper.Map<Project>(model);
                await uow.Projects.CreateAsync(Project);
            }
        }

        public async Task<List<ProjectIndexModel>> GetAll()
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var Projects = await uow.Projects.GetAllAsync();
                return Mapper.Map<List<ProjectIndexModel>>(Projects);
            }
        }

        public async Task Delete(int Id)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var Project = await uow.Projects.GetByIdAsync(Id);
                Project.IsDelete = true;
                await uow.Projects.UpdateAsync(Project);
            }
        }

        public async Task Update(ProjectIndexModel model)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var Project = Mapper.Map<Project>(model);
                await uow.Projects.UpdateAsync(Project);
            }
        }


    }
}
