using EmployeeManagement.Domain.Interfaces.Repositories.Common;
using System;

namespace EmployeeManagement.Infrastructure.Repositories.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmployeeManagementContext _context;

        public UnitOfWork(EmployeeManagementContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            if (_context.Database.CurrentTransaction == null)
                _context.Database.BeginTransaction();
        }

        public bool CommitTransaction()
        {
            try
            {
                _context.SaveChanges();
                _context.Database.CommitTransaction();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void RollBackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

    }
}
