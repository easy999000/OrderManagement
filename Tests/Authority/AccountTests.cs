using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderManagementBll.Authority;
using OrderManagementDao_Mysql;
using OrderManagementModel.DBWhere;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagementBll.Authority.Tests
{
    [TestClass()]
    public class AccountTests
    {
        public AccountTests()
        {
            OrderManagementDB.ConnectionString = "server=192.168.18.115;user=root;database=OrderManagement;port=3306;password=ABCabc123!;SslMode=None";
        }

        [TestMethod()]
        public void GetAccountWhereTest()
        {
            OrderManagementBll.Authority.Account AccountBll = new OrderManagementBll.Authority.Account(null);


            AccountWhere where = new AccountWhere();

            //where.ID = 3;
            //where.Age_Min = 2;
            //where.Age_Max = 20;
            //where.Name = "33";
            //where.Phone = "15940";
            //where.IDs = new int[] { 1, 3, 5, 7, 9 };
            where.NoIDs = new int[] { 1, 3, 5, 7, 9 };


            var v1 = AccountBll.GetAccountWhere(where);


        }
    }
}