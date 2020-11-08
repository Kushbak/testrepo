using FinanceManagmentApplication.DAL.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Factories
{
    public interface IUnitOfWorkFactory
    {
        UnitOfWork Create();
    }
}
