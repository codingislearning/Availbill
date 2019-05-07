using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Bill
    {
        public string PhoneNumber { get; set; }

        public string ShopName { get; set; }

        public string BillNumber { get; set; }

        public  DateTime? TransactionTime { get; set; }

        public  double  Total { get; set; }

        public int TotalItems { get; set; }

        public int TotalQuantity { get; set; }

        public List<ItemDetail> ItemList { get; set; }

    }
}