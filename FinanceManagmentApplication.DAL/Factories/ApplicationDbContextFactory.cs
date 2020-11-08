using FinanceManagmentApplication.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Factories
{
    public class ApplicationDbContextFactory: IApplicationDbContextFactory
    {
        private DbContextOptions _dbContextOptions { get; set; }

        public ApplicationDbContextFactory(DbContextOptions dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        public ApplicationDbContext Create()
        {
            return new ApplicationDbContext(_dbContextOptions);
        }
    }
}
