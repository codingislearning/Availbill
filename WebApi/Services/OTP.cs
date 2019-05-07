using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApi.Services
{

    public class OtpResponse
    {
        public Enums.OtpResponseType Type { get; set; }

        public string Message { get; set; }

        public static OtpResponse Deserialize(string response)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                var otpObject = serializer.Deserialize<OtpResponse>(response);
                return otpObject;
            }
            catch
            {
                //Log
                return null;
            }
        }
    }

    public class OTP
    {
        public const string BaseUrl = "http://control.msg91.com/api/sendotp.php?";

        private static Dictionary<string, string> Parameters = new Dictionary<string, string>()
        {
            {"template", "" },
            {"otp_length", "" },
            {"authkey", "248644AbGQUfiSTRLH5bf76b67" },
            {"message", "" },
            {"sender", "" },
            {"mobile", "" },
            {"otp", "" },
            {"otp_expiry", "" },
            {"email", "" }
        };

        public static OtpResponse SendOTP(string mobileNumber, string otp)
        {
            Parameters["mobile"] = mobileNumber;
            Parameters["otp"] = otp;
            Parameters["message"] = otp + " is your availbill verification code";
            Parameters["sender"] = "AVLBIL";
            var response = SendRequest(BaseUrl, Parameters);
            return OtpResponse.Deserialize(response);
        }

        private static string SendRequest(string baseUrl, Dictionary<string, string> parameters)
        {
            var requestUrl = baseUrl + GetFlattenDict(parameters);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Method = "POST";

            using (var response = request.GetResponse())
            {
                using (var serverStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(serverStream))
                    {
                        string responseFromServer = reader.ReadToEnd();
                        return responseFromServer;
                    }
                }
            }        
        }

        private static string GetFlattenDict(Dictionary<string, string> dict)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var key in dict.Keys)
            {
                if (!string.IsNullOrEmpty(dict[key]))
                {
                    sb.Append(key);
                    sb.Append("=");
                    sb.Append(HttpUtility.UrlEncode(dict[key]));
                    sb.Append("&");
                }
            }

            return sb.ToString();
        }

        public static bool GenerateAndSendOTP(ApplicationDbContext context, string phoneNumber, out int requestCount)
        {
            var dbOtp = OtpController.GetOTP(context, phoneNumber);

            if (dbOtp?.RequestCount >= 2)
            {
                requestCount = dbOtp.RequestCount;
                return false;
            }

            OtpResponse response;
            int enteredIndatabase = 1;

            if (dbOtp == null)
            {
                var otp = GetRandomOTP();

                response = SendOTP(phoneNumber, otp);
                var otpDetail = new OtpDetail
                {
                    Otp = otp,
                    PhoneNumber = phoneNumber,
                    Message = response.Message,
                    Type = response.Type.ToString(),
                    RequestCount = 1
                };

                context.OtpDetails.Add(otpDetail);
                enteredIndatabase = context.SaveChanges();
                dbOtp = otpDetail;
            }
            else
            {
                response = SendOTP(phoneNumber, dbOtp.Otp);
                dbOtp.RequestCount++;
                enteredIndatabase = OtpDetail.UpdateRow(context, dbOtp, "RequestCount");
            }

            requestCount = dbOtp?.RequestCount ?? 0;
            return enteredIndatabase > 0 && response.Type == Enums.OtpResponseType.success;
        }

        private static void UpdateRequestCount(ApplicationDbContext context, string phoneNumber, string dbOtp)
        {
            
        }

        public static string GetRandomOTP()
        {
            Random generator = new Random();
            String r = generator.Next(0, 9999).ToString("D4");
            return r;
        }
    }
}