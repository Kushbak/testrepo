using FinanceManagmentApplication.DAL.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Factories
{
    public class UnitOfWorkFactory: IUnitOfWorkFactory
    {
        private IApplicationDbContextFactory _applicationDbContextFactory { get; }

        public UnitOfWorkFactory(IApplicationDbContextFactory applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory;
        }

        public UnitOfWork Create()
        {
            return new UnitOfWork(_applicationDbContextFactory.Create());
        }

    }
}
