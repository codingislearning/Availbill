using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class OtpDetail
    {
        [Key]
        public string OtpDetailId { get; set; }

        public DateTime GeneratedTimeStamp { get; set; }

        public DateTime ExpiryTimeStamp { get; set; }

        public string PhoneNumber { get; set; }

        public string Otp { get; set; }

        public string Message { get; set; }

        public string Type { get; set; }

        public int RequestCount { get; set; }

        public bool OtpVerified { get; set; }

        public static int UpdateRow(ApplicationDbContext context, OtpDetail detailRow, string property)
        {
            context.OtpDetails.Attach(detailRow);
            context.Entry(detailRow).Property(property).IsModified = true;
            return context.SaveChanges();
        }

        public OtpDetail()
        {
            OtpDetailId = Guid.NewGuid().ToString();
            GeneratedTimeStamp = DateTime.Now;
            ExpiryTimeStamp = DateTime.Now.AddMinutes(10);
        }
    }
}