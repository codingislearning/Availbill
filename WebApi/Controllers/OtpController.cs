using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    public class OtpController : ApiController
    {
        private ApplicationDbContext context;

        public OtpController()
        {
            context = HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
        }

        // GET: api/Otp
        public IQueryable<OtpDetail> GetOtpDetails()
        {
            return context.OtpDetails;
        }

        // GET: api/Otp/5
        [ResponseType(typeof(OtpDetail))]
        public IHttpActionResult GetOtpDetail(string id)
        {
            OtpDetail otpDetail = context.OtpDetails.Find(id);
            if (otpDetail == null)
            {
                return NotFound();
            }

            return Ok(otpDetail);
        }

        // PUT: api/Otp/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOtpDetail(string id, OtpDetail otpDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != otpDetail.OtpDetailId)
            {
                return BadRequest();
            }

            context.Entry(otpDetail).State = EntityState.Modified;

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OtpDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Otp
        [ResponseType(typeof(OtpDetail))]
        public IHttpActionResult PostOtpDetail(OtpDetail otpDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.OtpDetails.Add(otpDetail);

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OtpDetailExists(otpDetail.OtpDetailId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = otpDetail.OtpDetailId }, otpDetail);
        }

        // DELETE: api/Otp/5
        [ResponseType(typeof(OtpDetail))]
        public IHttpActionResult DeleteOtpDetail(string id)
        {
            OtpDetail otpDetail = context.OtpDetails.Find(id);
            if (otpDetail == null)
            {
                return NotFound();
            }

            context.OtpDetails.Remove(otpDetail);
            context.SaveChanges();

            return Ok(otpDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OtpDetailExists(string id)
        {
            return context.OtpDetails.Count(e => e.OtpDetailId == id) > 0;
        }

        public static OtpDetail GetOTP(ApplicationDbContext context , string phoneNumber)
        {
            var otpRow = context.OtpDetails.Where(s => s.PhoneNumber == phoneNumber && s.ExpiryTimeStamp > DateTime.Now).ToList();

            if (otpRow.Count == 1)
            {
                return otpRow.First();
            }
            return null;
        }

        //To Test
        public static bool IsOtpVerified(ApplicationDbContext context, string phoneNumber)
        {
            var otpDetailForPhoneNumber = GetOTP(context, phoneNumber);
            if (otpDetailForPhoneNumber != null)
            {
                return otpDetailForPhoneNumber.OtpVerified;
            }

            return false;
        }
    }
}