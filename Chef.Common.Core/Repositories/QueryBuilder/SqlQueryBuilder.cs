using Chef.Common.Core;
using Newtonsoft.Json;
using SqlKata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Chef.Common.Repositories
{
    public class SqlQueryBuilder : ISqlQueryBuilder
    {
        public Query Query<TModel>() where TModel : Model
        {
            var tableName = string.Format("{0}.{1}", typeof(TModel).Namespace.Split('.')[1].ToLower(),
                typeof(TModel).Name.ToLower());
            return new Query(tableName);
        }
    }

    public class SqlSearch : IEquatable<SqlSearch>
    {
        public static readonly SqlSearch DefaultInstance = new SqlSearch();
        public List<SqlSearchGroup> Groups { get; set; } = new List<SqlSearchGroup>();
        public int? Limit { get; set; } = null;

        public SqlPage Page { get; set; } = null;

        public override int GetHashCode()
        {
            return Limit != null ? Limit.GetHashCode() : 0 ^ Groups.GetHashCode() ^ (Page != null? Page.GetHashCode():0);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SqlSearch))
                return false;

            return Equals((SqlSearch)obj);
        }

        public bool Equals(SqlSearch other)
        {
            if (other == null)
                return false;

            return
                (object.ReferenceEquals(this.Limit, other.Limit) ||
                this.Limit != null &&
                this.Limit.Equals(other.Limit))
                 &&
                (object.ReferenceEquals(this.Groups, other.Groups) ||
                this.Groups != null &&
                 Groups.SequenceEqual(other.Groups));
        }
    }

    public class SqlSearchGroup : IEquatable<SqlSearchGroup>
    {
        public List<SqlSearchConditon> Conditions { get; set; } = new List<SqlSearchConditon>();
        public bool IsOrGroup { get; set; } = false;

        public override int GetHashCode()
        {
            return IsOrGroup.GetHashCode() ^ (Conditions.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SqlSearchGroup))
                return false;

            return Equals((SqlSearchGroup)obj);
        }

        public bool Equals(SqlSearchGroup other)
        {
            if (other == null)
                return false;

            return
                (object.ReferenceEquals(this.IsOrGroup, other.IsOrGroup) ||
                this.IsOrGroup.Equals(other.IsOrGroup))
                 &&
                (object.ReferenceEquals(this.Conditions, other.Conditions) ||
                this.Conditions != null &&
                 Conditions.SequenceEqual(other.Conditions));
        }
    }

    public struct SqlSearchConditon : IEquatable<SqlSearchConditon>
    {
        [Required]
        public string Field { get; set; }
        
        [Required]
        public SqlSearchOperator Operator { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public object Value { get; set; }
        
        public override bool Equals(object obj)
        {
            if (!(obj is SqlSearchConditon))
                return false;

            return Equals((SqlSearchConditon)obj);
        }

        public override int GetHashCode()
        {
            return Field != null ? Field.GetHashCode() : 0 ^ Operator.GetHashCode() ^ (Value != null ? Value.GetHashCode() : 0);
        }

        public bool Equals(SqlSearchConditon other)
        {
            return
                (object.ReferenceEquals(this.Field, other.Field) ||
                this.Field != null &&
                this.Field.Equals(other.Field))
                 &&
                (object.ReferenceEquals(this.Operator, other.Operator) ||
                this.Operator.Equals(other.Operator))
                 &&
                (object.ReferenceEquals(this.Value, other.Value) ||
                this.Value != null &&
                this.Value.Equals(other.Value));
        }
    }

    public class SqlPage
    {
        [Required]
        public int PageNo { get; set; } = 1;
        [Required]
        public int PageLimit { get; set; } = 15;
    }

    public enum SqlSearchOperator
    {
        Contains=1,
        In=2,
        StartsWith=3,
        EndsWith=4,
        Equal=5,
        GreaterThan=6,
        LessThan=7,
        GreaterThanEqual=8,
        LessThanEqual=9,
        NotEqual=10
    }
}