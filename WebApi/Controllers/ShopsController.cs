using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;
using WebApi.Response.Controller;

namespace WebApi.Controllers
{
    public class ShopsController : ApiController
    {
        private ApplicationDbContext db;

        public ShopsController()
        {
            //Redesign Database access
            db = HttpContext.Current?.GetOwinContext()?.Get<ApplicationDbContext>() ?? new ApplicationDbContext();
        }

        // GET: api/Shops
        public IQueryable<Shop> GetShops()
        {
            return db.Shops;
        }

        // GET: api/Shops/5
        [ResponseType(typeof(Shop))]
        public IHttpActionResult GetShops(string phoneNumber)
        {
            var userId = AccountController.GetUserId(phoneNumber);
            var shopList = db.Shops.Include("Transactions").Where(s => s.Transactions.Any(t => t.UserId == userId)).ToList();
            if (shopList == null)
            {
                return NotFound(); 
            }

            var shopResponse = new ShopControllerResponse(shopList.ToList());
            return Ok(shopResponse);
        }

        // PUT: api/Shops/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShop(string id, Shop shop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shop.ShopId)
            {
                return BadRequest();
            }

            db.Entry(shop).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopExists(id))
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

        // POST: api/Shops
        [ResponseType(typeof(Shop))]
        public IHttpActionResult PostShop(Shop shop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Shops.Add(shop);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shop.ShopId }, shop);
        }

        // DELETE: api/Shops/5
        [ResponseType(typeof(Shop))]
        public IHttpActionResult DeleteShop(string id)
        {
            Shop shop = db.Shops.Find(id);
            if (shop == null)
            {
                return NotFound();
            }

            db.Shops.Remove(shop);
            db.SaveChanges();

            return Ok(shop);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShopExists(string id)
        {
            return db.Shops.Count(e => e.ShopId == id) > 0;
        }

        public static string GetShopName(ApplicationDbContext context, string shopId)
        {
            var shop = context.Shops.Find(shopId);

            return shop?.ShopDetails.Name;
        }
    }
}