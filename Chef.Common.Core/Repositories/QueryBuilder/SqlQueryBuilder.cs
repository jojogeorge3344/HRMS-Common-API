using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using Chef.Common.Core;
using Newtonsoft.Json;
using SqlKata;

namespace Chef.Common.Repositories
{
    public class SqlQueryBuilder : ISqlQueryBuilder
    {
        readonly ConcurrentDictionary<Type, PropertyDescriptorCollection> PropertyDescriptors;

        public SqlQueryBuilder()
        {
            PropertyDescriptors = new ConcurrentDictionary<Type, PropertyDescriptorCollection>();
        }

        public Query Query<TModel>() where TModel : IModel
        {
            var tableName = string.Format("{0}.{1}", typeof(TModel).Namespace.Split('.')[1].ToLower(),
                typeof(TModel).Name.ToLower());
            return new Query(tableName);
        }

        PropertyDescriptorCollection GetPropertyDescriptors(object obj)
        {
            var type = obj.GetType();
            if (this.PropertyDescriptors.TryGetValue(type, out var collection))
                return collection;
            collection = TypeDescriptor.GetProperties(obj);
            this.PropertyDescriptors.TryAdd(type, collection);
            return collection;
        }

        public IDictionary<string, object> ToDictionary(object obj)
        {
            var dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            if (obj != null)
            {
                foreach (PropertyDescriptor propertyDescriptor in GetPropertyDescriptors(obj))
                {
                    object value = propertyDescriptor.GetValue(obj);
                    dictionary.Add(propertyDescriptor.Name.ToLower(), value);
                }
            }
            return dictionary;
        }
    }

    public class SqlSearch : IEquatable<SqlSearch>
    {
        public static readonly SqlSearch DefaultInstance = new SqlSearch();
        public int? Limit { get; set; } = null;
        public int? Offset { get; set; } = null;
        public SqlPage Page { get; set; } = null;
        public SqlConditionOperator Condition { get; set; } = SqlConditionOperator.AND;
        public List<SqlSearchRule> Rules { get; set; } = new List<SqlSearchRule>();
        public List<SqlSearchGroup> Groups { get; set; } = new List<SqlSearchGroup>();
        public List<SqlFilterRule> FilterRules { get; set; } = new List<SqlFilterRule>();

        #region IEquatable overrides

        public override int GetHashCode()
        {
            return Limit != null ? Limit.GetHashCode() : 0
                ^ (Page != null ? Page.GetHashCode() : 0);
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
                (object.ReferenceEquals(this.Page, other.Page) ||
                this.Page != null &&
                this.Page.Equals(other.Page))
                &&
                (object.ReferenceEquals(this.Condition, other.Condition) ||
                this.Condition.Equals(other.Condition))
                &&
                (object.ReferenceEquals(this.Rules, other.Rules) ||
                this.Rules != null &&
                 Rules.SequenceEqual(other.Rules));
        }
        #endregion

        public SqlSearch() { }
        public SqlSearch(SqlConditionOperator sqlConditionOperator)
        {
            Condition = sqlConditionOperator;
        }

        public SqlSearch AddGroup(Func<SqlSearch, SqlSearchGroup> sqlSearch)
        {
            this.Groups.Add(sqlSearch(this));
            return this;
        }
        //public SqlSearch AddGroup(SqlConditionOperator sqlConditionOperator)
        //{
        //    var group = new SqlSearchGroup() { Condition = sqlConditionOperator };
        //    Groups.Add(group);
        //    return this;
        //}
        public SqlSearch Where<T>(Expression<Func<T, object>> column, SqlSearchOperator sqlSearchOperator, object value)
        {
            this.Rules.Add(new SqlSearchRule
            {
                Field = column.GetFieldName<T>(),
                Operator = sqlSearchOperator,
                Value = value
            });
            return this;
        }

        public SqlSearch Where<T>(Expression<Func<T, object>> column, object value)
        {
            this.Rules.Add(new SqlSearchRule
            {
                Field = column.GetFieldName<T>(),
                Operator = SqlSearchOperator.Equal,
                Value = value
            });
            return this;
        }

        public SqlSearch WhereNull<T>(Expression<Func<T, object>> column)
        {
            this.Rules.Add(new SqlSearchRule
            {
                Field = column.GetFieldName<T>(),
                Operator = SqlSearchOperator.IsNull,
                Value = null
            });
            return this;
        }

        public SqlSearch WhereNotNull<T>(Expression<Func<T, object>> column)
        {
            this.Rules.Add(new SqlSearchRule
            {
                Field = column.GetFieldName<T>(),
                Operator = SqlSearchOperator.IsNotNull,
                Value = null
            });
            return this;
        }
    }

