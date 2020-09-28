using System;
using Xunit;

namespace Chef.Common.Repositories.Test
{
    [TestCaseOrderer("Chef.Common.Test.PriorityOrderer", "Chef.Common.Test")]
    public abstract class BaseServiceTest : IDisposable
    {
        //public TestFixture<TModel> TestFixture { get; set; }
        public BaseServiceTest()
        {
            // Do "global" initialization here; Called before every test method. 
        }

        public void Dispose()
        {
            // Do "global" teardown here; Called after every test method.
        }
    }
}
