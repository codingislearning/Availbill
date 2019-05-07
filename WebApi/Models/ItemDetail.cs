using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class ItemDetail
    {
        public string Name { get; set; }

        public double Quantity { get; set; }

        public double Rate { get; set; }

        public double Amount { get; set; }

        public string HSN { get; set; }

        public double CGSTPerc { get; set; }

        public double CGSTValue { get; set; }

        public double SGSTPerc { get; set; }

        public double SGSTValue { get; set; }

        public double Discount { get; set; }

        public double Tax { get; set; }
    }
}