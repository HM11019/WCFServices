using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFServices.Models
{
    public class CustomQueries
    {
        public class CustomerList
        {
            public string CustomerID { get; set; }
            public string CompanyName { get; set; }
            public string ContactName { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }
        }

        public class CustomerOrderInfoList
        {
            public int OrderID { get; set; }
            public string CustomerID { get; set; }
            public DateTime OrderDate { get; set; }
            public DateTime ShippedDate { get; set; }
        }
    }
}