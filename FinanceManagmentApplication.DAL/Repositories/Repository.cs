using FinanceManagmentApplication.DAL.Context;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected ApplicationDbContext Context;
        protected DbSet<T> DbSet;

        public Repository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        public async Task<int> CreateAsync(T entity)
        {
            var entityEntry = await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entityEntry.Entity.Id;

        }

        public async Task<List<T>> GetAllAsync()
        {
            return await DbSet.Where(E => !E.IsDelete).ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            Context.Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            Context.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<bool> CheckCount()
        {
            return await DbSet.CountAsync() > 0;
        }

        public async Task<int> Count()
        {
            return await DbSet.CountAsync();
        }
    }
}
