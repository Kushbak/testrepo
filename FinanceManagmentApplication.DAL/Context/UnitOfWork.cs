using FinanceManagmentApplication.DAL.Repositories;
using FinanceManagmentApplication.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Context
{
    public class UnitOfWork: IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed;

        public IOperationRepository Operations { get; }
        public ITransactionRepository Transactions { get; }
        public IOperationTypeRepository OperationTypes { get; }
        public IScoreRepository Scores { get; }
        public IPaymentTypeRepository PaymentTypes { get; }
        public IProjectRepository Projects { get; }
        public IUserRepository Users { get; }
        public ICounterPartyRepository CounterParties { get; }
        public IRemittanceRepository Remittances { get; }
        public IFinanceActionRepository FinanceActions { get; }

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            Operations = new OperationRepository(applicationDbContext);
            OperationTypes = new OperationTypeRepository(applicationDbContext);
            Scores = new ScoreRepository(applicationDbContext);
            PaymentTypes = new PaymentTypeRepository(applicationDbContext);
            Projects = new ProjectRepository(applicationDbContext);
            Users = new UserRepository(applicationDbContext);
            CounterParties = new CounterPartyRepository(applicationDbContext);
            Transactions = new TransactionRepository(applicationDbContext);
            Remittances = new RemittanceRepository(applicationDbContext);
            FinanceActions = new FinanceActionRepository(applicationDbContext);
        }

        #region Disposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _context.Dispose();

            _disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
        #endregion
    }
}
