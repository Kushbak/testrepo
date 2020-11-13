using FinanceManagmentApplication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.DAL.Repositories.Contracts
{
    public interface IRepository<T> where T: class, IEntity
    {
        Task<int> CreateAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task<bool> CheckCount();
        Task<int> Count();
    }
}
