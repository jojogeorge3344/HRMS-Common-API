using Chef.Common.Core;
using System;

namespace Chef.Common.Test
{
    public interface IGenericRepositoryTest<T>
        where T : Model
    {
        public int Insert(T item);
        public int Update(T item, Action updateAction = null);
        public T Get(T item);
        public int Delete(T item);
    }
}
