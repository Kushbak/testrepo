using FinanceManagmentApplication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Repositories.Contracts
{
    public interface IOperationTypeRepository: IRepository<OperationType>
    {
        bool Check(int Id);

        
    }
}
