using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using OrderManagementDao_Mysql;
using OrderManagementModel.Common;
using OrderManagementModel.DBModel.Authority;
using OrderManagementModel.DBWhere;

namespace OrderManagementBll.Authority
{
    public class PermissionBase
    {
        public IConfiguration Configuration { get; }
        public PermissionBase(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public List<OrderManagementModel.DBModel.Authority.Authority_PermissionBase> GetPermissionList()
        {
            OrderManagementDB DBcontext = new OrderManagementDB();

            var vl = DBcontext.Authority_PermissionBase.OrderByDescending(o => o.ID);

            return vl.ToList();
        }


        public PagingResult<Authority_PermissionBase> GetPaging(Authority_PermissionBaseWhere where)
        {
            PagingResult<Authority_PermissionBase> Result = new PagingResult<Authority_PermissionBase>();


            OrderManagementDB DBcontext = new OrderManagementDB();

            IQueryable<Authority_PermissionBase> Queryable;

            Queryable = DBcontext.Authority_PermissionBase
                .OrderByDescending(o => o.ID);



            if (where.ID > 0)
            {
                Queryable = Queryable.Where(w => w.ID == where.ID);
            }

            if (string.IsNullOrWhiteSpace(where.Action))
            {
                Queryable = Queryable.Where(w => w.Action == where.Action);
            }

            if (string.IsNullOrWhiteSpace(where.Area))
            {
                Queryable = Queryable.Where(w => w.Area == where.Area);
            }

            if (string.IsNullOrWhiteSpace(where.Control))
            {
                Queryable = Queryable.Where(w => w.Control == where.Control);
            }

            if (string.IsNullOrWhiteSpace(where.Description))
            {
                Queryable = Queryable.Where(w => w.Description.Contains(where.Description));
            }

            if (where.PageIndex > 0 && where.PageCount > 0)
            {

                Queryable = DBcontext.Authority_PermissionBase.Skip((where.PageIndex - 1) * where.PageCount).Take(where.PageCount);
            }


            Result.list = Queryable.ToList();

            return Result;

        }

    }
}
