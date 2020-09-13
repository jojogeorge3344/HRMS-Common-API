using Chef.Common.Core;
using SqlKata;

namespace Chef.Common.Repositories
{
    public interface ISqlQueryBuilder
    {
        ///<summary>
        ///This is a Sql Kata Query object. We can apply all Sql Kata query builder operation using this query object
        ///</summary>
        public Query Query<TModel>() where TModel : Model;
    }
}
