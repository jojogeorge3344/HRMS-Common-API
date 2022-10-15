using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Core
{
    public interface IAppConfiguration
    {
        public string GetHostString(string Host, string Name);
    }
}
