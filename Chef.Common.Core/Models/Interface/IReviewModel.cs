using Chef.Common.Core;
using Chef.Trading.Types;
using System;

namespace Chef.Trading.Models;

public interface IReviewModel : IModel
{
    public ReviewStatus ReviewStatus { get; set; }
    public string ReviewComment { get; set; }
    public string ReviewedBy { get; set; }
    public DateTime ReviewedDate { get; set; }
}
