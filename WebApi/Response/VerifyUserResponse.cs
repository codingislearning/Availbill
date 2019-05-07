using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Response
{
    public class VerifyUserResponse
    {
        public int OtpRequestCount { get; set; }

        public VerifyUserResponse(int requestCount)
        {
            OtpRequestCount = requestCount;
        }
    }
}