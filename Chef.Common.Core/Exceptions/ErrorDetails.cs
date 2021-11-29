using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Core
{
    public class ErrorDetails
    {

        public int StatusCode { get; set; }

        public List<string> Messages { get; set; }

        public List<ErrorExceptionMessage> ErrorMessages { get; set; }
    }
}
