using FinanceManagmentApplication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Repositories.Contracts
{
    public interface IPaymentTypeRepository: IRepository<PaymentType>
    {
        bool Check(int Id);
    }
}
