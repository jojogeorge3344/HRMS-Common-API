using AutoMapper;

namespace Chef.Common.Services
{
    public abstract class AsyncService
    {
        public IMapper Mapper { get; set; }
    }
}