using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApi.Utility
{
    public class Helper
    {
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            string regexPattern = @"[\d]{10}";
            var isMatch = Regex.IsMatch(phoneNumber, regexPattern);
            return isMatch;
        }
    }
}