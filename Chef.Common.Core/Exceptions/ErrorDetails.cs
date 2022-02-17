using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Core
{
    public class ErrorDetails
    {

        public string Code { get; set; }

        public List<string> Messages { get; set; }

        public string Message { get; set; }

        public List<ErrorExceptionMessage> ErrorMessages { get; set; }
    }
}
