using FinanceManagmentApplication.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Context
{
    public class ApplicationDbContext: IdentityDbContext<User, Role, int>
    {   
        public DbSet<PaymentType> PaymentTypes { get; set; }

        public DbSet<Score> Scores { get; set; }

        public DbSet<FinanceAction> FinanceActions { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Remittance> Remittances { get; set; }

        public DbSet<Operation> Operations { get; set; }

        public DbSet<OperationType> OperationTypes { get; set; }

        public DbSet<CounterParty> CounterParties { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users{ get; set; }

        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        { 
        
        }
    }
}
