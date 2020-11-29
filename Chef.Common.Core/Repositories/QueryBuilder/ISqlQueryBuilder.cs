using Chef.Common.Core;
using SqlKata;
using System.Collections.Generic;
using System.ComponentModel;

namespace Chef.Common.Repositories
{
    public interface ISqlQueryBuilder
    {
        ///<summary>
        ///This is a Sql Kata Query object. We can apply all Sql Kata query builder operation using this query object
        ///</summary>
        public Query Query<TModel>() where TModel : IModel;
        IDictionary<string, object> ToDictionary(object obj);
    }
}
