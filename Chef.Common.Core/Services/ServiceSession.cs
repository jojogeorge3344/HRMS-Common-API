using Chef.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Chef.Common.Services
{
    public class ServiceSession : IServiceSession
    {
        readonly IDatabaseSession databaseSession;
        public ServiceSession(IDatabaseSession databaseSession)
        {
            this.databaseSession = databaseSession;
        }
        public IUnitOfWorkSession UnitOfWorkSession(System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted)
          => databaseSession.UnitOfWorkSession(isolationLevel);
    }
}
