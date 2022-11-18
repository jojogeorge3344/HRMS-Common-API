using System;
using System.Collections.Generic;
using Chef.Common.Repositories;
using SqlKata;
using SqlKata.Execution;
using System.Linq;

namespace Chef.Common.Core.Extensions;

public static class SqlKataExtensionMethods
{
    public static Query Query<T>(this QueryFactory qf)
    {
        //schema.tablename
        return qf.Query(typeof(T).Namespace.Split('.')[1].ToLower() + "." + typeof(T).Name.ToLower());
    }

    public static Query Join<R, T>(this Query q)
    {
        var rname = typeof(R).Name.ToLower();
        var tname = typeof(T).Name.ToLower();

        return q.Join($"{tname}", $"{rname}.Id", $"{tname}.{rname}Id");
    }

    public static Query JoinChild<R, T>(this Query q)
    {
        var rschema = typeof(R).Namespace.Split('.')[1].ToLower();
        var tschema = typeof(T).Namespace.Split('.')[1].ToLower();

        var rname = typeof(R).Name.ToLower();
        var tname = typeof(T).Name.ToLower();

        return q.Join($"{tschema}.{tname}",
            $"{tschema}.{tname}.Id",
            $"{rschema}.{rname}.{tname}.Id");
    }

    public static Query WhereNotArchived(this Query q)
    {
        return q.Where("isarchived", false);
    }

    public static Query WhereForBranch(this Query q)
    {
        return q.Where("branchid", HttpHelper.BranchId);
    }

    public static Query Where<T>(this Query q, string field, object val)
    {
        var schema = typeof(T).Namespace.Split('.')[1].ToLower();
        return q.Where($"{schema}.{typeof(T).Name.ToLower()}.{field}", val);
    }

    public static Query InsertDefaults(this Query q)
    {
        return q.AsInsert(new
        {
            createddate = DateTime.UtcNow,
            isarchived = false,
            createdby = HttpHelper.Username
        }); ;
    }

    public static Query UpdateDefaults(this Query q)
    {
        return q.AsUpdate(new
        {
            modifieddate = DateTime.UtcNow,
            modifiedby = HttpHelper.Username
        });
    }


    public static Query InsertDefaults<T>(this Query q, ref T obj) where T : Model
    {
        obj.CreatedDate = DateTime.UtcNow;
        obj.IsArchived = false;
        obj.CreatedBy = HttpHelper.Username; 

        return q;
    }

    public static Query InsertDefaults<T>(this Query q, ref List<T> objs) where T : Model
    {
        objs.ToList().ForEach(obj =>
        {
            obj.CreatedDate = DateTime.UtcNow;
            obj.IsArchived = false;
            obj.CreatedBy = HttpHelper.Username;
        });

        return q;
    }

    public static Query UpdateDefaults<T>(this Query q, ref T obj) where T : Model
    {
        obj.ModifiedDate = DateTime.UtcNow;
        obj.ModifiedBy = HttpHelper.Username;

        return q;
    }

    public static Query UpdateDefaults<T>(this Query q, ref List<T> objs) where T : Model
    {
        objs.ToList().ForEach(obj =>
        {
            obj.ModifiedDate = DateTime.UtcNow;
            obj.ModifiedBy = HttpHelper.Username;
        });

        return q;
    }


    public static void AddArchiveFilter(this SqlSearch search)
    {
        search.Rules.Add(
            new SqlSearchRule()
            {
                Field = "isarchived",
                Operator = SqlSearchOperator.Equal,
                Value = false
            });
    }

    public static void AddActiveFilter(this SqlSearch search)
    {
        search.Rules.Add(
            new SqlSearchRule()
            {
                Field = "isactive",
                Operator = SqlSearchOperator.Equal,
                Value = false
            });
    }

    public static void AddBranchFilter(this SqlSearch search, int branchId)
    {
        search.Rules.Add(
             new SqlSearchRule()
             {
                 Field = "branchid",
                 Operator = SqlSearchOperator.Equal,
                 Value = branchId
             });
    }
}
