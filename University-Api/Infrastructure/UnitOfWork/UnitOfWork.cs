using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using UniversityApi.Data;
using UniversityApi.Infrastructure.Repository;

namespace UniversityApi.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{

    private UniversityRepository _universityRepository;
    private ManagerRepository _managerRepository;


    private IDbContextTransaction _transaction;
    public UnitOfWork(ApplicationDbContext dataContext)
    {
        _dataContext = dataContext;
    }


    public async Task Save()
    {
        await _dataContext.SaveChangesAsync();
    }

    public async Task BeginTransaction()
    {
        _transaction = await _dataContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
    }

    public async Task Commit()
    {
        await _transaction.CommitAsync();
    }

    public async Task CreateSavepoint(string nameSavePoint)
    {
        await _transaction.CreateSavepointAsync(nameSavePoint);
    }

    public async Task RollbackToSavepoint(string nameSavePoint)
    {
        await _transaction.RollbackToSavepointAsync(nameSavePoint);

    }

    public async Task RollBackTransaction()
    {
        await _transaction.RollbackAsync();
    }

    private readonly ApplicationDbContext _dataContext;

    public UniversityRepository  UniversityRepository
    {
        get
        {

            if (_universityRepository == null)
                _universityRepository = new UniversityRepository(_dataContext);
            return _universityRepository;
        }
    }

    public ManagerRepository ManagerRepository
    {
        get
        {

            if (_managerRepository == null)
                _managerRepository = new ManagerRepository(_dataContext);
            return _managerRepository;
        }
    }
}