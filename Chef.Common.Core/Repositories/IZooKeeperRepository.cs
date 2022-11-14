using Chef.Common.Core;

namespace Chef.Common.Core.Repositories;

public interface IZooKeeperRepository : IGenericRepository<Model>
{
    public void CreateSchema();
}