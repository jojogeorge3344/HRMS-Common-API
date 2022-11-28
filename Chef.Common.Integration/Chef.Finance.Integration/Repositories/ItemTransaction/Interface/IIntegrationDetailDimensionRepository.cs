
using Chef.Finance.Integration.Models;
namespace Chef.Finance.Integration;
public interface IIntegrationDetailDimensionRepository:IGenericRepository<IntegrationDetailDimension>
    {
        Task<IEnumerable<IntegrationDetailDimension>> GetDetailDimensionByHeaderId(int HeaderId);

        Task<IEnumerable<IntegrationDetailDimension>> GetDimensionDetailsbyId(int integrationId);

      
    }

