using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagementModel.Common
{
    public class PagingResult<T>:Paging
    {
        public List<T> list { get; set; }
    }
}
