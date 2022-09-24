using Chef.Common.Repositories;

namespace Chef.Common.Services
{
    public class ServiceSession : IServiceSession
    {
        private readonly IDatabaseSession databaseSession;

        public ServiceSession(IDatabaseSession databaseSession)
        {
            this.databaseSession = databaseSession;
        }

        public IUnitOfWorkSession UnitOfWorkSession(System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted)
        {
            return databaseSession.UnitOfWorkSession(isolationLevel);
        }
    }
}
