using UniversityApi.Infrastructure.Repository;

namespace UniversityApi.Infrastructure.UnitOfWork;

public interface IUnitOfWork
{

    public UniversityRepository  UniversityRepository{ get; }
    public ManagerRepository  ManagerRepository{ get; }


    public Task BeginTransaction();
    public Task Commit();
    public Task RollBackTransaction();
    public Task CreateSavepoint(string nameSavePoint);

    public Task RollbackToSavepoint(string nameSavePoint);



    public Task Save();

}