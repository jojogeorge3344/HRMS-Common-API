using System;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Core
{
    public interface IBranchModel : IModel
    {
        public string BranchCode { get; set; }
    }
}