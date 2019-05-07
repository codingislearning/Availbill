using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Enums;

namespace WebApi.Models
{
    public class ResponseSummary
    {
        public string Error { get; set; }

        public ActionStatus Status { get; set; }
    }
}