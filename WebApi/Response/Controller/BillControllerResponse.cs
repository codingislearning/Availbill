using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Response.Controller
{
    public class BillControllerResponse
    {
        public List<Bill> Bills { get; set; }

        public BillControllerResponse(List<Bill> billList)
        {
            Bills = billList;
        }
    }
}