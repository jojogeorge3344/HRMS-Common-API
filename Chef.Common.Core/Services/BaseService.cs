using AutoMapper;
using Chef.Common.Core.Services;
using Chef.Common.Repositories;
using System.Data;

namespace Chef.Common.Services;

public abstract class BaseService : IBaseService
{
    public IMapper Mapper { get; set; }

    public IServiceSession ServiceSession { get; set; }

    public IUnitOfWorkSession UnitOfWorkSession(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        return ServiceSession.UnitOfWorkSession(isolationLevel);
    }
}