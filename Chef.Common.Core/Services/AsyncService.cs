using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Chef.Common.Repositories;

namespace Chef.Common.Services
{
    public abstract class AsyncService<T> : IAsyncService<T>
    {
        public IMapper Mapper { get; set; }

        public IGenericRepository<T> GenericRepository { get; set; }

        public Task<int> DeleteAsync(int id)
        {
            return GenericRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return GenericRepository.GetAllAsync();
        }

        public Task<T> GetAsync(int id)
        {
            return GenericRepository.GetAsync(id);
        }

        public Task<int> InsertAsync(T obj)
        {
            return GenericRepository.InsertAsync(obj);
        }

        public Task<int> UpdateAsync(T obj)
        {
            return GenericRepository.UpdateAsync(obj);
        }
    }
}