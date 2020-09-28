using Chef.Common.Core;
using Chef.Common.Repositories;
using System;
using Xunit;

namespace Chef.Common.Test
{
    public class GenericRepositoryTest<T> //: IGenericRepositoryTest<T>
        where T : Model
    {
        IGenericRepository<T> repository;
        public GenericRepositoryTest(IGenericRepository<T> repository)
        {
            this.repository = repository;
        }

        //public int Insert(T item)
        //{
        //    var response = repository.InsertAsync(item).Result;
        //    Assert.IsType<int>(response);
        //    Assert.True((int)response > 0);
        //    item.Id = response;
        //    return response;
        //}

        //public int Update(T item, Action updateAction = null)
        //{
        //    if (item.Id == default)
        //        Insert(item);
        //    if (updateAction != null) updateAction();
        //    var response = repository.UpdateAsync(item).Result;
        //    Assert.IsType<int>(response);
        //    Assert.True((int)response > 0);
        //    return response;
        //}

        //public T Get(T item)
        //{
        //    if (item.Id == default)
        //        Insert(item);
        //    var response = repository.GetAsync(item.Id).Result;
        //    Assert.NotNull(response);
        //    Assert.IsType<T>(response);
        //    return response;
        //}

        //public int Delete(T item)
        //{
        //    if (item.Id == default)
        //        Insert(item);
        //    var response = repository.DeleteAsync(item.Id).Result;
        //    Assert.IsType<int>(response);
        //    Assert.True((int)response > 0);
        //    return response;
        //}
    }
}
