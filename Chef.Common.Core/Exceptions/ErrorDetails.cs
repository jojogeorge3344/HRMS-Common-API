using System.Collections.Generic;

namespace Chef.Common.Core;

public class ErrorDetails
{
    public string Code { get; set; }

    public List<string> Messages { get; set; }

    public string Message { get; set; }

    public List<ErrorExceptionMessage> ErrorMessages { get; set; }
}
