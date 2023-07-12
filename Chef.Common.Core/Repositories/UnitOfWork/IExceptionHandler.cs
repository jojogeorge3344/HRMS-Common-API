using System;

namespace Chef.Common.Core;

public interface IExceptionHandler
{
    bool Retry(Exception ex);
}
