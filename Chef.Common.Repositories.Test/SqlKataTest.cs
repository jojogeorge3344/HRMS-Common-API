using Chef.Common.Test;
using SqlKata;
using System.Collections.Generic;
using Xunit;

namespace Chef.Common.Repositories.Test
{
    public class SqlKataTest : BaseServiceTest
    {
        // Set skip reason here to skip this testcases class on whole, 
        // to skip individual test case set reason on Skip attribute in individual test case
        const string skip = null;
        public SqlKataTest()
        { }
        [Fact(Skip = skip), TestPriority(1)]
        public void TestSearchFilter()
        {
            List<SqlSearchGroup> groups = new List<SqlSearchGroup>
                {
                    new SqlSearchGroup
                    {
                        Conditions = new List<SqlSearchConditon>
                        {
                            new SqlSearchConditon {Field = "Group1Field1", Operator = SqlSearchOperator.In, Value= new string[] {"Group1Value1","Group1Value2" } },
                            new SqlSearchConditon {Field = "Group1Field2", Operator = SqlSearchOperator.NotEqual, Value="Group1Value2"}
                        }
                    },
                    new SqlSearchGroup
                    {
                        Conditions = new List<SqlSearchConditon>
                        {
                            new SqlSearchConditon {Field = "Group2Field1", Operator = SqlSearchOperator.GreaterThan, Value="Group2Value1"},
                            new SqlSearchConditon {Field = "Group2Field2", Operator = SqlSearchOperator.Contains, Value="Group2Value2"}
                        }
                    }
                };
            SqlSearch sqlSearch = new SqlSearch() { Groups = groups };
            var query = new Query("Item").ApplySqlSearch(sqlSearch);
            var result = query.Compile();
            Assert.True(true);
        }
    }
}
