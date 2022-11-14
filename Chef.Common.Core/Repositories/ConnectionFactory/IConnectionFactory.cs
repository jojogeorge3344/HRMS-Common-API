using System.Data;

namespace Chef.Common.Core.Repositories;

public interface IConnectionFactory
{
    IDbConnection Connection { get; }
    IDbTransaction Transaction { get; set; }
}

public interface ITenantConnectionFactory : IConnectionFactory
{
}

public interface IConsoleConnectionFactory : IConnectionFactory
{
}
