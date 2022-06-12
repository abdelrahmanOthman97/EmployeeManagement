namespace EmployeeManagement.Domain.Interfaces.Repositories.Common
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        bool CommitTransaction();
        void RollBackTransaction();
    }
}
