using System.Data;

namespace Chef.Common.Repositories;

public interface IConnectionFactory
{
    IDbConnection Connection { get; }
    IDbTransaction Transaction { get; set; }
}
