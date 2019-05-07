using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Response
{
    public class ModelStateResponse
    {
        public string Message { get; set; }

        public Dictionary<string, string> ModelState { get; set; }

        public ModelStateResponse()
        {
            Message = "The request is invalid.";
            ModelState = new Dictionary<string, string>();
        }
    }
}