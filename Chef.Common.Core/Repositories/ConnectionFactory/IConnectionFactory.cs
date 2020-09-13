using System.Data;

namespace Chef.Common.Repositories
{
    public interface IConnectionFactory
    {
        public IDbConnection Connection { get; }
    }
}
