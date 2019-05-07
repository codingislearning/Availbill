using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Response.Controller
{
    
    public class ShopControllerResponse
    {
        public List<Shop> Shops { get; set; }

        public ShopControllerResponse(List<Shop> shopList)
        {
            this.Shops = shopList;
        }
    }
}