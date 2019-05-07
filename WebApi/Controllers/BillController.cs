using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Web;
using WebApi.Response.Controller;

namespace WebApi.Controllers
{
    public class BillController : ApiController
    {
        private ApplicationDbContext context;

        public BillController()
        {
            context = HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
        }

        [HttpGet]
        public IHttpActionResult GetBills(string phoneNumber)
        {
            var userId = AccountController.GetUserId(phoneNumber);

            if (!string.IsNullOrEmpty(userId))
            {
                var billList = GetBillByUserId(userId);

                var response = new BillControllerResponse(billList);
                return Ok(response);
            }
            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult GetBills(string phoneNumber, string shopId)
        {
            var userId = AccountController.GetUserId(phoneNumber);

            if (!string.IsNullOrEmpty(userId))
            {
                var billList = GetBillByUserIdAndShopId(userId, shopId);

                var response = new BillControllerResponse(billList);
                return Ok(response);
            }
            return NotFound();
        }

        [HttpPost]
        public IHttpActionResult PostBill([FromBody]Bill bill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //bill cannot be present with item list

            if (bill.ItemList == null || string.IsNullOrWhiteSpace(bill.PhoneNumber))
            {
                return BadRequest();
            }

            //get item for each item in bill
            List<Item> dbItemList = new List<Item>();
            foreach (var item in bill.ItemList)
            {
                var dbItem = new Item() { Detail = item };
                dbItemList.Add(dbItem);
            }
            
            var transaction = new Transaction();
            transaction.Items = dbItemList;
            transaction.TotalBill = bill.Total;
            transaction.TransactionNumber = bill.BillNumber;
            transaction.TransactionDate = bill.TransactionTime;
            transaction.UserId = AccountController.GetUserId(bill.PhoneNumber);
            transaction.ShopId = bill.ShopName;
            //transaction.Shop.Users.Add(AccountController.GetUser(bill.PhoneNumber));

            if (transaction.UserId == null)
            {
                ModelState.AddModelError("Error", "PhoneNumber not registered");
                return BadRequest(ModelState);
            }

            context.Transactions.Add(transaction);
            try
            {
                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

            return Ok();
        }

        private List<Bill> GetBillByUserId(string userId)
        {
            List<Bill> billList = new List<Bill>();
            var transactions = context.Transactions.Include(t => t.Items).Where(t => t.UserId == userId).ToList();

            foreach (var transaction in transactions)
            {
                var bill = new Bill()
                {
                    BillNumber = transaction.TransactionNumber,
                    ItemList = transaction.Items.Select(s => s.Detail).ToList(),
                    ShopName = ShopsController.GetShopName(context, transaction.ShopId) ?? string.Empty,
                    Total = transaction.TotalBill,
                    TransactionTime = transaction.TransactionDate,
                };

                billList.Add(bill);
            }

            return billList;
        }

        private List<Bill> GetBillByUserIdAndShopId(string userId, string shopId)
        {
            List<Bill> billList = new List<Bill>();
            var transactions = context.Transactions.Include(t => t.Items).Where(t => t.UserId == userId && t.ShopId == shopId).ToList();

            foreach (var transaction in transactions)
            {
                var bill = new Bill()
                {
                    BillNumber = transaction.TransactionNumber,
                    ItemList = transaction.Items.Select(s => s.Detail).ToList(),
                    ShopName = ShopsController.GetShopName(context, transaction.ShopId) ?? string.Empty,
                    Total = transaction.TotalBill,
                    TransactionTime = transaction.TransactionDate,
                };

                billList.Add(bill);
            }

            return billList;
        }
    }
}