    public class SqlSearchGroup : IEquatable<SqlSearchGroup>
    {
        public SqlConditionOperator Condition { get; set; } = SqlConditionOperator.AND;
        public List<SqlSearchRule> Rules { get; set; } = new List<SqlSearchRule>();
        public List<SqlSearchGroup> Groups { get; set; } = new List<SqlSearchGroup>();

        #region IEquatable overrides

        public override bool Equals(object obj)
        {
            if (obj is not SqlSearchGroup)
                return false;

            return Equals((SqlSearchGroup)obj);
        }

        public override int GetHashCode()
        {
            return Rules.GetHashCode() ^ Condition.GetHashCode();
        }

        public bool Equals(SqlSearchGroup other)
        {
            return

                (object.ReferenceEquals(Condition, other.Condition) ||
                this.Condition.Equals(other.Condition))
                &&
                (object.ReferenceEquals(this.Rules, other.Rules) ||
                this.Rules != null &&
                 Rules.SequenceEqual(other.Rules));
        }
        #endregion

        public SqlSearchGroup() { }
        public SqlSearchGroup(SqlConditionOperator sqlConditionOperator)
        {
            Condition = sqlConditionOperator;
        }

        public SqlSearchGroup AddGroup(Func<SqlSearchGroup, SqlSearchGroup> sqlSearch)
        {
            this.Groups.Add(sqlSearch(this));
            return this;
        }

        //public SqlSearchGroup AddGroup(SqlConditionOperator sqlConditionOperator)
        //{
        //    var group = new SqlSearchGroup() { Condition = sqlConditionOperator };
        //    Groups.Add(group);
        //    return this;
        //}

        public SqlSearchGroup Where<T>(Expression<Func<T, object>> column, SqlSearchOperator sqlSearchOperator, object value)
        {
            this.Rules.Add(new SqlSearchRule
            {
                Field = column.GetFieldName<T>(),
                Operator = sqlSearchOperator,
                Value = value
            });
            return this;
        }

        public SqlSearchGroup Where<T>(Expression<Func<T, object>> column, object value)
        {
            this.Rules.Add(new SqlSearchRule
            {
                Field = column.GetFieldName<T>(),
                Operator = SqlSearchOperator.Equal,
                Value = value
            });
            return this;
        }

        public SqlSearchGroup WhereNull<T>(Expression<Func<T, object>> column)
        {
            this.Rules.Add(new SqlSearchRule
            {
                Field = column.GetFieldName<T>(),
                Operator = SqlSearchOperator.IsNull,
                Value = null
            });
            return this;
        }

        public SqlSearchGroup WhereNotNull<T>(Expression<Func<T, object>> column)
        {
            this.Rules.Add(new SqlSearchRule
            {
                Field = column.GetFieldName<T>(),
                Operator = SqlSearchOperator.IsNotNull,
                Value = null
            });
            return this;
        }
    }

    public class SqlSearchRule : IEquatable<SqlSearchRule>
    {
        [Required]
        public string Field { get; set; }

        [Required]
        public SqlSearchOperator Operator { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public object Value { get; set; }
        public string FieldType { get; set; } = null;

        #region IEquatable overrides

        public override bool Equals(object obj)
        {
            if (obj is not SqlSearchRule)
                return false;

            return Equals((SqlSearchRule)obj);
        }

        public override int GetHashCode()
        {
            return Field != null ? Field.GetHashCode() : 0
                ^ Operator.GetHashCode()
                ^ (Value != null ? Value.GetHashCode() : 0);
        }

        public bool Equals(SqlSearchRule other)
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
        #endregion
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
        Contains = 1,
        In = 2,
        StartsWith = 3,
        EndsWith = 4,
        Equal = 5,
        GreaterThan = 6,
        LessThan = 7,
        GreaterThanEqual = 8,
        LessThanEqual = 9,
        NotEqual = 10,
        IsNull = 11,
        IsNotNull = 12
       
    }

    public enum SqlConditionOperator
    {
        AND = 1,
        OR = 2
    }



    public class SqlFilterRule : IEquatable<SqlFilterRule>
    {
        [Required]
        public string Field { get; set; }

        [Required]
        public SqlSearchOperator Operator { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public object Value { get; set; }
       
        #region IEquatable overrides

        public override bool Equals(object obj)
        {
            if (obj is not SqlFilterRule)
                return false;

            return Equals((SqlFilterRule)obj);
        }

        public override int GetHashCode()
        {
            return Field != null ? Field.GetHashCode() : 0
                ^ Operator.GetHashCode()
                ^ (Value != null ? Value.GetHashCode() : 0);
        }

        public bool Equals(SqlFilterRule other)
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
        #endregion
    }
}