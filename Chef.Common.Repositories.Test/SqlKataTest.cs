using Chef.Common.Core;
using Chef.Common.Test;
using SqlKata;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Xunit;

namespace Chef.Common.Repositories.Test
{
    public class SqlKataTest : BaseServiceTest
    {
        // Set skip reason here to skip this testcases class on whole, 
        // to skip individual test case set reason on Skip attribute in individual test case
        const string skip = null;
        readonly ISqlQueryBuilder sqlQueryBuilder;
        //public SqlKataTest()
        //{ }

        public SqlKataTest(IQueryBuilderFactory queryBuilderFactory)
        {
            this.sqlQueryBuilder = queryBuilderFactory.SqlQueryBuilder();
        }

        [Fact(Skip = skip), TestPriority(1)]
        public void TestSearchFilter()
        {
            var sqlSearchGroup1 = new SqlSearchGroup(SqlConditionOperator.OR)
                .Where<TestModel>(t => t.CreatedBy, SqlSearchOperator.Equal, "1")
                .Where<TestModel>(t => t.CreatedBy, SqlSearchOperator.Equal, "1");
            var sqlSearchGroup2 = new SqlSearchGroup(SqlConditionOperator.OR)
                .Where<TestModel>(t => t.CreatedBy, SqlSearchOperator.Equal, "2")
                .Where<TestModel>(t => t.CreatedBy, SqlSearchOperator.Equal, "2");
            var sqlSearchGroup3 = new SqlSearchGroup(SqlConditionOperator.AND)
                .AddGroup(q => sqlSearchGroup1)
                .Where<TestModel>(t => t.CreatedBy, SqlSearchOperator.Equal, "2")
                .Where<TestModel>(t => t.CreatedBy, SqlSearchOperator.Equal, "2");
            var sqlSearchGroup4 = new SqlSearchGroup(SqlConditionOperator.AND)
                .AddGroup(q => sqlSearchGroup2)
                .Where<TestModel>(t => t.CreatedBy, SqlSearchOperator.Equal, "2")
                .Where<TestModel>(t => t.CreatedBy, SqlSearchOperator.Equal, "2");
            var sqlSearch = new SqlSearch()
                .AddGroup(q=> sqlSearchGroup3)
                .AddGroup(q => sqlSearchGroup4)
                .Where<TestModel>(t => t.ModifiedBy, SqlSearchOperator.Equal, "3")
                .Where<TestModel>(t => t.ModifiedBy, SqlSearchOperator.Equal, "3");

            var query = new Query("Item").ApplySqlSearch(sqlSearch);
            var result = query.Compile();
            Assert.True(true);

            //List<SqlSearchGroup> groups = new List<SqlSearchGroup>
            //    {
            //        new SqlSearchGroup
            //        {
            //            Conditions = new List<SqlSearchConditon>
            //            {
            //                new SqlSearchConditon {Field = "Group1Field1", Operator = SqlSearchOperator.In, Value= new string[] {"Group1Value1","Group1Value2" } },
            //                new SqlSearchConditon {Field = "Group1Field2", Operator = SqlSearchOperator.NotEqual, Value="Group1Value2"}
            //            }
            //        },
            //        new SqlSearchGroup
            //        {
            //            Conditions = new List<SqlSearchConditon>
            //            {
            //                new SqlSearchConditon {Field = "Group2Field1", Operator = SqlSearchOperator.GreaterThan, Value="Group2Value1"},
            //                new SqlSearchConditon {Field = "Group2Field2", Operator = SqlSearchOperator.Contains, Value="Group2Value2"}
            //            },IsOrGroup = true
            //        }
            //    };
            //sqlSearch = new SqlSearch() { Groups = groups };
            //var query = new Query("Item").ApplySqlSearch(sqlSearch);
            //var result = query.Compile();
            //Assert.True(true);
        }

        [Fact(Skip = skip), TestPriority(2)]
        public void TestSelectExt()
        {
            var query = new Query("Item").Select<TestModel>(x => x.Status);
            var result = query.Compile();
            Assert.True(true);
            query = new Query("Item").Select<TestModel>(x => new { Status1 = x.Status, IsSelected = true, x.Values });
            result = query.Compile();
            Assert.True(true);
        }


        //[Fact(Skip = skip)]
        //public void TestAsInsertExt()
        //{
        //    var testitem = new TestModel { Id = 1, Values = new string[] { "V1", "V2", "V3", "V4" }, Status = false };
        //    var propertiesCount = testitem.GetType().GetProperties().Count();
        //    var query = sqlQueryBuilder.Query<TestModel>().AsInsertExt(testitem, returnId: true); 
        //    var sqlResult = query.Compile();
        //    var sql = sqlResult.Sql;
        //    var param = sqlResult.NamedBindings;
        //    Assert.True(!string.IsNullOrEmpty(sql));
        //    Assert.True(param.Count == (propertiesCount-1)); 
        //}
    }

    internal class TestModel : Model
    {
        public string[] Values { get; set; }
        public bool Status { get; set; }
    }
}
