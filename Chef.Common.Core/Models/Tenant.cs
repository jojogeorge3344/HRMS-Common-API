﻿using Chef.Common.Core;
using System.Collections.Generic;

namespace Chef.Common.Models
{
    public class Tenant : Model
    {
        public string BaseCompany { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public string ConnectionString { get; set; }
        public List<Module> Modules { get; set; }
        public List<Identity> Identityserver { get; set; }
    }

    public class TenantDto
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public string ConnectionString { get; set; }
        public List<Module> Modules { get; set; }
    }
}
