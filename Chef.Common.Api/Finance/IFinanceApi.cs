using System;
using Chef.Common.Models;
using Refit;

namespace Chef.Common.Api
{
	public interface IFinanceApi
	{
        [Get("/Branch/CheckForBranch")]
        Task<int> IsBranchUsed(int id);

        //TODO return only int in finance
        [Post("/JournalBook/InsertJournalBooksForBranch")]
        Task<Branch> CreateJournalBooksForBranchAsync(Branch branch);
    }
}
