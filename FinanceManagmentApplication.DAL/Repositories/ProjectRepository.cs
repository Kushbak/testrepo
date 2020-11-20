using FinanceManagmentApplication.DAL.Context;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.DAL.Repositories
{
    public class ProjectRepository: Repository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            DbSet = applicationDbContext.Projects;
        }

        public bool Check(int Id)
        {
            return DbSet.Any(i => i.Id == Id);
        }

        public async Task<int> GetNullProjectId()
        {
            var Project = DbSet.Where(i => i.Name.ToLower() == "прочее").FirstOrDefault();
            if (Project == null)
            {
                return await CreateAsync(new Project { Name = "Прочее" });
            }

            return Project.Id;
        }

    }
}
