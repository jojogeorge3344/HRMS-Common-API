using Chef.Common.Core;
using Chef.Common.Services;

namespace Chef.Common.Core.Services;

public interface IZooKeeperService : IBaseService
{
    public void CreateSchema();
}
