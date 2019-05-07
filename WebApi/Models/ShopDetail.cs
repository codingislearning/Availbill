using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class ShopDetail
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public string GSTIN { get; set; }

        public string PhoneNumber { get; set; }
    }
}