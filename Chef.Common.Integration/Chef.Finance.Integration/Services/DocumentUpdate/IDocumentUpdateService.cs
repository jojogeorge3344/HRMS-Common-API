using Chef.Common.Core.Services;
using Chef.Finance.Integration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Finance.Integration;

public interface  IDocumentUpdateService: IAsyncService<ARCancelDto>
{
    Task<IntegrationResponseDto> CancelDocument(ARCancelDto aRCancelDto);
}
