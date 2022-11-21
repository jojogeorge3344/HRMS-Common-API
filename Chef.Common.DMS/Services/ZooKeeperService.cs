namespace Chef.Common.DMS.Services;

public class ZooKeeperService : AsyncService<Tenant>, IZooKeeperService
{
    private readonly IZooKeeperRepository zooKeeperRepository;
    private readonly ISimpleUnitOfWork simpleUnitOfWork;

    public ZooKeeperService(
        IZooKeeperRepository zooKeeperRepository,
        ISimpleUnitOfWork simpleUnitOfWork)
    {
        this.zooKeeperRepository = zooKeeperRepository;
        this.simpleUnitOfWork = simpleUnitOfWork;
    }

    public void CreateSchema()
    {
        simpleUnitOfWork.BeginTransaction();
        try
        {
            zooKeeperRepository.CreateSchema();
            simpleUnitOfWork.Commit();
        }
        catch
        {
            simpleUnitOfWork.Rollback();
            throw;
        }
    }
}
